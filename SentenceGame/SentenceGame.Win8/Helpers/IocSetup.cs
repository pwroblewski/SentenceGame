using GalaSoft.MvvmLight.Ioc;
using SentenceGame.Portable.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentenceGame.Win8.Helpers
{
    public class IocSetup
    {
        static IocSetup()
        {
            SimpleIoc.Default.Register<INavigationService>(() => new NavigationService());
        }
    }
}
