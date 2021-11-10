using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TestRatingSystem.Models;

namespace TestRatingSystem.ViewModels {
	[Keyless]
	public class SubmissionAdminGet {
		public int SubmissionId { get; set; }
		//public Grade Grade { get; set; }
		public int GradeValue { get; set; }
		public bool GradeIsSet { get; set; }
		public string Email { get; set; }
		public string GitHubLink { get; set; }
		public ICollection<TestCriterion> Criterions { get; set; }
	}
}
