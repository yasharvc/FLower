using Core.Models.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
	public interface IRoleMenuRepository : ICRUDRepository<RoleMenu>
	{
		Task<IEnumerable<RoleMenu>> GetByRoleID(string id);
	}
}