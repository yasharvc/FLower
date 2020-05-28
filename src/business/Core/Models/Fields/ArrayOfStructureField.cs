using Core.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Core.Models.Fields
{
	public class ArrayOfStructureField : Field
	{
		List<Field> Structure => SubFields as List<Field>;
		List<Dictionary<string, object>> Data => Value as List<Dictionary<string, object>>;

		public ArrayOfStructureField(string name, string title, params Field[] structureFileds)
		{
			Name = name;
			Title = title;
			FieldType = FieldTypeEnum.ArrayOfStructure;
			Value = new List<Dictionary<string, object>>();
			SubFields = new List<Field>();
			AddIntoStructure(structureFileds);
		}

		public void AddIntoStructure(params Field[] fields)
		{
			foreach (var field in fields)
			{
				if (!Structure.Any(m => m.Name.Equals(field.Name, StringComparison.OrdinalIgnoreCase)))
					Structure.Add(field);
				else
					throw new DuplicateNameException(field.Name);

			}
		}

		public IDictionary<string,string> InsertValue(IDictionary<string, object> value,bool insertWithError = true)
		{
			var newItem = CastIntoStructure(value, out Dictionary<string, string> errors);
			if (insertWithError || errors.Count == 0)
				Data.Add(newItem);
			return errors;
		}

		private Dictionary<string, object> CastIntoStructure(IDictionary<string, object> value, out Dictionary<string, string> errors)
		{
			var newItem = new Dictionary<string, object>();
			errors = new Dictionary<string, string>();
			foreach (var item in SubFields)
				newItem = AddItemsByStructureValidation(value, errors, item);
			return newItem;
		}

		private Dictionary<string, object> AddItemsByStructureValidation(IDictionary<string, object> value, Dictionary<string, string> errors, Field item)
		{
			var newItem = new Dictionary<string, object>();
			if (value.ContainsKey(item.Name))
			{
				if (item.IsValid(value[item.Name]))
					newItem[item.Name] = item.Convert(value[item.Name]);
				else
					errors[item.Name] = $"'{value[item.Name]}' is in wrong format for '{item.Name}' field with `{item.GetType().Name}` type.";
			}
			else
			{
				newItem[item.Name] = item.Default();
			}
			return newItem;
		}

		public IDictionary<string,string> UpdateValue(int index,IDictionary<string, object> value,bool updateWithError = true)
		{
			var updatedItem = CastIntoStructure(value, out Dictionary<string, string> errors);
			if (updateWithError || errors.Count == 0)
				Data[index] = updatedItem;
			return errors;
		}

		public IEnumerable<IDictionary<string, object>> GetValues() => Data;
	}
}
