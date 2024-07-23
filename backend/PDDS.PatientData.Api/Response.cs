using Newtonsoft.Json;

namespace PDDS.PatientData.Api
{
    public class Response<T>
    {
        [JsonProperty(Order = -2)]
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }

        public Response() {}

        public Response(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
    }
}
