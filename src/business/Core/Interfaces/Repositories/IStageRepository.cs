using Core.Models;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
	public interface IStageRepository : ICRUDRepository<Stage>
	{
		Task<Stage> GetByUniqueID(string uniqueID);
	}
}