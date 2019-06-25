using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsAndAnswers.Hubs.Interfaces
{
    public interface IQuestionsAndAnswersClient
    {
        Task ReceiveQuestion(string user, string question);
        Task MessageReceived(string username, string message);
    }
}
