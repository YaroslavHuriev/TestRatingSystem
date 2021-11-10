using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TestRatingSystem.Models;

using static TestRatingSystem.Models.Submission;

namespace TestRatingSystem.ViewModels {
	public class SubmissionResultModel {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string FathersName { get; set; }
		public string GitHubLink { get; set; }
		public SubmissionStates State { get; set; }
		public Grade SubmissionResultGrade { get; set;}

		public SubmissionResultModel(string name, string gitHubLink, SubmissionStates state, Grade grade, string surname, string fathersName, int id) {
			Name = name;
			GitHubLink = gitHubLink;
			State = state;
			SubmissionResultGrade = grade;
			Surname = surname;
			FathersName = fathersName;
			Id = id;
		}
	}
}
