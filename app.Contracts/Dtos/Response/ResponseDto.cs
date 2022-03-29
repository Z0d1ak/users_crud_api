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
    public class ResponseDto
    {
        [XmlAttribute(AttributeName = "ErrorId", Namespace = "")]
        [JsonIgnore]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public int ErrorCodeIdXml
        {
            get => (int)ErrorCode;
            set => ErrorCode = (ErrorCodeDto)value;
        }

        [XmlIgnore]
        [JsonPropertyName("ErrorId")]
        public ErrorCodeDto ErrorCode { get; set; }

        [XmlAttribute(AttributeName = "Success", Namespace = "")]
        [JsonPropertyName("Success")]
        public bool Success { get; set; } = true;
    }
}
