using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentenceGame.Portable.Services
{
    public interface IDialogService
    {
        Task ShowMessage(string message, string title);
        Task ShowWrongAnswerMessage(ObservableCollection<string> goodAnswer, string title);
    }
}
