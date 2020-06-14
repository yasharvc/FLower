using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
	public class RoleMenuService : BaseCRUDService<RoleMenu>, IRoleMenuService
	{
		IRoleMenuRepository RoleMenuRepository { get; set; }
		public RoleMenuService(IRoleMenuRepository repository) : base(repository) {
			RoleMenuRepository = repository;
		}

		public async Task<IEnumerable<RoleMenu>> GetByRolesIDs(IEnumerable<Role> roles)
		{
			var res = new List<RoleMenu>();
			roles.ToList().ForEach(async role => res.AddRange(await RoleMenuRepository.GetByRoleID(role._id)));
			return res;
		}

		public async Task<IEnumerable<RoleMenu>> GetByRoleID(string roleID) => await RoleMenuRepository.GetByRoleID(roleID);
	}
}