using Core.Models.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
	public interface IUserRoleService : ICRUDService<UserRole>
	{
		IAsyncEnumerable<Role> GetUserRoles(string userID);
		Task<bool> HasUserThisRole(string userID, string roleID);
		Task<bool> HasUserOnOfTheseRoles(string userID, IEnumerable<string> roleIDs);
		Task GrantRoleToUser(string userID, string roleID);
		Task RevokeRoleFromUser(string userID, string roleID);
	}
}