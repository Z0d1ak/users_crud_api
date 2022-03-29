using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerArgs;

namespace ConsoleClient
{
	public class BasicArgs
	{
		[ArgRequired, ArgShortcut("-u"), ArgDescription("UserName")]
		public string Username { get; set; }

		[ArgRequired, ArgShortcut("-p"), ArgDescription("Password")]
		public string Password { get; set; }

		[ArgShortcut("-url"), ArgDescription("Basic Url")]
		public string BasicUrl { get; set; }
	}

	public class CreateUserArgs : BasicArgs
	{
		[ArgRequired, ArgShortcut("-id"), ArgDescription("UserId")]
		public int UserId { get; set; }
	
		[ArgRequired, ArgShortcut("-n"), ArgDescription("Name")]
		public string Name { get; set; }
	
		[ArgRequired, ArgShortcut("-s"), ArgDescription("Status")]
		public string Status { get; set; }
	}
	
	//public class DeleteUserArgs : BasicArgs
	//{
	//	[ArgRequired, ArgShortcut("-did"), ArgDescription("UserId")]
	//	public int DeleteUseId { get; set; }
	//}
	//
	//public class GetUserInfoUserArgs
	//{
	//	[ArgRequired, ArgShortcut("-gid"), ArgDescription("UserId")]
	//	public int Id { get; set; }
	//
	//	[ArgShortcut("-url"), ArgDescription("Basic Url")]
	//	public string url { get; set; }
	//}
	//
	//public class ChangeStatusArgs : BasicArgs
	//{
	//	[ArgRequired, ArgShortcut("-cid"), ArgDescription("UserId")]
	//	public int Id { get; set; }
	//
	//	[ArgRequired, ArgShortcut("-s"), ArgDescription("Status")]
	//	public string Status { get; set; }
	//
	//}
}