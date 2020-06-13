using Core.Models;
using Core.Models.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
	public interface IUserService : ICRUDService<User>
	{
		Task<bool> IsUserNamePasswordValid(string username, string password);
		Task<User> GetUser(string userID);
		Task<User> GetUser(string username, string password);
		Task AddUserToGroup(string userID, string groupID);
		Task AddUserToGroups(string userID, IEnumerable<string> groupsIDs);
	}
}