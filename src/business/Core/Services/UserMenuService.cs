using Core.Interfaces.Services;
using Core.Models;
using Core.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
	public class UserMenuService : IUserMenuService
	{
		IRoleMenuService RoleMenuService { get; }
		IMenuService MenuService { get; }
		IUserRoleService UserRoleService { get; }

		public UserMenuService(
			IUserRoleService userRoleService
			, IRoleMenuService roleMenuService
			, IMenuService menuService)
		{
			RoleMenuService = roleMenuService;
			MenuService = menuService;
			UserRoleService = userRoleService;
		}


		public async IAsyncEnumerable<Menu> GetUserMenu(string userID)
		{
			await foreach (var role in UserRoleService.GetUserRoles(userID))
			{
				var menuIDS = await RoleMenuService.GetByRoleID(role._id);
				foreach (var item in await MenuService.GetMenusByIDs(menuIDS.Select(m => m.MenuID)))
				{
					yield return item;
				}
			}
		}
	}
}
