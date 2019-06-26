using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestionsAndAnswers.Services
{
    public interface IControlPanelService
    {
        Task StartGame();

        Task RestartGame();
    }
}
