using SentenceGame.Portable.Model;
using SentenceGame.Portable.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentenceGame.Win8.Service
{
    public class SentenceService : ISentenceService
    {
        private List<Sentence> Sentences = new List<Sentence>();

        public List<Sentence> GetSentences()
        {
            Sentences.Add(new Sentence
            {
                Text = "Chłopiec czyta książkę",
                Translation = "The boy reads book"
            });
            Sentences.Add(new Sentence
            {
                Text = "Dziewczynka jeździ na rowerze",
                Translation = "The girl rides a bike"
            });
            Sentences.Add(new Sentence
            {
                Text = "Mam na imię Piotrek",
                Translation = "My name is Peter"
            });

            return Sentences;
        }
    }
}
