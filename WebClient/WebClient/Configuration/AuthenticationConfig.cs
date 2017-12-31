using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient
{
	/// <summary>
	/// Class to hold the Authentication Configuration settings
	/// </summary>
	public class AuthenticationConfig
	{
		public string Authority { get; set; }
		public string ClientId { get; set; }
		public bool RequireHttps { get; set; }
		public bool SaveTokens { get; set; }
	}
}
