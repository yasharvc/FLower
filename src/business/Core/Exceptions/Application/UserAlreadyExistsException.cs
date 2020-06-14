using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions.Application
{
	public class UserAlreadyExistsException : ApplicationException
	{
		public UserAlreadyExistsException():base("")
		{

		}

		public override int Code => 1;
	}
}
