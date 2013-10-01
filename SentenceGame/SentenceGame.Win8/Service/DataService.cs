using SentenceGame.Portable.Model;
using SentenceGame.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentenceGame.Win8.Service
{
    public class SentenceService : ISentenceService
    {
        private ObservableCollection<Domain> domains = new ObservableCollection<Domain>();

        public async Task<ObservableCollection<Domain>> GetDomains()
        {
            ObservableCollection<Sentence> sentences = new ObservableCollection<Sentence>()
            {
                new Sentence
                {
                    Text = "Kot pije mleko",
                    Translation = "The cat drinks milk"
                },
                new Sentence
                {
                    Text = "Pies bawi się piłką",
                    Translation = "Dog plays with the ball"
                },
                new Sentence
                {
                    Text = "Gołąb lata po niebie",
                    Translation = "Pigeon is flying on the sky "
            }};

            ObservableCollection<Lesson> lessons = new ObservableCollection<Lesson>()
            {
                new Lesson
                {
                    Title = "Poziom podstawowy",
                    ImagePath = "ms-appx:///Images/Zwierzeta/Zwierzeta.jpg",
                    Description = "To jest lekcja na poziomie podstawowym",
                    Sentences = sentences
                }, 
                new Lesson
                {
                    Title = "Poziom średniozaawansowany",
                    ImagePath = "ms-appx:///Images/Zwierzeta/Zwierzeta.jpg",
                    Description = "To jest lekcja na poziomie średniozaawansowanym"
                },
                new Lesson
                {
                    Title = "Poziom zaawansowany",
                    ImagePath = "ms-appx:///Images/Zwierzeta/Zwierzeta.jpg",
                    Description = "To jest lekcja na poziomie zaawansowanym"
            }};

            Domain domZw = new Domain
            {
                Title = "Zwierzęta",
                ImagePath = "ms-appx:///Images/Zwierzeta/Zwierzeta.jpg",
                Description = "Lekcje do nauki układania zdań ze zwięrzętami",
                Lessons = lessons
            };

            Domain domR = new Domain
            {
                Title = "Rośliny",
                ImagePath = "ms-appx:///Images/Zwierzeta/Zwierzeta.jpg",
                Description = "Lekcje do nauki układania zdań z roślinami",
                Lessons = lessons
            };

            domains.Add(domZw);
            domains.Add(domR);

            return await Task.FromResult(domains);
        }
        public async Task<Domain> GetDomain(string title)
        {
            return await Task.FromResult(domains.Single(x => x.Title.Equals(title)));
        }

        public async Task<Lesson> GetLesson(string dTitle, string lTitle)
        {
            Domain dom = await GetDomain("Zwierzęta");
            return await Task.FromResult(dom.Lessons[0]);
        }
    }
}
