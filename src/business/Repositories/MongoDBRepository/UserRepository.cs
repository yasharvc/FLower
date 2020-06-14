using Core.Exceptions.Application;
using Core.Interfaces.Repositories;
using Core.Models;
using Core.Models.SystemModels;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace MongoDBRepository
{
	public class UserRepository : BaseMongoRepository<User>, IUserRepository
	{
		public UserRepository(DatabaseSettings settings) : base(settings)
		{
		}

		protected override string CollectionName => "Users";

		public override async Task Create(User entry)
		{
			if(await Repository.Find(m=>m.Username == entry.Username).AnyAsync())
				throw new UserAlreadyExistsException();
			await base.Create(entry);
		}
	}
}