using Core.Enums;
using Microsoft.VisualBasic;

namespace Core.Models
{
	public class DataHistory
	{
		public string UserID { get; set; }
		public DataStructure Data { get; set; }
		public DateAndTime ArchivedDateTime { get; set; }
		public DataManipulationEnum ActionTake { get; set; }
	}
}