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
        Task<IList<Domain>> GetDomains();
        Task<Domain> GetDomain(string title);
        Task<IList<Sentence>> GetSentences(string lessonPath);
    }
}
