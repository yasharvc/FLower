using Core.Interfaces.Repositories;
using Core.Models;
using Core.Models.SystemModels;

namespace MongoDBRepository
{
	public class UserRepository : BaseMongoRepository<User>, IUserRepository
	{
		public UserRepository(DatabaseSettings settings) : base(settings)
		{
		}

		protected override string CollectionName => "Users";

	}
}