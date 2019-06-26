using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QuestionsAndAnswers.Hubs;
using QuestionsAndAnswers.Hubs.Interfaces;
using QuestionsAndAnswers.Services;

namespace QuestionsAndAnswers.Controllers
{
    public class ControlPanelController : Controller
    {
        private readonly IControlPanelService _controlPanelService;

        public ControlPanelController(IControlPanelService controlPanelService)
        {
            _controlPanelService = controlPanelService;
        }

        public async Task<IActionResult> Index()
        {
            await _controlPanelService.StartGame();
            return View();
        }
    }
}