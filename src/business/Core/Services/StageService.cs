using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using System.Collections.Generic;
using System.Linq;
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

		public async Task<Stage> GetStageByTraceID(string traceID)
		{
			var list = await Query(new List<string>(), new Dictionary<string, bool>(), new List<Comparison>
			{
				new Comparison
				{
					ColumnType = Enums.BaseColumnType.String,
					FieldName = nameof(Stage.TraceID),
					Operation = Enums.ComparisonOperation.Equal,
					Values = new List<string> { traceID }
				}
			}, 1, 1);
			return list.Result.First();
		}

		public async Task<GenericQueryResult<Stage>> Query(List<string> fieldsList, Dictionary<string, bool> sort, List<Comparison> conditions, int page, int sizeInPage) => await StageRepository.Query(fieldsList, sort, conditions, page, sizeInPage);

		public async Task Update(Stage entity) => await StageRepository.Update(entity);
	}
}
