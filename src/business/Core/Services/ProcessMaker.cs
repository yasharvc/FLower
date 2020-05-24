using Core.Enums;
using Core.Interfaces;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
	public class ProcessMaker : IProcessMaker
	{
		public ProcessResult Process { get; set; }

		public Task AddStage(Stage whichStage, Stage stage)
		{
			throw new NotImplementedException();
		}

		public Task<bool> ChangeStageStaus(Stage stage, StatusEnum status)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Stage> GetNextStages(Stage from)
		{
			throw new NotImplementedException();
		}

		public Stage GetStartStage()
		{
			throw new NotImplementedException();
		}

		public Task SetEnd(Stage stage)
		{
			throw new NotImplementedException();
		}

		public Task SetStart(Stage stage)
		{
			throw new NotImplementedException();
		}
	}
}
