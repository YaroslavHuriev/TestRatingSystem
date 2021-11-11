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
			IEnumerable<SubmissionAdminGetAllSubmissions> submissionsForView = submissions.Select(submission => new SubmissionAdminGetAllSubmissions {
				Email = submission.Email,
				FathersName = submission.FathersName,
				GitHubLink = submission.GitHubLink,
				Grade = submission.Grade.Value,
				Name = submission.Name,
				Notes = submission.Notes,
				PhoneNumber = submission.PhoneNumber,
				State = submission.State,
				SubmissionId = submission.Id,
				Surname = submission.Surname
			});
			return new JsonResult(submissionsForView);
		}

		[HttpPut]
		public async Task<IActionResult> Review(List<TestCriterionAdminReviewPut> criterions, int submissionId) {
			Submission[] submissions = db.Submissions
				.Include(submission => submission.Grade)
				.Include(submission => submission.Feedback)
					.ThenInclude(feedback => feedback.Criterions)
				.ToArray();
			Submission submission = submissions.FirstOrDefault(submission => submission.Id == submissionId);
			if (submission == null || criterions.Count != db.TestCriterionInfos.Count()) {
				return BadRequest();
			}
			else if (submission.State == Submission.SubmissionStates.New) {
				submission.State = Submission.SubmissionStates.Resolved;
			}
			submission.Grade.Value = criterions.Count(criterion => criterion.IsDone);
			for (int i = 0; i < db.TestCriterionInfos.Count(); i++) {
				TestCriterionAdminReviewPut criterion = criterions.FirstOrDefault(criterion => criterion.Id == submission.Feedback.Criterions[i].Id);
				submission.Feedback.Criterions[i].IsDone = criterion.IsDone;
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
			SubmissionAdminReviewGet submissionAdminReview = new SubmissionAdminReviewGet {
				Id = submission.Id,
				Email = submission.Email,
				Feedback = new FeedbackAdminReviewGet {
					Id = submission.Feedback.Id,
					SubmissionId = submission.Id,
					Criterions = submission.Feedback.Criterions.Select(criterion =>
					new TestCriterionAdminReviewGet {
						Id = criterion.Id,
						Description = criterion.Description,
						IsDone = criterion.IsDone,
						Group = criterion.Group,
						AdminsTestFeedbackId = criterion.AdminsTestFeedbackId
					})
					.ToList()
				},
				FathersName = submission.FathersName,
				GitHubLink = submission.GitHubLink,
				Grade = submission.Grade.Value,
				Name = submission.Name,
				Surname = submission.Surname
			};
			return new JsonResult(submissionAdminReview);
		}
	}
}
