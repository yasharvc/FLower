using Core.Interfaces.Repositories;
using Core.Models.Security;
using Core.Models.SystemModels;

namespace MongoDBRepository
{
	public class MenuRepository : BaseMongoRepository<Menu>, IMenuRepository
	{
		public MenuRepository(DatabaseSettings settings) : base(settings) { }

		protected override string CollectionName => "Menus";
	}
}