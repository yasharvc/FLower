using Core.Enums;
using Core.Interfaces.Repositories;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InMemoryRepository
{
	public abstract class InMemoryRepository<T> : ICRUDRepository<T> where T : Model,new()
	{
		protected List<T> repository { get; set; } = new List<T>();
		public int Count => repository.Count;

		public async Task Create(T entity)
		{
			entity._id = Guid.NewGuid().ToString();
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
				if (repository[i]._id == entity._id)
					return i;
			}
			return -1;
		}

		public async Task<GenericQueryResult<T>> Query(List<string> fieldsList,
			Dictionary<string, bool> sort,
			List<Comparison> conditions, int page, int sizeInPage)
		{
			var wheredList = repository.Where(x => GetWhere(x,conditions));
			var sortedList = Sort(wheredList, sort).Select(m => Select(m, fieldsList));
			return await Task.Run(() => new GenericQueryResult<T>
			{
				Result = sortedList.AsQueryable(),
				_id = Guid.NewGuid().ToString(),
				TotalRecordCount = sortedList.Count()

			});
		}

		private T Select(T m, List<string> props)
		{
			T myobject = new T();

			foreach (var item in props)
			{
				var prop = m.GetType().GetProperty(item);
				if(prop != null)
					prop.SetValue(myobject, prop.GetValue(m));
			}
			return myobject;
		}

		private IOrderedEnumerable<T> Sort(IEnumerable<T> wheredList, Dictionary<string, bool> sorts)
		{
			bool isSorted = false;
			IOrderedEnumerable<T> res = null;

			foreach (var sort in sorts)
			{
				var _class = Expression.Parameter(typeof(T), "cls");
				var nameSelection = Expression.PropertyOrField(_class, sort.Key);
				var lambda = Expression.Lambda<Func<T, string>>(nameSelection, _class);
				if (!isSorted)
				{
					res = sort.Value ?
						wheredList.OrderBy(lambda.Compile())
						: wheredList.OrderByDescending(lambda.Compile());
					isSorted = true;
				}
				else
					res = sort.Value ?
						res.ThenBy(lambda.Compile())
						: res.ThenByDescending(lambda.Compile());
			}
			return res;
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

		public async Task<T> GetByID(string id) => await Task.Run(() => repository.Single(m => m._id == id));
	}
}
