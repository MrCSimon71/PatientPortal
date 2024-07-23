using PDDS.PatientData.Api.Helpers;
using Newtonsoft.Json;

namespace PDDS.PatientData.Api
{
    [JsonConverter(typeof(ResponseDataConverter))]
    public class PagedResponse<T> : Response<T>
    {
        public Meta Meta { get; set; }

        public PagedResponse(T data)
        {
            this.Data = data;
            this.Meta = new Meta();
            this.Message = string.Empty;
            this.Succeeded = true;
            this.Errors = null;
        }
    }

    public class Meta
    {
        public int Count { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }

        [JsonProperty(PropertyName = "_links")]
        public Links Links { get; set; }
    }

    public class Links
    {
        public string Self { get; set; } = string.Empty;
        public string First { get; set; } = string.Empty;
        public string? Prev { get; set; } = string.Empty;
        public string? Next { get; set; } = string.Empty;
        public string Last { get; set; } = string.Empty;
    }
}
