using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;

namespace Core.Services
{
	public class GroupService : BaseCRUDService<Group>, IGroupService
	{
		public GroupService(IGroupRepository repository) : base(repository) { }
	}
}
