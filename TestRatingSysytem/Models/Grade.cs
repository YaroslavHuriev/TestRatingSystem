using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace TestRatingSystem.Models {
	public class Grade {
		public int Id { get; set; }
		public int SubmissionId { get; set; }
		public int Value { get; set; }
		public bool IsSet { get; set; }

		public Grade(int value, bool isSet) {
			Value = value;
			IsSet = isSet;
		}

		public Grade() {
			IsSet = false;
		}
	}
}
