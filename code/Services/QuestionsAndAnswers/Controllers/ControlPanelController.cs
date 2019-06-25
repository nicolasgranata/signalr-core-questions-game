using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QuestionsAndAnswers.Hubs;
using QuestionsAndAnswers.Hubs.Interfaces;

namespace QuestionsAndAnswers.Controllers
{
    public class ControlPanelController : Controller
    {
        private readonly IHubContext<QuestionsAndAnswersHub, IQuestionsAndAnswersClient> _hubContext;

        public ControlPanelController(IHubContext<QuestionsAndAnswersHub, IQuestionsAndAnswersClient> hubContext)
        {
            _hubContext = hubContext;
        }

        public IActionResult Index()
        {
            _hubContext.Clients.All.MessageReceived("var", "message");
            return View();
        }
    }
}