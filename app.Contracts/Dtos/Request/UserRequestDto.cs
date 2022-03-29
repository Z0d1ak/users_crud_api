using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace app.Contracts.Dtos
{
    [XmlRoot(ElementName = "Request")]
    public class UserRequestDto
    {
        [XmlElement(ElementName = "user")]
        [JsonPropertyName("user")]
        public UserDto User { get; set; }
    }
}
