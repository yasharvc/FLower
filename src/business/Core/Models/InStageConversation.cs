using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models
{
	public class InStageConversation : Model
	{
		public string ProcessTraceID { get; set; }
		public string StageTraceID { get; set; }
		public string FromUserID { get; set; }
		//User must be priviliged
		public string ToUserID { get; set; }
		public DateTime CreationDateTime { get; set; }
		public string Conversation { get; set; }
	}
}
