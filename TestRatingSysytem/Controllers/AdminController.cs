using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TestRatingSystem.Models;
using TestRatingSystem.ViewModels;

namespace TestRatingSystem.Controllers {
	[Authorize]
	[Route("Admin/[action]")]
	[ApiController]
	public class AdminController : ControllerBase {

		SubmissionsContext db;
		public AdminController(SubmissionsContext context) {
			db = context;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllSubmissions() {
			IEnumerable<Submission> submissions = Array.Empty<Submission>();
			await Task.Run(() => submissions = db.Submissions
			.Include(submission => submission.Grade)
			.Include(submission => submission.Feedback)
				.ThenInclude(feedback => feedback.Criterions)
			.ToArray());
			return new JsonResult(submissions);
		}

		[HttpPut]
		public async Task<IActionResult> Review(AdminsTestFeedback feedback, int submissionId) {
			//Submission submission = db.Submissions.Find(submissionId);
			Submission[] submissions = db.Submissions
				.Include(submission => submission.Grade)
				.Include(submission => submission.Feedback)
					.ThenInclude(feedback => feedback.Criterions)
				.ToArray();
			Submission submission = submissions.FirstOrDefault(submission => submission.Id == submissionId);
			if (submission == null || feedback.Criterions.Count != db.TestCriterionInfos.Count()) {
				return BadRequest();
			}
			else if (submission.State == Submission.SubmissionStates.New) {
				submission.State = Submission.SubmissionStates.Resolved;
				submission.Grade = new Grade(feedback.Criterions.Count(criterion => criterion.IsDone), true);
				submission.Feedback = feedback;
			}
			else if (submission.State == Submission.SubmissionStates.Resolved) {
				submission.Grade.Value = feedback.Criterions.Count(criterion => criterion.IsDone);
				for (int i = 0; i < db.TestCriterionInfos.Count(); i++) {
					TestCriterion criterion = feedback.Criterions.FirstOrDefault(criterion => criterion.Id == submission.Feedback.Criterions[i].Id);
					submission.Feedback.Criterions[i].IsDone = criterion.IsDone;
				}
			}
			db.Update(submission);
			await db.SaveChangesAsync();
			return Ok();
		}

		[HttpGet]
		public async Task<IActionResult> Review(int submissionId) {
			Submission[] submissions = Array.Empty<Submission>();
			await Task.Run(() => submissions = db.Submissions
			.Include(submission => submission.Grade)
			.Include(submission => submission.Feedback)
				.ThenInclude(feedback => feedback.Criterions)
			.ToArray());
			Submission submission = submissions.FirstOrDefault(submission => submission.Id == submissionId);
			if (submission.State == Submission.SubmissionStates.New) {
				submission.Feedback = new AdminsTestFeedback{
					Criterions = db.TestCriterionInfos
					.Select(info => new TestCriterion(info))
					.ToList()
				};
			}
			return new JsonResult(submission);
		}
	}
}
