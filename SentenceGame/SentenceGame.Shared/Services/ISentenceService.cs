using SentenceGame.Portable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentenceGame.Portable.Services
{
    public interface ISentenceService
    {
        List<Sentence> GetSentences();
    }
}
