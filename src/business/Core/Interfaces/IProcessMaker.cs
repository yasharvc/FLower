using Core.Enums;
using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
	public interface IProcessMaker
	{
		Task AddStage(Stage whichStage, Stage stage);
		IEnumerable<Stage> GetNextStages(Stage from);
		Stage GetStartStage();
		Task<bool> ChangeStageStaus(Stage stage,StatusEnum status);
	}
}