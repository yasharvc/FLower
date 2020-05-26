using Core.Enums;
using Core.Models;
using System.Threading.Tasks;

namespace Core.Interfaces
{
	public interface IProcessMaker
	{
		Task AddStage(Stage stage, StatusEnum previousStageResult = StatusEnum.Complete);
		Task SetStatus(StatusEnum status);
		Task ChangeData(DataStructure newStructure);
	}
}