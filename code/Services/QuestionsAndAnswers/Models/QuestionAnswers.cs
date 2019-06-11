using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace QuestionsAndAnswers.Models
{
    public class QuestionAnswers
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public IEnumerable<Option> Options { get; set; }

        public int Answer { get; set; }
    }

    public class Option
    {
        public int Id { get; set; }
        
        [JsonProperty("option")]
        public string Text { get; set; }
    }

    public class QuestionAnswersData
    {
        public QuestionAnswers[] QuestionAnswers { get; set; }
    }
}
