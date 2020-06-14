using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
	public class MenuService : BaseCRUDService<Menu>, IMenuService
	{
		public MenuService(IMenuRepository repository) : base(repository) { }

		public async Task<IEnumerable<Menu>> GetMenusByIDs(IEnumerable<string> ids)
		{
			var res = new List<Menu>();
			foreach (var id in ids)
			{
				var menu = await Repository.GetByID(id);
				res.Add(menu);
			}
			return res;
		}
	}
}