using Core.Enums;

namespace Core.Models.Fields
{
	public class IntegerField:Field
	{
		public int Min
		{
			get => GetPropertyValue<int>(nameof(Min));
			set => SetPropertyValue(nameof(Min), value, FieldTypeEnum.Integer);
		}
		public int Max
		{
			get => GetPropertyValue<int>(nameof(Max));
			set => SetPropertyValue(nameof(Max), value, FieldTypeEnum.Integer);
		}

		public IntegerField(string name, string title) : this(name, title, int.MinValue, int.MaxValue) { }
		public IntegerField(string name, string title, int min) : this(name, title, min, int.MaxValue) { }
		public IntegerField(string name, string title, int min, int max)
			: base(name, title)
		{
			FieldType = FieldTypeEnum.Integer;
			Min = min;
			Max = max;
		}
		public override bool IsValid(object obj)
		{
			if (!base.IsValid(obj))
				return false;
			var value = Convert(obj);
			return (int)value >= Min && (int)value <= Max;
		}
		public override object Default() => 0;
		public override object Convert(object v) => System.Convert.ToInt32(v);
	}
}
