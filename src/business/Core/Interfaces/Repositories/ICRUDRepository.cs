using Core.Models;

namespace Core.Interfaces.Repositories
{
	public interface ICRUDRepository<T> : ICUDRepository<T>, IQueryRepository where T : Model
	{
	}
}