using Core.Interfaces.Repositories;
using Core.Models.Security;
using Core.Models.SystemModels;

namespace MongoDBRepository
{
	public class RoleMenuRepository : BaseMongoRepository<RoleMenu>, IRoleMenuRepository
	{
		public RoleMenuRepository(DatabaseSettings settings) : base(settings)
		{
		}

		protected override string CollectionName => "RoleMenus";
	}
}