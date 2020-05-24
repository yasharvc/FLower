using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Core.Services
{
	public class ProcessService : IProcessService
	{
		IProcessRepository ProcessRepository { get; set; }
		public ProcessService(IProcessRepository processRepository)
		{
			ProcessRepository = processRepository;
		}
	}
}