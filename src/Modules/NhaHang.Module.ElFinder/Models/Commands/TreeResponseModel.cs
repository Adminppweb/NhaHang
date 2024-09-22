using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NhaHang.Module.ElFinder.Models.Commands
{
    public class TreeResponseModel
    {
        public TreeResponseModel()
        {
            Tree = new List<object>();
        }

        [JsonPropertyName("tree")]
        public List<object> Tree { get; private set; }
    }
}