using Core.Exceptions.Application;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
	public class UserService : BaseCRUDService<User>, IUserService
	{
		IRoleService RoleService { get; set; }
		IGroupService GroupService { get; set; }

		public UserService(IUserRepository repository, IRoleService roleService, IGroupService groupService) : base(repository)
		{ 
			RoleService = roleService;
			GroupService = groupService;
		}


		public async Task<User> GetUser(string userID) => await Repository.GetByID(userID);

		public async Task<List<Role>> GetUserRoles(string userID)
		{
			try
			{
				var user = await Repository.GetByID(userID);
				return await RoleService.GetRolesByIDs(user.Groups);
			}
			catch
			{
				throw new UserNotFoundException(userID);
			}
		}

		public async Task GrantRoleToUser(string userID, string roleID)
		{
			var user = new User();
			var role = new Role();
			try
			{
				user = await Repository.GetByID(userID);
			}
			catch
			{
				throw new UserNotFoundException(userID);
			}
			try
			{
				role = await RoleService.GetRoleByID(roleID);
			}
			catch
			{
				throw new RoleNotFoundException(roleID);
			}
			throw new NotImplementedException();
		}

		public async Task<bool> HasUserRole(string userID, string roleID)
		{
			throw new NotImplementedException();
		}

		public async Task<bool> IsUserNamePasswordValid(string username, string password)
		{
			throw new NotImplementedException();
		}

		public async Task RevokeRoleFromUser(string userID, string roleID)
		{
			throw new NotImplementedException();
		}

		public async Task AddUserToGroup(string userID, string groupID)
		{
			var user = new User();
			var group = new Group();
			try
			{
				user = await Repository.GetByID(userID);
				if (user.Groups.Contains(groupID))
					return;
			}
			catch
			{
				throw new UserNotFoundException(userID);
			}
			try
			{
				group = await GroupService.GetGroupByID(groupID);
			}
			catch
			{
				throw new GroupNotFoundException(groupID);
			}
			((List<string>)user.Groups).Add(groupID);
			await Repository.Update(user);
		}

		public async Task AddUserToGroups(string userID, IEnumerable<string> groupsIDs)
		{
			var user = new User();
			var group = new List<Group>();
			try
			{
				user = await Repository.GetByID(userID);
				var commons = user.Groups.Intersect(groupsIDs);
				groupsIDs = groupsIDs.Where(m => !commons.Contains(m));
			}
			catch
			{
				throw new UserNotFoundException(userID);
			}
			try
			{
				group = await GroupService.GetGroupsByIDs(groupsIDs);
			}
			catch
			{
				throw new GroupNotFoundException(string.Join(",", groupsIDs));
			}
			((List<string>)user.Groups).AddRange(groupsIDs);
			await Repository.Update(user);
		}
	}
}
