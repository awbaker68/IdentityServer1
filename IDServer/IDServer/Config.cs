using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;

namespace IDServer
{
	/// <summary>
	/// Identity server in memory configuration details
	/// </summary>
	public class Config
	{
		#region Identity
		/// <summary>
		/// Get the list of configured identity resources
		/// </summary>
		/// <returns></returns>
		public static List<IdentityResource> GetIdentityResources()
		{
			return new List<IdentityResource>
				{
					new IdentityResources.OpenId(),
					new IdentityResources.Profile()
				};
		}
		#endregion

		#region API Resources
		/// <summary>
		/// Get the list of API Resources
		/// </summary>
		/// <returns>The list of resources</returns>
		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
					{
						new ApiResource("api1", "My API")
					};
		}
		#endregion

		#region Clients
		/// <summary>
		/// Get the list of allowable clients
		/// </summary>
		/// <returns>The list of client details</returns>
		public static IEnumerable<Client> GetClients()
		{
			return new List<Client>
					{
						new Client
						{
							ClientName = "MVC Client",
							ClientId = "mvcclient",
							AllowedGrantTypes = GrantTypes.Implicit,
							RedirectUris = new List<string> { "http://localhost:5001/signin-oidc" },
							PostLogoutRedirectUris = new List<string> { "http://localhost:5001/signout-callback-oidc" },

							RequireConsent = false,

							ClientSecrets =
							{
								new Secret("secret".Sha256())
							},
							AllowedScopes = new List<string>
							 {
								IdentityServerConstants.StandardScopes.OpenId,
								IdentityServerConstants.StandardScopes.Profile,
								IdentityServerConstants.StandardScopes.Email
							 }
						},
						new Client
						{
							ClientName = "API Client",
							ClientId = "apiclient",
							AllowedGrantTypes = GrantTypes.ClientCredentials,
							ClientSecrets = { new Secret("secret".Sha256()) },
							AllowedScopes = { "api1"}
						}
					};
		}
		#endregion
	}
}
