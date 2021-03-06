using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
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
		public string GitHubURL { get; set; }
		public string Notes { get; set; }
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public Submission.SubmissionStates State { get; set; }
		public int Grade { get; set; }
	}
}
