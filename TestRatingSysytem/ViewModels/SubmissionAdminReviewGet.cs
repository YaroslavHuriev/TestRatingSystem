using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRatingSystem.ViewModels {
	public class SubmissionAdminReviewGet {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string FathersName { get; set; }
		public string Email { get; set; }
		public int Grade { get; set; }
		public string GitHubLink { get; set; }
		public FeedbackAdminReviewGet Feedback { get; set; }

		public SubmissionAdminReviewGet() {
		}
	}

	public class FeedbackAdminReviewGet {
		public int Id { get; set; }
		public int SubmissionId { get; set; }
		public List<TestCriterionAdminReviewGet> Criterions { get; set; }

		public FeedbackAdminReviewGet() {
		}
	}

	public class TestCriterionAdminReviewGet {
		public int Id { get; set; }
		public int AdminsTestFeedbackId { get; set; }
		public bool IsDone { get; set; }
		public string Description { get; set; }
		public string Group { get; set; }

		public TestCriterionAdminReviewGet() {
		}
	}
}
