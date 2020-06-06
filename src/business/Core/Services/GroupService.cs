using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
	public class GroupService : IGroupService
	{
		IGroupRepository Repository { get; set; }
		public GroupService(IGroupRepository repository)
		{
			Repository
		}
		public Task Create(Group entity)
		{
			throw new NotImplementedException();
		}

		public Task Delete(Group entity)
		{
			throw new NotImplementedException();
		}

		public Task<GenericQueryResult<Group>> Query(List<string> fieldsList, Dictionary<string, bool> sort, List<Comparison> conditions, int page, int sizeInPage)
		{
			throw new NotImplementedException();
		}

		public Task Update(Group entity)
		{
			throw new NotImplementedException();
		}
	}
}
