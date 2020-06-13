using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
	public interface IQueryRepository<T> where T: Model
	{
		Task<GenericQueryResult<T>> Query(
			List<string> fieldsList,
			Dictionary<string, bool> sort,
			List<Comparison> conditions,
			int page,
			int sizeInPage);
		Task<T> GetByID(string id);
		Task<IQueryable<T>> Where(Func<T, bool> predicate);
		Task<bool> Any(Func<T, bool> predicate);
	}
}