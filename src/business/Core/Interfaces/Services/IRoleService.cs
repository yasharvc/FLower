using Core.Models.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
	public interface IRoleService : ICRUDService<Role>
	{
		Task<List<Role>> GetRolesByIDs(IEnumerable<string> ids);
		Task<Role> GetRoleByID(string roleID);
	}
}