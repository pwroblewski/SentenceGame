using SentenceGame.Portable.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentenceGame.Portable.Services
{
    public interface ISentenceService
    {
        Task<ObservableCollection<Domain>> GetDomains();
        Task<Domain> GetDomain(string title);
        Task<Lesson> GetLesson(string dTitle, string lTitle);
    }
}
