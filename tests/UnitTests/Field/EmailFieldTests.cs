using Core.Models.Fields;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace UnitTests.Field
{
	public class EmailFieldTests
	{
		[Fact]
		public void ShouldReturnFalseForInvalidEmailAddress()
		{
			var email = new EmailField("email","");

			Assert.False(email.IsValid("123"));
		}
		[Fact]
		public void ShouldReturnFalseForEmailWithoutDomainAddress()
		{
			var email = new EmailField("email", "");

			Assert.False(email.IsValid("yashar@"));
		}
		[Fact]
		public void ShouldReturnFalseForEmailWithoutDomainExtenstionAddress()
		{
			var email = new EmailField("email", "");

			Assert.False(email.IsValid("yashar@gmail."));
		}
		[Fact]
		public void ShouldReturnTrueForValidEmailAddress()
		{
			var email = new EmailField("email", "");

			Assert.True(email.IsValid("yashar@gmail.com"));
		}
	}
}
