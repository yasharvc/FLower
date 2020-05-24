using Core.Models;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
	public interface IProcessRepository: ICRUDRepository<Process>
	{
		Task<Process> GetByUniqueID(string uniqueID);
	}
}