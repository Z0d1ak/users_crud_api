using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace app.Contracts.Dtos
{
    public sealed class RemoveUserDto
    {
        [XmlElement(ElementName = "Id", Namespace = "")]
        [JsonPropertyName("Id")]
        public int Id { get; set; }
    }
}
