using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
	public interface IGroupService : ICRUDService<Group>
	{
		Task<List<Group>> GetGroupsByIDs(IEnumerable<string> groupsIDs);
		Task<Group> GetGroupByID(string groupID);
	}
}