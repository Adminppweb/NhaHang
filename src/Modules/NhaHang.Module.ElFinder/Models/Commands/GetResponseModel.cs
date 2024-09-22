using System.Text.Json.Serialization;

namespace NhaHang.Module.ElFinder.Models.Commands
{
    public class GetResponseModel
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}