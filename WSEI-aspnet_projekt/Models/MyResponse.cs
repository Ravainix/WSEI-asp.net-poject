using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSEI_aspnet_projekt.Models
{
	public class MyResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }

		public MyResponse(bool success, string message)
		{
			this.Success = success;
			this.Message = message;
		}

		public MyResponse(bool success)
		{
			this.Success = success;
		}

		public bool IsSuccess()
		{
			return Success;
		}
		public bool IsFailed()
		{
			return !Success;
		}
	}
}
