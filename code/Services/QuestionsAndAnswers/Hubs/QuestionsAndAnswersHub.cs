using Microsoft.AspNetCore.SignalR;
using QuestionsAndAnswers.Hubs.Interfaces;
using System.Threading.Tasks;

namespace QuestionsAndAnswers.Hubs
{
    public class QuestionsAndAnswersHub : Hub<IQuestionsAndAnswersClient>
    {
        public async Task NewMessage(string username, string message)
        {
            await Clients.All.MessageReceived(username, message);
        }
    }
}
