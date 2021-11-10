using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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
		public string GitHubLink { get; set; }
		public string Notes { get; set; }
		[ReadOnly(true)]
		public SubmissionStates State { get; set; }
		public enum SubmissionStates {
			New,
			Resolved
		}
		public Grade Grade {get;set;}
		public AdminsTestFeedback Feedback { get; set; }

		public Submission(string name,string surname,string fathersName, string phoneNumber, string email, string gitHubLink, Grade grade) {
			Name = name;
			Surname = surname;
			FathersName = fathersName;
			PhoneNumber = phoneNumber;
			Email = email;
			GitHubLink = gitHubLink;
			Grade = grade;
			State = SubmissionStates.New;
		}

		public Submission() { }

		public Submission(SubmissionPost submissionPost) {
			Name = submissionPost.Name;
			Surname = submissionPost.Surname;
			FathersName = submissionPost.FathersName;
			PhoneNumber = submissionPost.PhoneNumber;
			Email = submissionPost.Email;
			GitHubLink = submissionPost.GitHubLink;
			Grade = new Grade();
			State = SubmissionStates.New;
			Notes = submissionPost.Notes;
		}
	}
}
