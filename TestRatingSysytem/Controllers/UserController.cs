using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using TestRatingSystem.Models;
using TestRatingSystem.ViewModels;

namespace TestRatingSystem.Controllers {
	[ApiController]
	[Route("User/[action]")]
	public class UserController : ControllerBase {

		SubmissionsContext db;
		public UserController(SubmissionsContext context) {
			db = context;
			if (!db.Submissions.Any()) {
				db.Submissions.Add(new Submission {
					Grade = new Grade(),
					Email = "email@gmail.com",
					Name = "Peter",
					Surname = "Griffin",
					FathersName = "Bob",
					GitHubLink = "github.com/microsoft",
					PhoneNumber = "911"
				});
				db.SaveChanges();
			}
		}

		[HttpPost]
		public async Task<IActionResult> Add(SubmissionPost submissionPost) {
			if (submissionPost == null) {
				return BadRequest();
			}
			Submission submission = new(submissionPost);
			db.Submissions.Add(submission);
			await db.SaveChangesAsync();
			return Ok();
		}
	}
}
