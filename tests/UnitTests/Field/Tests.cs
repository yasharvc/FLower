using Core.Enums;
using Core.Models.Fields;
using Xunit;

namespace UnitTests.Field
{
	public class Tests
	{
		[Fact]
		public void ShouldCreateStringField()
		{
			var stringField = new StringField("Address","Address of customer");

			Assert.Equal(FieldTypeEnum.String, stringField.FieldType);
			Assert.Equal("Address", stringField.Name);
			Assert.Equal("Address of customer", stringField.Title);
			Assert.Equal(-1, stringField.Length);
		}
	}
}
