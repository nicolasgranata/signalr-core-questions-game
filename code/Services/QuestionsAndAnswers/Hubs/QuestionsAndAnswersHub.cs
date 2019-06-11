using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace QuestionsAndAnswers.Hubs
{
    public class QuestionsAndAnswersHub : Hub
    {
        public async Task NewMessage(string username, string message)
        {
            await Clients.All.SendAsync("messageReceived", username, message);
        }
    }
}
