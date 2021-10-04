using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Invictus.Core
{
    public class DateTimeOffsetConverter : JsonConverter<DateTime>
    {
        //string formatacao = StringFormat
        public override DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                DateTime.ParseExact(reader.GetString(),
                     //"yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff''zzz", null);
                     "dd/MM/yyyy", null);
        // "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'-'%K'"
        // "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff''zzz", null);

        public override void Write(
            Utf8JsonWriter writer,
            DateTime dateTimeValue,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(dateTimeValue.ToString(
                    "yyyy'-'MM'-'dd'T'HH':'mm':'ss", null));
    }
}
