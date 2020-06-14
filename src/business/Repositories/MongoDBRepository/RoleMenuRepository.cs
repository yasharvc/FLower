using Core.Interfaces.Repositories;
using Core.Models.Security;
using Core.Models.SystemModels;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDBRepository
{
	public class RoleMenuRepository : BaseMongoRepository<RoleMenu>, IRoleMenuRepository
	{
		public RoleMenuRepository(DatabaseSettings settings) : base(settings)
		{
		}

		protected override string CollectionName => "RoleMenus";

		public async Task<IEnumerable<RoleMenu>> GetByRoleID(string id) => await Repository.Find(m => m.RoleID == id).ToListAsync();
	}
}