using Core.Models.Fields;
using Xunit;

namespace UnitTests.Field
{
	public class EuroFieldTests
	{
		[Fact]
		public void ShouldSupressTheextraNumbers()
		{
			var value = "1.123";
			var field = new EuroField("","");

			Assert.Equal(1.12m, field.Convert(value));
		}
	}
}