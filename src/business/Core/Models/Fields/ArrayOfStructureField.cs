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

		public IDictionary<string,string> UpdateValue(int index,IDictionary<string, object> value,out bool isSomethingUpdated,bool updateWithError = true)
		{
			isSomethingUpdated = false;
			var updatedItem = CastIntoStructure(value, out Dictionary<string, string> errors, Data[index]);
			if (updateWithError || errors.Count == 0)
			{
				foreach (var item in value)
				{
					if (!item.Value.Equals(Data[index][item.Key]))
					{
						isSomethingUpdated = true;
						break;
					}
				}
				Data[index] = updatedItem;
			}
			return errors;
		}

		public bool DeleteValue(int index)
		{
			if (Data.Count > index)
			{
				Data.RemoveAt(index);
				return true;
			}
			return false;
		}

		public IEnumerable<IDictionary<string, object>> GetValues() => Data;

		private Dictionary<string, object> CastIntoStructure(IDictionary<string, object> value, out Dictionary<string, string> errors, IDictionary<string,object> baseItem = null)
		{
			errors = new Dictionary<string, string>();
			var newItem = AddItemsByStructureValidation(value, errors, baseItem);
			return newItem;
		}

		private Dictionary<string, object> AddItemsByStructureValidation(IDictionary<string, object> value, Dictionary<string, string> errors, IDictionary<string, object> baseItem)
		{
			var newItem = new Dictionary<string, object>();
			foreach (var item in SubFields)
			{
				if (value.ContainsKey(item.Name))
				{
					if (item.IsValid(value[item.Name]))
						newItem[item.Name] = item.Convert(value[item.Name]);
					else
						errors[item.Name] = $"'{value[item.Name]}' is in wrong format for '{item.Name}' field with `{item.GetType().Name}` type.";
				}
				else
				{
					newItem[item.Name] = baseItem == null || !baseItem.ContainsKey(item.Name) ? item.Default() : baseItem[item.Name];
				}
			}
			return newItem;
		}
	}
}
