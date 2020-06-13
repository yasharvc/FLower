using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models.Security;

namespace Core.Services
{
	public class RoleMenuService : BaseCRUDService<RoleMenu>, IRoleMenuService
	{
		public RoleMenuService(IRoleMenuRepository repository) : base(repository) { }
	}
}