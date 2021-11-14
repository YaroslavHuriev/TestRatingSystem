using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using TestRatingSystem.ViewModels;

namespace TestRatingSystem.Models {
	public class AdminsTestFeedback {
		public int Id { get; set; }
		public int SubmissionId { get; set; }
		public List<TestCriterion> Criteria { get; set; }

	}
}
