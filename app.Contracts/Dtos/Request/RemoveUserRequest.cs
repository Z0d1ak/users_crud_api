﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace app.Contracts.Dtos
{
    [XmlRoot(ElementName = "Request")]
    public sealed class RemoveUserRequest
    {
        [XmlElement(ElementName = "RemoveUser", Namespace = "")]
        [JsonPropertyName("RemoveUser")]
        public RemoveUserDto RemoveUserDto { get; set; }
    }
}
