using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
	public interface IQueryRepository
	{
		Task<GenericQueryResult<T>> Query<T>(
			List<string> fieldsList,
			Dictionary<string, bool> sort,
			List<Comparison> conditions,
			int page,
			int sizeInPage);
	}
}