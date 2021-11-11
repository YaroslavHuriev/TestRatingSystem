using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRatingSystem.Models {
	public class TestCriterion {
		public int Id { get; set; }
		public int AdminsTestFeedbackId { get; set; }
		public bool IsDone { get; set; }
		public string Description { get; set; }
		public bool IsVisibleForUser { get; set; }
		public string Group { get; set; }

		public TestCriterion() { }

		public TestCriterion(bool isDone, string description, bool isVisibleForUser) {
			IsDone = isDone;
			Description = description;
			IsVisibleForUser = isVisibleForUser;
		}

		public TestCriterion(TestCriterionInfo info) {
			Description = info.Description;
			IsVisibleForUser = info.IsVisibleForUser;
			Group = info.Group;
		}
	}
}
