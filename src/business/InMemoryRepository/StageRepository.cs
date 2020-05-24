using Core.Interfaces.Repositories;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InMemoryRepository
{
	public class StageRepository : InMemoryRepository<Stage>, IStageRepository
	{
		
	}
}
