using Core.Models.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
	public interface IRoleMenuService : ICRUDService<RoleMenu>
	{
		Task<IEnumerable<RoleMenu>> GetByRolesIDs(IEnumerable<Role> roles);
		Task<IEnumerable<RoleMenu>> GetByRoleID(string roleID);
	}
}