using Core.Enums;
using Core.Interfaces.Models;
using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace Core.Models
{
	public class Process : Model, IName, IUniqueID
	{
		public string Name { get; set; }
		public string UniqueID { get; set; }
		public List<Stage> Stages { get; set; }
		public DateAndTime CreationDateTime { get; set; }
		public string CreatorID { get; set; }
		public DateAndTime LastModifiedDateTime { get; set; }
		public string ModifierID { get; set; }
		public Privileges AccessPrivileges { get; set; }
		public DataStructure Data { get; set; }
		public List<DataHistory> DataHistories { get; set; }
		public ManipulationPrivileges DataPrivileges { get; set; }
		public StatusEnum Status { get; set; }
	}
}