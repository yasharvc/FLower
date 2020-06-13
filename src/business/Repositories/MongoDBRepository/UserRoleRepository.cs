using Core.Interfaces.Repositories;
using Core.Models.Security;
using Core.Models.SystemModels;

namespace MongoDBRepository
{
	public class UserRoleRepository : BaseMongoRepository<UserRole>, IUserRoleRepository
	{
		public UserRoleRepository(DatabaseSettings settings) : base(settings)
		{
		}

		protected override string CollectionName => "UsersRoles";
	}
}