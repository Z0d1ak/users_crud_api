using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace app.Contracts.Dtos
{
    [XmlRoot(ElementName = "Response", Namespace = "")]
    public class ErrorResponseDto : ResponseDto
    {
        [XmlElement(ElementName = "ErrorMsg", Namespace = "")]
        [JsonPropertyName("Msg")]
        public string ErrorMessage { get; set; }
    }
}
