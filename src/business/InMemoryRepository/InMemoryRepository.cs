using Core.Enums;
using Core.Interfaces.Repositories;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryRepository
{
	public abstract class InMemoryRepository<T> : ICRUDRepository<T> where T : Model
	{
		protected List<T> repository { get; set; } = new List<T>();
		public int Count => repository.Count;

		public async Task Create(T entity)
		{
			entity.ID = Guid.NewGuid().ToString();
			await Task.Run(() => repository.Add(entity));
		}

		public async Task Delete(T entity) => await Task.Run(() => repository.Remove(entity));

		public async Task<IQueryable<T>> GetAll() => await Task.Run(() => repository.AsQueryable());

		public async Task<IQueryable<T>> GetPaged(int page, int countInPage) => await Task.Run(() => repository.Skip((page - 1) * countInPage).Take(countInPage).AsQueryable());

		public async Task Update(T entity)
		{
			await Task.Run(() =>
			{
				repository[FindEntity(entity)] = entity;
			});
		}

		protected virtual int FindEntity(T entity)
		{
			for (int i = 0; i < repository.Count; i++)
			{
				if (repository[i].ID == entity.ID)
					return i;
			}
			return -1;
		}

		public async Task<GenericQueryResult<T>> Query(List<string> fieldsList,
			Dictionary<string, bool> sort,
			List<Comparison> conditions, int page, int sizeInPage)
		{
			var list = repository.Where(x => GetWhere(x,conditions));
			return await Task.Run(() => new GenericQueryResult<T>
			{
				Result = list,
				ID = Guid.NewGuid().ToString(),
				TotalRecordCount = list.Count()

			});
		}

		private bool GetWhere(T x,List<Comparison> conditions)
		{
			var res = true;
			foreach (var item in conditions)
			{
				var prop = typeof(T).GetProperty(item.FieldName);
				object value = prop.GetValue(x);
				switch (item.Operation)
				{
					case ComparisonOperation.Equal:
						res &= value
							== Convert.ChangeType(item.GetFirstValue(), prop.PropertyType);
						break;
					case ComparisonOperation.Bigger:
						res &= (value as IComparable).
							CompareTo(Convert.ChangeType(item.GetFirstValue(), prop.PropertyType)) > 0;
						break;
					case ComparisonOperation.Smaller:
						res &= (value as IComparable).
							CompareTo(Convert.ChangeType(item.GetFirstValue(), prop.PropertyType)) < 0;
						break;
					case ComparisonOperation.BiggerOrEqaul:
						res &= (value as IComparable).
							CompareTo(Convert.ChangeType(item.GetFirstValue(), prop.PropertyType)) >= 0;
						break;
					case ComparisonOperation.SmallerOrEqual:
						res &= (value as IComparable).
							CompareTo(Convert.ChangeType(item.GetFirstValue(), prop.PropertyType)) <= 0;
						break;
					case ComparisonOperation.NotEqual:
						res &= (value as IComparable).
							CompareTo(Convert.ChangeType(item.GetFirstValue(), prop.PropertyType)) != 0;
						break;
					case ComparisonOperation.Contains:
						res &= value.ToString().Contains(
							Convert.ToString(item.GetFirstValue()));
						break;
					case ComparisonOperation.EndWith:
						res &= value.ToString().EndsWith(
							Convert.ToString(item.GetFirstValue()));
						break;
					case ComparisonOperation.StartWith:
						res &= value.ToString().StartsWith(
							Convert.ToString(item.GetFirstValue()));
						break;
				}
			}
			return res;
		}
	}
}
