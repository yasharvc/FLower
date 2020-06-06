using Core.Models;
using System.Collections.Generic;
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
	}
}