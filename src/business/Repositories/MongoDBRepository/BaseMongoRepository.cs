using Core.Enums;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Models.SystemModels;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBRepository
{
	public abstract class BaseMongoRepository<T> : ICRUDRepository<T> where T : Model
	{
		public IMongoCollection<T> Repository { get; protected set; }

		protected IMongoDatabase Database { get; set; }

		protected abstract string CollectionName { get; }

		public int Count => (int)Repository.CountDocuments<T>(m => true);

		protected virtual void ConfigureCollection() { }


		internal BaseMongoRepository(DatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			Database = client.GetDatabase(settings.DatabaseName);

			Repository = Database.GetCollection<T>(CollectionName);

			ConfigureCollection();
		}

		public async Task Create(T entry)
		{
			GetNewGUID(entry);
			await Repository.InsertOneAsync(entry);
		}

		private static void GetNewGUID(T entry) => entry._id = Guid.NewGuid().ToString();

		public async Task<IEnumerable<T>> CreateMany(IEnumerable<T> entry)
		{
			foreach (var item in entry)
			{
				GetNewGUID(item);
			}
			await Repository.InsertManyAsync(entry);
			return entry;
		}

		public async Task<IQueryable<T>> GetAll()
		{
			var result = await Repository.FindAsync(e => true);
			var list = await result.ToListAsync();
			return list.AsQueryable();
		}

		public async Task<T> GetById(string id)
		{
			var result = await Repository.FindAsync(t => t._id == id);
			return result.Single();
		}

		public async Task<T> GetFirst()
		{
			var res = await Repository.FindAsync(m => true);
			return res.First();
		}

		public async Task Delete(T entry) => await Repository.DeleteOneAsync(t => t._id == entry._id);
		public async Task Delete(string id) => await Repository.DeleteOneAsync(t => t._id == id);

		public async Task Update(T entry) => await Repository.ReplaceOneAsync(t => t._id == entry._id, entry);

		public async Task<IQueryable<T>> GetPaged(int page, int countInPage)
		{
			return await Task.Run(() =>
			{
				var repo = Repository as IQueryable<T>;
				return repo.Skip((page - 1) * countInPage).Take(countInPage).AsQueryable();
			});
		}

		public async Task<GenericQueryResult<T>> Query(List<string> fieldsList, Dictionary<string, bool> sort, List<Comparison> conditions, int page, int sizeInPage)
		{
			var filter = await GetFilter(CollectionName, conditions, fieldsList);
			var collection = Database.GetCollection<BsonDocument>(CollectionName);
			var totalCount = await collection.CountDocumentsAsync(filter);

			var res = await collection
				.Find(filter)
				.Project<T>(await GetProjection(fieldsList))
				.Sort(GetSort(sort))
				.Skip((page - 1) * sizeInPage).Limit(sizeInPage).ToListAsync();

			return new GenericQueryResult<T>
			{
				Result = res.AsQueryable(),
				TotalRecordCount = totalCount
			};
		}

		private async Task<ProjectionDefinition<BsonDocument>> GetProjection(List<string> columnsList = null)
		{
			var projection = Builders<BsonDocument>.Projection;
			var projectionDefinitions = new List<ProjectionDefinition<BsonDocument>>();
			if (columnsList == null)
			{
				var lst = await GetColumns(CollectionName);
				columnsList = lst.Select(m => m.Key).ToList();
			}
			columnsList.ForEach(m => {
				projectionDefinitions.Add(projection.Include(m));
			});
			if (!columnsList.Contains("_id"))
				projectionDefinitions.Add(projection.Exclude("_id"));
			return projection.Combine(projectionDefinitions);
		}

		private SortDefinition<BsonDocument> GetSort(Dictionary<string, bool> sort = null)
		{
			var sorts = new List<SortDefinition<BsonDocument>>();
			if (sort != null)
			{
				foreach (var item in sort)
				{
					if (item.Value)
						sorts.Add(Builders<BsonDocument>.Sort.Ascending(item.Key));
					else
						sorts.Add(Builders<BsonDocument>.Sort.Descending(item.Key));
				}
			}
			return Builders<BsonDocument>.Sort.Combine(sorts);
		}

		private async Task<string> GetFilter(string collectionName, List<Comparison> conditions, List<string> columnsList)
		{
			if (conditions == null || conditions.Count == 0)
				return "{}";
			string filter = null;
			foreach (var item in conditions)
			{
				if (filter == null)
					filter = await GetFilterFor(item);
				else
					filter += "," + await GetFilterFor(item);
			}
			return $"{{{filter}}}";
		}

		private async Task<Dictionary<string, BsonType>> GetColumns(string collectionName, BsonDocument item = null)
		{
			if (item == null)
				item = await GetFirstItem(collectionName);
			if (item == null)
				return new Dictionary<string, BsonType>();
			var res = new Dictionary<string, BsonType>();
			foreach (var element in item.Elements)
			{
				res[element.Name] = element.Value.BsonType;
			}
			return res;
		}

		private async Task<BsonDocument> GetFirstItem(string collectionName) => await Database.GetCollection<BsonDocument>(collectionName)
								.Find(Builders<BsonDocument>.Filter.Empty).FirstOrDefaultAsync();

		private async Task<string> GetFilterFor(Comparison comparison)
		{
			return await Task.Run(() =>
			{
				var fieldName = $"'{comparison.FieldName}'";
				var firstValue = comparison.GetFirstValue();
				firstValue = ConvertToMongoTypeValue(comparison, firstValue);
				switch (comparison.Operation)
				{
					case ComparisonOperation.Equal:
						return $"{fieldName} : {{'$eq':{firstValue}}}";
					case ComparisonOperation.Bigger:
						return $"{fieldName} : {{'$gt':{firstValue}}}";
					case ComparisonOperation.Smaller:
						return $"{fieldName} : {{'$lt':{firstValue}}}";
					case ComparisonOperation.BiggerOrEqaul:
						return $"{fieldName} : {{'$gte':{firstValue}}}";
					case ComparisonOperation.SmallerOrEqual:
						return $"{fieldName} : {{'$lte':{firstValue}}}";
					case ComparisonOperation.Between:
						return $"{fieldName} : {{'$gte':{firstValue},'$lt' : {ConvertToMongoTypeValue(comparison, comparison.Values[1])}}}";
					case ComparisonOperation.Contains:
						return $"{fieldName} : {{'$regex':{ConvertToMongoTypeValue(comparison, $".*{comparison.GetFirstValue()}.*")}}}";
					case ComparisonOperation.EndWith:
						return $"{fieldName} : {{'$regex':{ConvertToMongoTypeValue(comparison, $".*{comparison.GetFirstValue()}")}}}";
					case ComparisonOperation.StartWith:
						return $"{fieldName} : {{'$regex':{ConvertToMongoTypeValue(comparison, $"{comparison.GetFirstValue()}.*")}}}";
					case ComparisonOperation.NotEqual:
						return $"{fieldName} : {{'$ne':{firstValue}}}";
					case ComparisonOperation.In:
						string arrayEqaul = "";
						comparison.Values.ForEach(value =>
							arrayEqaul += (arrayEqaul.Length > 0 ? "," : "") +
										ConvertToMongoTypeValue(comparison, value));
						return $"{fieldName} : {{'$in':[{arrayEqaul}]}}";
					default:
						return "";
				}
			});
		}
		private string ConvertToMongoTypeValue(Comparison comparison, string value)
		{
			if (comparison.ColumnType == BaseColumnType.String)
				return $"'{value}'";
			else if (comparison.ColumnType == BaseColumnType.Date)
				return $"ISODate('{value}')";
			else if (comparison.ColumnType == BaseColumnType.Boolean)
				return Convert.ToBoolean(value) ? "true" : "false";
			else
				return value;
		}

		public async Task<T> GetByID(string id) => await Database.GetCollection<T>(CollectionName)
			.Find(m => m._id == id).SingleAsync();

		public async Task<IQueryable<T>> Where(Func<T, bool> predicate)
		{
			var lst = await Database.GetCollection<T>(CollectionName).Find(new BsonDocument()).ToListAsync();
			return lst.Where(predicate).AsQueryable();
		}

		public async Task<bool> Any(Func<T, bool> predicate)
		{
			var lst = await Database.GetCollection<T>(CollectionName).Find(new BsonDocument()).ToListAsync();
			return lst.Any(predicate);
		}
	}
}