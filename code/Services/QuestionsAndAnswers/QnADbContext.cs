using System.IO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using QuestionsAndAnswers.Models;

namespace QuestionsAndAnswers
{
    public class QnADbContext: DbContext
    {
        public QnADbContext(DbContextOptions<QnADbContext> options)
            : base(options)
        {

        }

        public DbSet<QuestionAnswers> QuestionAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            QuestionAnswersData questionAnswersData;
            using (var stream = File.OpenRead("QnA.json"))
            using (var streamReader = new StreamReader(stream))
            using (var reader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();
                questionAnswersData = serializer.Deserialize<QuestionAnswersData>(reader);
            }

            modelBuilder.Entity<QuestionAnswers>()
                .HasData(questionAnswersData.QuestionAnswers);

            modelBuilder.Entity<QuestionAnswers>()
            .Ignore(p => p.Options);

            base.OnModelCreating(modelBuilder);
        }
    }
}
