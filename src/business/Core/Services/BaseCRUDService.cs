using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Services
{
	public abstract class BaseCRUDService<T> : ICRUDService<T> where T: Model
	{
		protected ICRUDRepository<T> Repository { get; set; }
		internal BaseCRUDService(ICRUDRepository<T> repository) => Repository = repository;
		public async Task Create(T entity) => await Repository.Create(entity);

		public async Task Delete(T entity) => await Repository.Delete(entity);

		public async Task<GenericQueryResult<T>> Query(List<string> fieldsList, Dictionary<string, bool> sort, List<Comparison> conditions, int page, int sizeInPage) => await Repository.Query(fieldsList, sort, conditions, page, sizeInPage);

		public async Task Update(T entity) => await Repository.Update(entity);
	}
}