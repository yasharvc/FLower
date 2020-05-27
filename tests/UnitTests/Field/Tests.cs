using Core.Enums;
using Core.Models.Fields;
using System.Collections.Generic;
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

		[Fact]
		public void ShouldCreateArrayOfStructure()
		{
			var a = new Core.Models.Field();
			a.FieldType = FieldTypeEnum.ArrayOfStructure;
			a.Name = "UserInfo";
			//var lst = a.Properties as List<Core.Models.Field>;
			//lst.Add()
		}
	}
}