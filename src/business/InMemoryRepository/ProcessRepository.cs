using Core.Interfaces.Repositories;
using Core.Models;

namespace InMemoryRepository
{
	public class ProcessRepository : InMemoryRepository<Process> , IProcessRepository
	{
	}
}