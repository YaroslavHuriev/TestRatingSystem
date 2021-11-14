using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using TestRatingSystem.ViewModels;

namespace TestRatingSystem.Models {
	public class Submission {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string FathersName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string GitHubURL { get; set; }
		public string Notes { get; set; }
		[JsonConverter(typeof(JsonStringEnumConverter))]
		public SubmissionStates State { get; set; }
		public enum SubmissionStates {
			New,
			Resolved
		}
		public int Grade {get;set;}
		public AdminsTestFeedback Feedback { get; set; }
	}
}
