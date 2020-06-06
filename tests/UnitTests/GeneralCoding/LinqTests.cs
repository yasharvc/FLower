using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Xunit;

namespace UnitTests.GeneralCoding
{
	public class LinqTests
	{
		class TestClass
		{
			public int ID { get; set; }
			public string Name { get; set; }
		}
		[Fact]
		public void ShouldSelectIDByStringOfIt()
		{
			var list = new List<TestClass>
			{
				new TestClass
				{
					ID=1,
					Name="Yashar"
				},
				new TestClass
				{
					ID=200,
					Name="Test"
				}
			};

			var propertyName = nameof(TestClass.Name);
			var propertyID = nameof(TestClass.ID);

			var testClass = Expression.Parameter(typeof(TestClass), "testClass");
			var nameSelection = Expression.PropertyOrField(testClass, propertyName);
			//var idSelection = Expression.PropertyOrField(testClass, propertyID);
			////var test = Expression.Property(nameSelection, idSelection);
			var lambda = Expression.Lambda<Func<TestClass,string>>(nameSelection, testClass);

			var res = list
				.Where(m => m.ID > 0)
				.OrderByDescending(lambda.Compile())
				.Select(m => Fx(m, propertyName, propertyID));

			Assert.NotNull(res.First().Name);
		}

		private dynamic Fx(TestClass m,params string[] props)
		{
			dynamic myobject = new ExpandoObject();

			IDictionary<string, object> myUnderlyingObject = myobject;

			foreach (var item in props)
			{
				myUnderlyingObject[item] = m.GetType().GetProperty(item).GetValue(m);
			}
			return myobject;
		}
	}
}
