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
	[Route("Submissions")]
	public class UserController : ControllerBase {

		SubmissionsContext db;
		public UserController(SubmissionsContext context) {
			db = context;
		}

		[HttpPost("")]
		public async Task<IActionResult> Add(NewSubmission newSubmission) {
			if (newSubmission == null) {
				return BadRequest();
			}
			Submission submission = new() {
				Name = newSubmission.Name,
				Surname = newSubmission.Surname,
				FathersName = newSubmission.FathersName,
				PhoneNumber = newSubmission.PhoneNumber,
				Email = newSubmission.Email,
				GitHubURL = newSubmission.GitHubURL,
				State = Submission.SubmissionStates.New,
				Notes = newSubmission.Notes,
				Feedback = new AdminsTestFeedback()
			};
			submission.Feedback.Criteria = db.TestCriterionInfos.Select(criterion => new TestCriterion(criterion)).ToList();
			db.Submissions.Add(submission);
			await db.SaveChangesAsync();
			return Ok();
		}
	}
}
