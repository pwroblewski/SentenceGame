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

        public ObservableCollection<Domain> GetDomain()
        {
            ObservableCollection<Sentence> sentences = new ObservableCollection<Sentence>();

            sentences.Add(new Sentence
            {
                Text = "Kot pije mleko",
                Translation = "The cat drinks milk"
            });
            sentences.Add(new Sentence
            {
                Text = "Pies bawi się piłką",
                Translation = "Dog plays with the ball"
            });
            sentences.Add(new Sentence
            {
                Text = "Gołąb lata po niebie",
                Translation = "Pigeon is flying on the sky"
            });

            ObservableCollection<Lesson> lessons = new ObservableCollection<Lesson>();
            lessons.Add(new Lesson
            {
                Title = "Poziom podstawowy",
                ImagePath = "ms-appx:///Images/Zwierzeta/Zwierzeta.jpg",
                Description = "To jest lekcja na poziomie podstawowym",
                Sentences = sentences
            });
            lessons.Add(new Lesson
            {
                Title = "Poziom średniozaawansowany",
                ImagePath = "ms-appx:///Images/Zwierzeta/Zwierzeta.jpg",
                Description = "To jest lekcja na poziomie średniozaawansowanym"
            });
            lessons.Add(new Lesson
            {
                Title = "Poziom zaawansowany",
                ImagePath = "ms-appx:///Images/Zwierzeta/Zwierzeta.jpg",
                Description = "To jest lekcja na poziomie zaawansowanym"
            });

            Domain domain = new Domain
            {
                Title = "Zwierzęta",
                ImagePath = "ms-appx:///Images/Zwierzeta/Zwierzeta.jpg",
                Description = "Lekcje do nauki układania zdań ze zwięrzętami",
                Lessons = lessons
            };

            domains.Add(domain);
            domains.Add(domain);

            return domains;
        }
        public ObservableCollection<Lesson> GetLessons(Domain domain)
        {
            var dom = domains.Single(x => x.Title.Equals(domain.Title));
            return dom.Lessons;
        }
    }
}
