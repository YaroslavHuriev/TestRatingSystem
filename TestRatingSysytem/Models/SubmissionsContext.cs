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
			modelBuilder.Entity<TestCriterionInfo>().HasData(
				new TestCriterionInfo[]
				{
				new TestCriterionInfo { Id = 1,Description = "Код розбитий на функції", Group = "codeStructure",IsVisibleForUser = true},
				new TestCriterionInfo { Id = 2,Description = "Зрозумілі найменування змінних", Group = "codeStructure",IsVisibleForUser = true},
				new TestCriterionInfo { Id = 3,Description = "Послідовні коміти з осмисленими коментарями", Group = "codeStructure",IsVisibleForUser = true},
				new TestCriterionInfo { Id = 4,Description = "Лінійна або логарифмічна складність алгоритму", Group = "codeStructure",IsVisibleForUser = true},
				new TestCriterionInfo { Id = 5,Description = "Додавання декількох записів у день", Group = "functionalRequirements",IsVisibleForUser = true},
				new TestCriterionInfo { Id = 6,Description = "Виведення 0 за пропущені дати", Group = "functionalRequirements",IsVisibleForUser = true},
				new TestCriterionInfo { Id = 7,Description = "Форматування дати у відповідності з прикладом", Group = "functionalRequirements",IsVisibleForUser = true},
				new TestCriterionInfo { Id = 8,Description = "Шляхи до файлів зчитуються з параметрів", Group = "functionalRequirements",IsVisibleForUser = true}
				});
			modelBuilder.Entity<Submission>().HasData(
				new Submission[] {
					new Submission{ Id = 1,Name = "Peter",Surname = "Griffin",FathersName = "Bob",Email = "test@gmail.com",GitHubLink = "github.com/microsoft",PhoneNumber = "911",Notes = "Nice work",State = Submission.SubmissionStates.New},
					new Submission{ Id = 2,Name = "Chris",Surname = "Griffin",FathersName = "Peter",Email = "test@gmail.com",GitHubLink = "github.com/microsoft",PhoneNumber = "911",Notes = "Nice work",State = Submission.SubmissionStates.New},
					new Submission{ Id = 3,Name = "Stewart",Surname = "Griffin",FathersName = "Fatman",Email = "test@gmail.com",GitHubLink = "github.com/microsoft",PhoneNumber = "911",Notes = "Nice work",State = Submission.SubmissionStates.New}
				});
		}
	}
}
