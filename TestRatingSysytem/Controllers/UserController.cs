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
		}

		[HttpPost]
		public async Task<IActionResult> Add(SubmissionPost submissionPost) {
			if (submissionPost == null) {
				return BadRequest();
			}
			Submission submission = new(submissionPost);
			submission.Feedback.Criterions = db.TestCriterionInfos.Select(criterion => new TestCriterion(criterion)).ToList();
			db.Submissions.Add(submission);
			await db.SaveChangesAsync();
			return Ok();
		}
	}
}
