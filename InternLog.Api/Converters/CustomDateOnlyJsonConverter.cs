using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace InternLog.Api.Converters
{
	public class CustomDateOnlyJsonConverter : JsonConverter<DateOnly>
	{
		public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			if (DateOnly.TryParse(reader.GetString()!, out var dateOnly))
			{
				return dateOnly;
			}
			else if (DateTime.TryParse(reader.GetString()!, out var dateTime))
			{
				return DateOnly.FromDateTime(dateTime);
			}
			throw new FormatException("Cannot read DateOnly from string");
		}

		public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
		{
			var isoDate = value.ToString("O");
			writer.WriteStringValue(isoDate);
		}
	}
}