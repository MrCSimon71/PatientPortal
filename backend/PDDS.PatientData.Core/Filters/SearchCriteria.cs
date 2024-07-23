using Microsoft.Extensions.Primitives;

namespace PDDS.PatientData.Core.Filters
{
    public class SearchCriteria
    {
        public string FieldName { get; set; } = string.Empty;
        public string LookupValue { get; set; } = string.Empty;
        public string ComparisonOperator { get; set; } = "eq";

        public SearchCriteria(KeyValuePair<string, StringValues> queryParam)
        {
            var keyParts = queryParam.Key.Split('.');
            this.FieldName = keyParts[0];

            if (keyParts.Length > 1)
            {
                this.ComparisonOperator = keyParts[1];
            }
        }
    }
}
