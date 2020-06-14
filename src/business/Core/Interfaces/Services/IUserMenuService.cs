using Core.Models.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
	public interface IUserMenuService
	{
		IAsyncEnumerable<Menu> GetUserMenu(string userID);
	}
}