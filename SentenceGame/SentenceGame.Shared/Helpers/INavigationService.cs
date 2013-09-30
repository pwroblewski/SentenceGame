using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentenceGame.Portable.Helpers
{
    public interface INavigationService
    {
        void GoBack();
        void Navigate(string pageKey);
        void Navigate(string pageKey, object parameter);
    }
}
