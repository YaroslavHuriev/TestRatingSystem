using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;


namespace TestRatingSystem.Models {
	public class SubmissionsContext : DbContext {
		public DbSet<Submission> Submissions { get; set; }
		public DbSet<TestCriterionInfo> TestCriterionInfos { get; set; }

		public SubmissionsContext(DbContextOptions<SubmissionsContext> options)
			: base(options) {
			//Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			List<TestCriterion> criteria = new List<TestCriterion>();
			
			TestCriterionInfo[] infos = new TestCriterionInfo[]{
				new TestCriterionInfo { Id = 1,Title = "Код розбитий на функції", Group = "codeStructure",IsVisibleForUser = true},
				new TestCriterionInfo { Id = 2,Title = "Зрозумілі найменування змінних", Group = "codeStructure",IsVisibleForUser = true},
				new TestCriterionInfo { Id = 3,Title = "Послідовні коміти з осмисленими коментарями", Group = "codeStructure",IsVisibleForUser = true},
				new TestCriterionInfo { Id = 4,Title = "Лінійна або логарифмічна складність алгоритму", Group = "codeStructure",IsVisibleForUser = true},
				new TestCriterionInfo { Id = 5,Title = "Додавання декількох записів у день", Group = "functionalRequirements",IsVisibleForUser = true},
				new TestCriterionInfo { Id = 6,Title = "Виведення 0 за пропущені дати", Group = "functionalRequirements",IsVisibleForUser = true},
				new TestCriterionInfo { Id = 7,Title = "Форматування дати у відповідності з прикладом", Group = "functionalRequirements",IsVisibleForUser = true},
				new TestCriterionInfo { Id = 8,Title = "Шляхи до файлів зчитуються з параметрів", Group = "functionalRequirements",IsVisibleForUser = true}
				};
			for (int i = 0; i < 3; i++) {
				criteria.AddRange(infos.Select((info) => new TestCriterion {
					Id = info.Id+i*8,
					AdminsTestFeedbackId = i+1,
					Title = info.Title,
					Group = info.Group,
					IsVisibleForUser = info.IsVisibleForUser
				}));
			}
			modelBuilder.Entity<TestCriterionInfo>().HasData(infos);
			modelBuilder.Entity<Submission>().HasData(new Submission[] {
				new Submission{ 
					Id = 1,
					Name = "Peter",
					Surname = "Griffin",
					FathersName = "Bob",
					Email = "test@gmail.com",
					GitHubURL = "github.com/microsoft",
					PhoneNumber = "911",
					Notes = "Nice work",
					State = Submission.SubmissionStates.New
				},
				new Submission{ 
					Id = 2,
					Name = "Chris",
					Surname = "Griffin",
					FathersName = "Peter",
					Email = "test@gmail.com",
					GitHubURL = "github.com/microsoft",
					PhoneNumber = "911",
					Notes = "Nice work",
					State = Submission.SubmissionStates.New
				},
				new Submission{ 
					Id = 3,
					Name = "Stewart",
					Surname = "Griffin",
					FathersName = "Fatman",
					Email = "test@gmail.com",
					GitHubURL = "github.com/microsoft",
					PhoneNumber = "911",
					Notes = "Nice work",
					State = Submission.SubmissionStates.New
				}
			});
			modelBuilder.Entity<AdminsTestFeedback>().HasData(new AdminsTestFeedback[]{
				new AdminsTestFeedback{
					Id = 1,
					SubmissionId = 1
				},
				new AdminsTestFeedback{
					Id = 2,
					SubmissionId = 2
				},
				new AdminsTestFeedback{
					Id = 3,
					SubmissionId = 3
				}
			});
			modelBuilder.Entity<TestCriterion>().HasData(criteria);
		}
	}
}
