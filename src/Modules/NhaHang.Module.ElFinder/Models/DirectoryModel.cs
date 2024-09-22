using System.Text.Json.Serialization;

namespace NhaHang.Module.ElFinder.Models
{
    public class DirectoryModel : BaseModel
    {
        /// <summary>
        ///  Indicates the volume Id.
        /// </summary>
        //[JsonPropertyName("volumeId")]
        [JsonPropertyName("volumeid")] // https://github.com/Studio-42/NhaHang.Module.ElFinder/issues/2403
        public string VolumeId { get; set; }

        /// <summary>
        ///  Hash of parent directory. Required except roots dirs.
        /// </summary>
        [JsonPropertyName("phash")]
        public string ParentHash { get; set; }

        /// <summary>
        /// Is directory contains subfolders
        /// </summary>
        [JsonPropertyName("dirs")]
        public byte Dirs { get; set; }
    }
}