using Core.Interfaces.Repositories;
using Core.Models;

namespace InMemoryRepository
{
	public class GroupRepository : InMemoryRepository<Group>, IGroupRepository
	{
	}
}