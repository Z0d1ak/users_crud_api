using System;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace app.Contracts.Dtos
{
    public class UserDto
    {
        [XmlAttribute(AttributeName = "Id", Namespace = "")]
        [JsonPropertyName("Id")]
        public int Id { get; set; }

        [XmlAttribute(AttributeName = "Name", Namespace = "")]
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [XmlElement(ElementName = "Status", Namespace = "")]
        [JsonIgnore]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public string StatusXml
        {
            get => Status.ToString();
            set => Status = (StatusDto)Enum.Parse(typeof(StatusDto), value);
        }

        [XmlIgnore]
        [JsonPropertyName("Status")]
        public StatusDto Status { get; set; }
    }
}
