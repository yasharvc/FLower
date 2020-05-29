using System;
using System.Globalization;
using System.Linq;

namespace Core.Models.Fields
{
	public class PersianDateField : Field
	{
		public PersianDateField(string name, string title)
			: base(name, title)
		{
			FieldType = Enums.FieldTypeEnum.PersianDate;
		}
		public override bool IsValid(object obj) => base.IsValid(obj);
		public override object Default() => 0;
		public override object Convert(object v)
		{
			try
			{
				var date = System.Convert.ToString(v);
				PersianCalendar persianCalendar = new PersianCalendar();
				var dateParts = date.Split(new char[] { '/' }).Select(d => int.Parse(d)).ToArray();
				return persianCalendar.ToDateTime(dateParts[0], dateParts[1], dateParts[2], 0, 0, 0, 0);
			}
			catch
			{
				throw;
			}
		}
	}
}