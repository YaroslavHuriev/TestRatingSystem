using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestRatingSystem.Models {
	public class User {
		public int Id { get; set; }
		public string FullName { get; set; }
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string GitHubLink { get; set; }
		public int Grade { get; set; }

		public User(string fullName, string phoneNumber, string email, string gitHubLink, int grade) {
			FullName = fullName;
			PhoneNumber = phoneNumber;
			Email = email;
			GitHubLink = gitHubLink;
			Grade = grade;
		}

		public User() { }
	}
}
