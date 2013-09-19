using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blinds.com.Helpers
{
    public class EsacapeSpecialCharConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            string result = value.ToString();
            if (!String.IsNullOrEmpty(result))
            {
                //single quote
                result = result.Replace("'", "\\u0027");
                //double quote
                result = result.Replace("\"", "\\u0022");
            }
            writer.WriteValue(result);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = JToken.Load(reader).Value<string>();

            if (!String.IsNullOrEmpty(value))
            {
                value = value.Replace("\\u0027", "'");
                value = value.Replace("\\u0022", "\"");
            }
            return value;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

    }
}