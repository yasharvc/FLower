﻿using Core.Models.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
	public interface IMenuService : ICRUDService<Menu>
	{
		Task<IEnumerable<Menu>> GetMenusByIDs(IEnumerable<string> ids);
	}
}