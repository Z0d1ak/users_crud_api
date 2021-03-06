using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using app.Contracts;
using app.Contracts.Dtos;
using Newtonsoft.Json.Linq;
using PowerArgs;
using static System.Net.WebRequestMethods;

namespace ConsoleClient
{
    [ArgExceptionBehavior(ArgExceptionPolicy.StandardExceptionHandling)]
	public class Methods
    {
		private const string localHostUrl ="https://localhost:44372";

		[HelpHook, ArgShortcut("-h"), ArgDescription("Shows this help")]
        public bool Help { get; set; }


		[ArgActionMethod, ArgShortcut("-cu"), ArgDescription("(-cu) Create user")]
		public void CreateUser(CreateUserArgs args)
		{
			var client = new HttpClient();
			client.BaseAddress = new Uri(args.BasicUrl ?? localHostUrl);
			var dto = new UserRequestDto
			{
				User = new UserDto
				{
					Id = args.UserId,
					Name = args.Name,
					StatusXml = args.Status,
				},
			};

			try
			{
				var httpContent = new StringContent(SerializeObject(dto), Encoding.UTF8);
				httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/xml");
				var encoding = Encoding.GetEncoding("iso-8859-1");
				var byteArray = encoding.GetBytes(args.Username + ":" + args.Password);
				client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));


				var result = client.PostAsync("/CreateUser", httpContent).GetAwaiter().GetResult();
				var xmldoc = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
				XDocument doc = XDocument.Parse(xmldoc);
				Console.Write(doc.ToString());
			}
            catch
            {
				Console.Write("Request failed. Try again later.");
			}
		}

		//[ArgActionMethod, ArgShortcut("-ru"), ArgDescription("(-ru) Remove user")]
		//public void RemoveUser(DeleteUserArgs args)
		//{
		//	
		//}
		//
		//[ArgActionMethod, ArgShortcut("-ss"), ArgDescription("(-ru) Set status")]
		//public void SetStatus(ChangeStatusArgs args)
		//{
		//
		//}
		//
		//[ArgActionMethod, ArgShortcut("-ui"), ArgDescription("(-ru) GetUserInfo")]
		//public void UserInfo(GetUserInfoUserArgs args)
		//{
		//
		//}
		//
		private static string SerializeObject<T>(T toSerialize)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(toSerialize.GetType());
			using (Utf8StringWriter textWriter = new Utf8StringWriter())
			{
				xmlSerializer.Serialize(textWriter, toSerialize);
				return textWriter.ToString();
			}
		}
	}

	public sealed class Utf8StringWriter : StringWriter
	{
		public override Encoding Encoding => Encoding.UTF8;
	}
}
