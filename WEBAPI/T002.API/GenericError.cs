using Newtonsoft.Json;

namespace T002.API.T002.API
{
    public class GenericError
    {
        public int StatusCode { get; set; }
        public string ErrorType { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
