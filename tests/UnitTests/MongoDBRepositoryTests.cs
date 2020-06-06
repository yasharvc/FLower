using Core.Models.SystemModels;
using MongoDBRepository;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
	public class MongoDBRepositoryTests
	{
		[Fact]
		public async void ShouldInsertTest()
		{
			var repo = new TestRepository(new DatabaseSettings
			{
				ConnectionString="mongodb://localhost:27017/",
				DatabaseName="Flower"
			});

			List<Test> demo = new List<Test>{
				new Test
				{
					Name = "Test"
				},
				new Test
				{
					Name = "Yashar"
				},
				new Test
				{
					Name = "Ali"
				}
			};
			await repo.CreateMany(demo);

			Assert.Equal(3, repo.Count);
			var res = await repo.Query(
				new List<string> { "Name", "_id" },
				new Dictionary<string, bool>
				{
					{ "Name",true }
				},
				new List<Core.Models.Comparison>
				{
					new Core.Models.Comparison
					{
						ColumnType = Core.Enums.BaseColumnType.String,
						FieldName="Name",
						Operation = Core.Enums.ComparisonOperation.Equal,
						Values=new List<string>{"Ali"}
					}
				},
				1, 1);

			Assert.Equal(1, res.TotalRecordCount);

			foreach (var item in demo)
			{
				await repo.Delete(item);
			}
		}
	}
}
