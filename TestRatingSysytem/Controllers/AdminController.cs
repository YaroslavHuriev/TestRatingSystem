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
	[Route("Submissions")]
	[ApiController]
	public class AdminController : ControllerBase {

		SubmissionsContext db;
		public AdminController(SubmissionsContext context) {
			db = context;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAllSubmissions() {
			IEnumerable<Submission> submissions = Array.Empty<Submission>();
			await Task.Run(() => submissions = db.Submissions
			.Include(submission => submission.Feedback)
				.ThenInclude(feedback => feedback.Criteria)
			.ToArray());
			IEnumerable<SubmissionAdminGetAllSubmissions> submissionsForView = submissions.Select(submission => new SubmissionAdminGetAllSubmissions {
				Email = submission.Email,
				FathersName = submission.FathersName,
				GitHubURL = submission.GitHubURL,
				Grade = submission.Grade,
				Name = submission.Name,
				Notes = submission.Notes,
				PhoneNumber = submission.PhoneNumber,
				State = submission.State,
				SubmissionId = submission.Id,
				Surname = submission.Surname
			});
			return new JsonResult(submissionsForView);
		}

		[HttpPatch("{submissionId}")]
		public async Task<IActionResult> Review(List<TestCriterionAdminReviewPut> criteria, int submissionId) {
			Submission[] submissions = db.Submissions
				.Include(submission => submission.Feedback)
					.ThenInclude(feedback => feedback.Criteria)
				.ToArray();
			Submission submission = submissions.FirstOrDefault(submission => submission.Id == submissionId);
			if (submission == null || criteria.Count != db.TestCriterionInfos.Count()) {
				return BadRequest();
			}
			else if (submission.State == Submission.SubmissionStates.New) {
				submission.State = Submission.SubmissionStates.Resolved;
			}
			submission.Grade = criteria.Count(criterion => criterion.IsDone);
			for (int i = 0; i < db.TestCriterionInfos.Count(); i++) {
				TestCriterionAdminReviewPut criterion = criteria.FirstOrDefault(criterion => criterion.Id == submission.Feedback.Criteria[i].Id);
				submission.Feedback.Criteria[i].IsPassed = criterion.IsDone;
			}
			db.Update(submission);
			await db.SaveChangesAsync();
			return Ok();
		}

		[HttpGet("{submissionId}")]
		public async Task<IActionResult> Review(int submissionId) {
			Submission[] submissions = Array.Empty<Submission>();
			await Task.Run(() => submissions = db.Submissions
			.Include(submission => submission.Feedback)
				.ThenInclude(feedback => feedback.Criteria)
			.ToArray());
			Submission submission = submissions.FirstOrDefault(submission => submission.Id == submissionId);
			SubmissionAdminReviewGet submissionAdminReview = new() {
				Id = submission.Id,
				Email = submission.Email,
				Feedback = new FeedbackAdminReviewGet {
					Id = submission.Feedback.Id,
					Criteria = submission.Feedback.Criteria.Select(criterion =>
					new TestCriterionAdminReviewGet {
						Id = criterion.Id,
						Title = criterion.Title,
						IsPassed = criterion.IsPassed,
						Group = criterion.Group,
					})
					.ToList()
				},
				State = submission.State,
				FathersName = submission.FathersName,
				GitHubURL = submission.GitHubURL,
				Grade = submission.Grade,
				Name = submission.Name,
				Surname = submission.Surname
			};
			return new JsonResult(submissionAdminReview);
		}
	}
}
