using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
	public class StageService : IStageService
	{
		IStageRepository StageRepository { get; set; }
		public StageService(IStageRepository stageRepository)
		{
			StageRepository = stageRepository;
		}
		public async Task Create(Stage entity) => await StageRepository.Create(entity);

		public async Task Delete(Stage entity) => await StageRepository.Delete(entity);

		public async Task<Stage> GetStageByTraceID(string traceID) => 
			await Query()

		public async Task<GenericQueryResult<Stage>> Query(List<string> fieldsList, Dictionary<string, bool> sort, List<Comparison> conditions, int page, int sizeInPage) => await StageRepository.Query(fieldsList, sort, conditions, page, sizeInPage);

		public async Task Update(Stage entity)
		{
			throw new NotImplementedException();
		}
	}
}
