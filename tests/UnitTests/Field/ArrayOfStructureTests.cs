using Core.Models.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace UnitTests.Field
{
	public class ArrayOfStructureTests
	{
		[Fact]
		public void ShouldAddValueSuccessfully()
		{
			var array = new ArrayOfStructureField("userinfo","User information");
			array.AddIntoStructure(new StringField("username", "User name"));
			array.AddIntoStructure(new IntegerField("timeout", "Timeout"));

			var element = new Dictionary<string, object>
			{
				{"username","yashar" },
				{"timeout",300 }
			};

			var errors = array.InsertValue(element);

			var lst = array.GetValues();

			Assert.NotEmpty(lst);
			Assert.Empty(errors);
			Assert.False(string.IsNullOrEmpty(lst.First()["username"].ToString()));
			Assert.True((int)lst.First()["timeout"] == 300);
		}

		[Fact]
		public void ShouldAddValueWithError()
		{
			var array = new ArrayOfStructureField("userinfo", "User information");
			array.AddIntoStructure(new StringField("username", "User name"));
			array.AddIntoStructure(new IntegerField("timeout", "Timeout"));

			var element = new Dictionary<string, object>
			{
				{"username","yashar" },
				{"timeout","test" }
			};

			var errors = array.InsertValue(element, false);

			var lst = array.GetValues();

			Assert.Empty(lst);
			Assert.NotEmpty(errors);
		}
	}
}
