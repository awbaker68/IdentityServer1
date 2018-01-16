using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		/// <summary>
		/// Display the about information
		/// </summary>
		public IActionResult About()
        {
            ViewData["Message"] = "Identity Server Test Web Client.";
            return View();
        }

		/// <summary>
		/// Display the contact information
		/// </summary>
		[Authorize]
		public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Login the user
        /// </summary>
        [Authorize]
        public IActionResult Login()
		{
			return Challenge(new AuthenticationProperties { RedirectUri = "/Home/Index" }, "oidc");
		}
        /// <summary>
        /// Logout the user
        /// </summary>
        [Authorize]
        public IActionResult Logout()
		{
            return SignOut(new AuthenticationProperties { RedirectUri = "/Home/Index"}, "Cookies", "oidc");
		}
        /// <summary>
        /// Register a user
        /// </summary>
        public IActionResult Register()
        {
            return Redirect("http://localhost:5000/account/register");
        }
        /// <summary>
        /// Manage the current user
        /// </summary>
        [Authorize]
        public IActionResult Manage()
        {
            return Redirect("http://localhost:5000/manage/index");
        }

        /// <summary>
        /// Example action to call a web API method
        /// </summary>
        [Authorize]
		public async Task<JsonResult> CallAPI()
		{
			// Get client credentials with Access Token
			var tokenClient = new TokenClient("http://localhost:5000/connect/token", "apiclient", "secret");
			var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

			// Call the API using the Access Token received
			var client = new HttpClient();
			client.SetBearerToken(tokenResponse.AccessToken);
			var content = await client.GetStringAsync("http://localhost:5002/identity");            // API Client

			return Json(JArray.Parse(content));
		}

		/// <summary>
		/// Error details
		/// </summary>
		public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
