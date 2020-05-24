using Core.Interfaces.Repositories;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTests.InMemoryRepositoryTests
{
	public class Tests : BaseTest
	{
		[Fact]
		public async void ShouldFilterByUniqueID()
		{
			var repo = GetService<IStageRepository>();
			var stage = new Stage
			{
				Name = "Test",
				Previous = null,
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
	}
}
