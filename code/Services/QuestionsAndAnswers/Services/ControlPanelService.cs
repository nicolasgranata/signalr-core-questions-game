using Microsoft.AspNetCore.SignalR;
using QuestionsAndAnswers.Hubs;
using QuestionsAndAnswers.Hubs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QuestionsAndAnswers.Services
{
    public class ControlPanelService : IControlPanelService, IDisposable
    {
        private readonly IHubContext<QuestionsAndAnswersHub, IQuestionsAndAnswersClient> _hubContext;
        private Timer _timer;

        public ControlPanelService(IHubContext<QuestionsAndAnswersHub, IQuestionsAndAnswersClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task StartGame()
        {
            await _hubContext.Clients.All.StartGame();
            
            _timer = new Timer(SendQuestions, null, 0, 5000);
        }

        public async Task RestartGame()
        {
            await _hubContext.Clients.All.RestartGame();
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        private async void SendQuestions(object state)
        {
            await _hubContext.Clients.Group("usersGame").ReceiveQuestion("TestQuestion");
        }
    }
}
