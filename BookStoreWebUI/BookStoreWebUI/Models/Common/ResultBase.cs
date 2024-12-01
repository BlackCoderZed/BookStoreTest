using Newtonsoft.Json;

namespace BookStoreWebUI.Models.Common
{
    public class ResultBase
    {
        [JsonProperty("result")]
        public Result Result { get; set; }
    }
}