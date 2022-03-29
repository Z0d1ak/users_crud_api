using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace app.Contracts.Dtos
{
    [XmlRoot(ElementName = "Response", Namespace = "")]
    public class EmptyResposeDto : ResponseDto
    {
        [XmlElement(ElementName = "Msg", Namespace = "")]
        [JsonPropertyName("Msg")]
        public string Message { get; set; }
    }
}
