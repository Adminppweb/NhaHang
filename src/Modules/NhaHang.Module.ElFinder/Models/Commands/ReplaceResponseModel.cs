﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NhaHang.Module.ElFinder.Models.Commands
{
    public class ReplaceResponseModel
    {
        public ReplaceResponseModel()
        {
            Added = new List<object>();
            Removed = new List<string>();
        }

        [JsonPropertyName("added")]
        public List<object> Added { get; private set; }

        [JsonPropertyName("removed")]
        public List<string> Removed { get; private set; }
    }
}