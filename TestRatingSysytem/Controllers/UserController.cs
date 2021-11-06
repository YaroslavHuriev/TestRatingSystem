using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using TestRatingSystem.Models;

namespace TestRatingSystem.Controllers {
	[ApiController]
	[Route("User/[action]")]
	public class UserController : ControllerBase {

		UsersContext db;
		public UserController(UsersContext context) {
			db = context;
			if (!db.Users.Any()) {
				db.Users.Add(new User {
					Grade = 10,
					Email = "email@gmail.com",
					FullName = "Peter Griffin",
					GitHubLink = "github.com/microsoft",
					PhoneNumber = "911"
				});
				db.SaveChanges();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Add(User user) {
			if (user == null) {
				return BadRequest();
			}
			db.Users.Add(user);
			await db.SaveChangesAsync();
			return Ok(user);
		}
	}
}
