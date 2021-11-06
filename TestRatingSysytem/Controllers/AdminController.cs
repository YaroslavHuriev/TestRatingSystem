using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TestRatingSystem.Models;

namespace TestRatingSystem.Controllers {
	[Authorize]
	[Route("Admin/[action]")]
	[ApiController]
	public class AdminController : ControllerBase {

		UsersContext db;
		public AdminController(UsersContext context) {
			db = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsers() {
			IEnumerable<User> users = new User[0];
			await Task.Run(() => users = db.Users);
			return new JsonResult(users);
		}

		[HttpPut]
		public async Task<IActionResult> AddGrade(int grade, int userId) {
			User user = db.Users.Find(userId);
			if (user==null) {
				return BadRequest();
			}
			user.Grade = grade;
			db.Update(user);
			await db.SaveChangesAsync();
			return Ok(user);
		}
	}
}
