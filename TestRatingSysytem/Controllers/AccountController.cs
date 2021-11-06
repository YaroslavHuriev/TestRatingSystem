using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

using TestRatingSystem.Authentication;
using TestRatingSystem.Models;

namespace TestRatingSystem.Controllers {
	[Route("Account/[action]")]
	[ApiController]
	public class AccountController : ControllerBase {

		[HttpPost("/token")]
		public IActionResult Token(string username, string password) {
			var identity = GetIdentity(username, password);
			if (identity == null) {
				return BadRequest(new { errorText = "Invalid username or password." });
			}

			var now = DateTime.UtcNow;
			// создаем JWT-токен
			var jwt = new JwtSecurityToken(
					issuer: AuthOptions.ISSUER,
					audience: AuthOptions.AUDIENCE,
					notBefore: now,
					claims: identity.Claims,
					expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
					signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			var response = new {
				access_token = encodedJwt,
				username = identity.Name
			};

			return new JsonResult(response);
		}

		private ClaimsIdentity GetIdentity(string username, string password) {
			string jsonString = System.IO.File.ReadAllText("Authentication/usernamesAndPasswords.json");
			AdminLoginData admin = JsonSerializer.Deserialize<AdminLoginData>(jsonString);
			if (admin.Username == username && admin.Password == password) {
				var claims = new List<Claim>
				{
					new Claim(ClaimsIdentity.DefaultNameClaimType, admin.Username),
					new Claim(ClaimsIdentity.DefaultRoleClaimType, admin.Role)
				};
				ClaimsIdentity claimsIdentity =
				new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
					ClaimsIdentity.DefaultRoleClaimType);
				return claimsIdentity;
			}

			// если пользователя не найдено
			return null;
		}
	}
}
