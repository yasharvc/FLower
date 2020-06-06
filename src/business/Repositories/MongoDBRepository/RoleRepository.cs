using Core.Interfaces.Repositories;
using Core.Models.Security;
using Core.Models.SystemModels;

namespace MongoDBRepository
{
	public class RoleRepository : BaseMongoRepository<Role>, IRoleRepository
	{
		public RoleRepository(DatabaseSettings settings) : base(settings) { }

		protected override string CollectionName => "Roles";
	}
}