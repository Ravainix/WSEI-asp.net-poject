using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSEI_aspnet_projekt.Models
{
	public class MyResponse
	{
		public bool _success { get; set; }
		public string _message { get; set; }

		public MyResponse(bool success, string message)
		{
			_success = success;
			_message = message;
		}

		public MyResponse(bool success)
		{
			_success = success;
		}

		public bool isSuccess()
		{
			return _success;
		}
		public bool isFailed()
		{
			return !_success;
		}
	}
}
