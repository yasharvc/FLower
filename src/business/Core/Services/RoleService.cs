﻿using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
	public class RoleService : BaseCRUDService<Role>, IRoleService
	{
		public RoleService(IRoleRepository repository) : base(repository) { }

		public async Task<Role> GetRoleByID(string roleID) => await Repository.GetByID(roleID);

		public async Task<List<Role>> GetRolesByIDs(IEnumerable<string> ids)
		{
			var res = new List<Role>();
			foreach (var item in ids)
			{
				res.Add(await GetRoleByID(item));
			}
			return res;
		}
	}
}