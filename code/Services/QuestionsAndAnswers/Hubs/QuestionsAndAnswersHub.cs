using Microsoft.AspNetCore.SignalR;
using QuestionsAndAnswers.Hubs.Interfaces;
using System;
using System.Threading.Tasks;

namespace QuestionsAndAnswers.Hubs
{
    public class QuestionsAndAnswersHub : Hub<IQuestionsAndAnswersClient>
    {       
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "usersGame");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "usersGame");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
