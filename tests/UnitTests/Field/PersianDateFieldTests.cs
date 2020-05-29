using Core.Models.Fields;
using System;
using Xunit;

namespace UnitTests.Field
{
	public class PersianDateFieldTests
	{
		[Fact]
		public void ShouldConvertIntoDateTime()
		{
			var persianDateStr = "1399/03/9";
			var persianDateField = new PersianDateField("","");

			var date = persianDateField.Convert(persianDateStr);

			Assert.True(date is DateTime);
		}
	}
}
