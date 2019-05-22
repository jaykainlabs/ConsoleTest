using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KainLabs.Siren
{
    public class SubEntityJsonConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var token = JObject.Load(reader);
            if (token == null) return null;
            var hrefToken = token["href"];

            if (hrefToken != null)
            {
                var lse = new LinkedSubEntity();
                using (var sr = CopyReaderForObject(reader, token))
                {
                    // Using "populate" avoids infinite recursion.
                    serializer.Populate(sr, lse);
                }
            }
            var ese = new EmbeddedSubEntity();
            using (var sr = CopyReaderForObject(reader, token))
            {
                serializer.Populate(sr, ese);
                return ese;
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(SubEntity).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        public static JsonReader CopyReaderForObject(JsonReader reader, JToken jt)
        {
            JsonReader jObjectReader = jt.CreateReader();
            jObjectReader.Culture = reader.Culture;
            jObjectReader.DateFormatString = reader.DateFormatString;
            jObjectReader.DateParseHandling = reader.DateParseHandling;
            jObjectReader.DateTimeZoneHandling = reader.DateTimeZoneHandling;
            jObjectReader.FloatParseHandling = reader.FloatParseHandling;
            jObjectReader.MaxDepth = reader.MaxDepth;
            jObjectReader.SupportMultipleContent = reader.SupportMultipleContent;
            return jObjectReader;
        }
    }
}
