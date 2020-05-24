using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
	public interface ICUDRepository<T> where T: Model
	{
		Task Create(T entity);
		Task Update(T entity);
		Task Delete(T entity);
	}
}
