using PDDS.PatientData.Core.Entities;
using PDDS.PatientData.Core.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace PDDS.PatientData.Api.Helpers
{
    public class ResponseDataConverter : JsonConverter
    {
        private readonly JsonSerializer _camelCaseSerializer = JsonSerializer.Create(
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

        public override bool CanConvert(Type objectType)
        {
            return objectType.IsGenericType;// && objectType.GetGenericTypeDefinition() == typeof(PagedResponse<Customer>);
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            JObject jo = new JObject();
            Type type = value.GetType();

            foreach (PropertyInfo prop in type.GetProperties())
            {
                var jsonPropertyName = GetJsonPropertyName(prop);
                var propertyName = string.IsNullOrEmpty(jsonPropertyName) ? ToCamelCaseString(prop.Name) : jsonPropertyName;
                var propertyValue = prop.GetValue(value) ?? "";

                if (prop.PropertyType.Name.Contains("List") || prop.PropertyType.Namespace.Contains("Collections") || prop.PropertyType.Name.Contains("[]"))
                {
                    if (prop.Name == "Data")
                    {
                        propertyName = type.GenericTypeArguments[0].GenericTypeArguments[0].Name;

                        if (propertyName.EndsWith("Dto"))
                        {
                            propertyName = propertyName.Substring(0, propertyName.Length - 3);
                        }
                    }

                    if (propertyValue == null || propertyValue == "")
                    {
                        propertyValue = new List<string>();
                    }

                    propertyName = propertyName.Pluralize();

                    jo.Add(propertyName.ToLower(), JArray.FromObject(propertyValue, _camelCaseSerializer));
                }
                else if (prop.PropertyType.Name.StartsWith("Dictionary"))
                {
                    propertyValue = JsonConvert.SerializeObject(propertyValue, Formatting.Indented);
                    jo.Add(propertyName, new JValue(propertyValue));
                }
                else if (prop.PropertyType.IsClass)
                {
                    jo.Add(propertyName, JToken.FromObject(propertyValue, _camelCaseSerializer));
                }
                else
                {
                    jo.Add(propertyName, new JValue(propertyValue));
                }
            }

            jo.WriteTo(writer);
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private string GetJsonPropertyName(PropertyInfo property)
        {
            var jsonPropertyName = string.Empty;

            JsonPropertyAttribute jsonPropertyAttribute = (JsonPropertyAttribute)property.GetCustomAttributes(typeof(JsonPropertyAttribute), false).FirstOrDefault();

            if (jsonPropertyAttribute != null)
            {
                jsonPropertyName = jsonPropertyAttribute.PropertyName;
            }

            return jsonPropertyName;
        }

        private string ToCamelCaseString(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                return char.ToLowerInvariant(str[0]) + str.Substring(1);
            }

            return str;
        }

        private bool IsEnumerableType(Type type)
        {
            return (type.GetInterface(nameof(IEnumerable)) != null);
        }
    }
}
