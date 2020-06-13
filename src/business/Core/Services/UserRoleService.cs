using Core.Exceptions.Application;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Models.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
	public class UserRoleService : BaseCRUDService<UserRole>, IUserRoleService
	{
		IRoleService RoleService { get; set; }
		IUserService UserService { get; set; }

		public UserRoleService(IUserRoleRepository repository, IRoleService roleService, IUserService userService) : base(repository)
		{
			RoleService = roleService;
			UserService = userService;
		}

		public async Task<IEnumerable<Role>> GetUserRoles(string userID)
		{
			var user = await UserService.GetUser(userID);
			var roleIDs = await Repository.Where(m => m.UserID == userID);
			return await RoleService.GetRolesByIDs(roleIDs.Select(m => m.RoleID));
		}

		public async Task GrantRoleToUser(string userID, string roleID)
		{
			try
			{
				await UserService.GetUser(userID);
			}
			catch
			{
				throw new UserNotFoundException(userID);
			}
			try
			{
				await RoleService.GetRoleByID(roleID);
			}
			catch
			{
				throw new RoleNotFoundException(roleID);
			}
			await Create(new UserRole
			{
				RoleID = roleID,
				UserID = userID
			});
		}
		public async Task RevokeRoleFromUser(string userID, string roleID) =>
			await Delete(new UserRole { RoleID = roleID, UserID = userID });

		public async Task<bool> HasUserThisRole(string userID, string roleID) => await Repository.Any(m => m.RoleID == roleID && m.UserID == userID);

		public async Task<bool> HasUserOnOfTheseRoles(string userID, IEnumerable<string> roleIDs) => await Repository.Any(m => m.UserID == userID && roleIDs.Contains(m.RoleID));

		public override async Task Create(UserRole entity)
		{
			if(!await HasUserThisRole(entity.UserID, entity.RoleID))
				await base.Create(entity);
		}
	}
}