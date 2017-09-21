using Newtonsoft.Json;

namespace XgagWebsite.AjaxResponses
{
    public class GenericOperationResponse<TResponse>
    {
        [JsonProperty(PropertyName = "isSuccess")]
        public bool IsSuccess { get; set; } = true;

        [JsonProperty(PropertyName = "message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "response")]
        public TResponse Response { get; set; }

        public GenericOperationResponse()
        {
        }

        public GenericOperationResponse(TResponse response)
        {
            Response = response;
        }
    }

    public class GenericOperationResponse : GenericOperationResponse<object>
    {
    }
}