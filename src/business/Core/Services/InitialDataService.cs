using Core.Consts;
using Core.Interfaces.Services;
using Core.Models;
using Core.Models.Security;
using System.Threading.Tasks;

namespace Core.Services
{
	public class InitialDataService : IInitialDataService
	{
		IRoleService RoleService { get; set; }
		IGroupService GroupService { get; set; }
		IUserService UserService { get; set; }
		IUserRoleService UserRoleService { get; set; }

		public InitialDataService(
			IRoleService roleService
			, IGroupService groupService
			, IUserService userService
			, IUserRoleService userRoleService
			)
		{
			RoleService = roleService;
			GroupService = groupService;
			UserService = userService;
			UserRoleService = userRoleService;
		}


		public async Task Init()
		{
			if (!await IsDataExists())
			{
				await CreateRoles();
				await CreateGroups();
				await CreateUser();
			}
		}

		private async Task<bool> IsDataExists() => await UserService.IsUserNamePasswordValid("Yashar", "123");

		private async Task CreateUser()
		{
			var superadmin = new User
			{
				Password = "123",
				Username = "Yashar"
			};
			await UserService.Create(superadmin);
			await AddRolesToUser(superadmin, Roles.SuperAdmin);
		}

		private async Task AddRolesToUser(User user, string roleName)
		{
			await UserRoleService.GrantRoleToUser(user._id,(await RoleService.GetRoleByBName(roleName))._id);
		}

		private async Task CreateGroups()
		{
		}

		private async Task CreateRoles()
		{
			await RoleService.Create(new Role
			{
				Name = Roles.SuperAdmin,
				Description = "User who has all the priviliges"
			});
			await RoleService.Create(new Role
			{
				Name = Roles.ExternalUser
			});
			await RoleService.Create(new Role
			{
				Name = Roles.ProcessCreator
			});
			await RoleService.Create(new Role
			{
				Name = Roles.ProcessDataManipulator
			});
			await RoleService.Create(new Role
			{
				Name = Roles.ProcessInitiator
			});
			await RoleService.Create(new Role
			{
				Name = Roles.ProcessManipulator
			});
		}
	}
}
