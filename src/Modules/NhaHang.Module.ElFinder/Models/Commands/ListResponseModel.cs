﻿using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NhaHang.Module.ElFinder.Models.Commands
{
    public class ListResponseModel
    {
        public ListResponseModel()
        {
            List = new List<string>();
        }

        [JsonPropertyName("list")]
        public List<string> List { get; private set; }
    }
}