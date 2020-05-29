using Core.Models.Fields;
using System;
using Xunit;

namespace UnitTests.Field
{
	public class GregorianDateTimeTests
	{
		[Fact]
		public void ShouldCastIntoDateTime()
		{
			var dateTime = "2020/01/15";
			var field = new GregorianDateField("test", "");

			var converted = field.Convert(dateTime);

			Assert.True(converted is DateTime);
		}
	}
}