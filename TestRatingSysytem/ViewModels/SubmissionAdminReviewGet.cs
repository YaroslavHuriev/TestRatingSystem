using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TestRatingSystem.Models;

namespace TestRatingSystem.ViewModels {
	public class SubmissionAdminReviewGet {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string FathersName { get; set; }
		public string Email { get; set; }
		public int Grade { get; set; }
		public string GitHubURL { get; set; }
		public Submission.SubmissionStates State { get; set; }
		public FeedbackAdminReviewGet Feedback { get; set; }
	}

	public class FeedbackAdminReviewGet {
		public int Id { get; set; }
		public List<TestCriterionAdminReviewGet> Criteria { get; set; }
	}

	public class TestCriterionAdminReviewGet {
		public int Id { get; set; }
		public bool IsPassed { get; set; }
		public string Title { get; set; }
		public string Group { get; set; }
	}
}
