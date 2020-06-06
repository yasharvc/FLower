using Core.Models;
using Core.Models.SystemModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDBRepository
{


	public class Test : Model
	{
		public string Name { get; set; }
	}
	public class TestRepository : BaseMongoRepository<Test>
	{
		protected override string CollectionName => "test";
		public TestRepository(DatabaseSettings databaseSettings) : base(databaseSettings)
		{

		}
	}
}