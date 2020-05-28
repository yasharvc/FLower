using Core.Enums;
using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Core.Models.Fields
{
	public class EmailField : Field
	{
        public EmailField(string name, string title) : base(name, title) {
            FieldType = FieldTypeEnum.Email;
        }

		public override bool IsValid(object obj) => Regex.IsMatch((string)Convert(obj), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
		public override object Convert(object v) => System.Convert.ToString(v);
        public override object Default() => "";
	}
}
