using System.Text.Json.Serialization;

namespace NhaHang.Module.ElFinder.Models
{
    public class RootModel : DirectoryModel
    {
        [JsonPropertyName("isroot")]
        public byte IsRoot => 1;
    }
}