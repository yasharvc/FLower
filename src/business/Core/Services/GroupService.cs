using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
	public class GroupService : BaseCRUDService<Group>, IGroupService
	{
		public GroupService(IGroupRepository repository) : base(repository) { }

		public async Task<Group> GetGroupByID(string groupID) => await Repository.GetByID(groupID);

		public async Task<List<Group>> GetGroupsByIDs(IEnumerable<string> groupsIDs)
		{
			var res = new List<Group>();
			foreach (var item in groupsIDs)
			{
				res.Add(await GetGroupByID(item));
			}
			return res;
		}
	}
}
