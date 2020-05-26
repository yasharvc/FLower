using Core.Enums;
using Core.Interfaces;
using Core.Models;
using System;
using System.Threading.Tasks;

namespace Core.Services
{
	public class ProcessMaker : IProcessMaker
	{
		public Process Process { get; set; }

		public Task AddStage(Stage stage, StatusEnum previousStageResult = StatusEnum.Complete)
		{
			throw new NotImplementedException();
		}

		public Task ChangeData(DataStructure newStructure)
		{
			throw new NotImplementedException();
		}

		public Task SetStatus(StatusEnum status)
		{
			throw new NotImplementedException();
		}
	}
}
