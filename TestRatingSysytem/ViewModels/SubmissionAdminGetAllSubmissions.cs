using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using TestRatingSystem.Models;

namespace TestRatingSystem.ViewModels {
	[Keyless]
	public class SubmissionAdminGetAllSubmissions {
		public int SubmissionId { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string FathersName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string GitHubLink { get; set; }
		public string Notes { get; set; }
		public Submission.SubmissionStates State { get; set; }
		public int Grade { get; set; }

		public SubmissionAdminGetAllSubmissions() { }
	}
}
