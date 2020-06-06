using Core.Interfaces.Repositories;
using Core.Models;
using Core.Models.SystemModels;

namespace MongoDBRepository
{
	public class GroupRepository : BaseMongoRepository<Group>, IGroupRepository
	{
		protected override string CollectionName => "Groups";

		public GroupRepository(DatabaseSettings databaseSettings) : base(databaseSettings)
		{

		}
	}
}