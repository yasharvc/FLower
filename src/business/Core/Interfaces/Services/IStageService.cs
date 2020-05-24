using Core.Models;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
	public interface IStageService : ICRUDService<Stage>
	{
		Task<Stage> GetStageByTraceID(string traceID);
	}
}