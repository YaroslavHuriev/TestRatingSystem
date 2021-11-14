using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRatingSystem.ViewModels {
	public class NewSubmission {
		public string Surname { get; set; }
		public string Name { get; set; }
		public string FathersName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string GitHubURL { get; set; }
		public string Notes { get; set; }
	}
}
