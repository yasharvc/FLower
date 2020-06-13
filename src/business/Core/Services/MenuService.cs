using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models.Security;

namespace Core.Services
{
	public class MenuService : BaseCRUDService<Menu>, IMenuService
	{
		public MenuService(IMenuRepository repository) : base(repository) { }
	}
}