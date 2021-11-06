using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestRatingSystem.Models {
	public class TestCriterion {
		public bool IsDone { get; set; }
		public string Description { get; set; }

		public TestCriterion(bool isDone, string description) {
			IsDone = isDone;
			Description = description;
		}
	}
}
