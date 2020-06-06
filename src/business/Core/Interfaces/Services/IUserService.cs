using Core.Models;
using Core.Models.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
	public interface IUserService : ICRUDService<User>
	{
		Task<bool> IsUserNamePasswordValid(string username, string password);
		Task GrantRoleToUser(string userID, string roleID);
		Task RevokeRoleFromUser(string userID, string roleID);
		Task<bool> HasUserRole(string userID, string roleID);
		Task<List<Role>> GetUserRoles(string userID);
		Task<User> GetUser(string userID);
		Task AddUserToGroup(string userID, string groupID);
		Task AddUserToGroups(string userID, IEnumerable<string> groupsIDs);
	}
}