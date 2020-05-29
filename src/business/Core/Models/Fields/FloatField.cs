using Core.Enums;

namespace Core.Models.Fields
{
	public class FloatField:Field
	{
		public float Min
		{
			get => GetPropertyValue<float>(nameof(Min));
			set => SetPropertyValue(nameof(Min), value, FieldTypeEnum.Float);
		}
		public float Max
		{
			get => GetPropertyValue<float>(nameof(Max));
			set => SetPropertyValue(nameof(Max), value, FieldTypeEnum.Float);
		}

		public FloatField(string name, string title) : this(name, title, float.MinValue, float.MaxValue) { }
		public FloatField(string name, string title, float min) : this(name, title, min, float.MaxValue) { }
		public FloatField(string name, string title, float min, float max)
			: base(name, title)
		{
			FieldType = FieldTypeEnum.Float;
			Min = min;
			Max = max;
		}
		public override bool IsValid(object obj)
		{
			if (!base.IsValid(obj))
				return false;
			var value = Convert(obj);
			return (float)value >= Min && (float)value <= Max;
		}
		public override object Default() => 0.0f;
		public override object Convert(object v) => (float)System.Convert.ToDouble(v);
	}
}
