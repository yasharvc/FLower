﻿using Core.Interfaces.Models;

namespace Core.Models
{
	public abstract class Model : IStringGUID
	{
		public string ID { get; set; }
	}
}