using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
	public class RoleService : BaseCRUDService<Role>, IRoleService
	{
		public RoleService(IRoleRepository repository) : base(repository) { }

		public async Task<Role> GetRoleByBName(string roleName)
		{
			var lst = await Repository.Where(m => m.Name.Equals(roleName));
			return lst.First();
		}

		public async Task<Role> GetRoleByID(string roleID) => await Repository.GetByID(roleID);

		public async Task<List<Role>> GetRolesByIDs(IEnumerable<string> ids)
		{
			var res = new List<Role>();
			if (ids == null)
				return res;
			foreach (var item in ids)
			{
				res.Add(await GetRoleByID(item));
			}
			return res;
		}
	}
}