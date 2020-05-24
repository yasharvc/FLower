using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
	public interface ICRUDService<T> where T: Model
	{
		Task Create(T entity);
		Task Update(T entity);
		Task Delete(T entity);
		Task<GenericQueryResult<Y>> Query<Y>(
			List<string> fieldsList,
			Dictionary<string, bool> sort,
			List<Comparison> conditions,
			int page,
			int sizeInPage);
	}
}