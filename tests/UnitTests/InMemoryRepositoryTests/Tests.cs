using Core.Interfaces.Repositories;
using Core.Models;
using InMemoryRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTests.InMemoryRepositoryTests
{
	public class Tests : BaseTest
	{
		class TestClass : Model
		{
			public string Name { get; set; }
			public int Age { get; set; }
		}
		class repo : InMemoryRepository<TestClass>
		{

		}
		[Fact]
		public async void ShouldFilterByUniqueID()
		{
			var repo = GetService<IStageRepository>();
			var stage = new Stage
			{
				Name = "Test",
				Next = new List<Stage>(),
				UniqueID = Guid.NewGuid().ToString()
			};
			await repo.Create(stage);

			var res = await repo.Query(new List<string>(), new Dictionary<string, bool>(), new List<Core.Models.Comparison>
			{
				new Comparison
				{
					ColumnType = Core.Enums.BaseColumnType.String,
					FieldName=nameof(Stage.UniqueID),
					Operation= Core.Enums.ComparisonOperation.Equal,
					Values = new List<string>
					{
						stage.UniqueID
					}
				}
			}, 1, 1);

			Assert.NotNull(res);
			Assert.Equal(stage.UniqueID, res.Result.ElementAt(0).UniqueID);
		}

		[Fact]
		public async void ShouldSortByName()
		{
			var repo = new repo();
			await repo.Create(new TestClass
			{
				Name = "Yashar",
				Age = 34
			});
			await repo.Create(new TestClass
			{
				Name = "Zahra",
				Age = 34
			});

			var res = await repo.Query(new List<string> { "Name", "Age" },
				new Dictionary<string, bool>
				{
					{ "Name",false }
				}, new List<Comparison>(), 1, 1);
			Assert.Equal("Zahra", res.Result.First().Name);
		}
	}
}
