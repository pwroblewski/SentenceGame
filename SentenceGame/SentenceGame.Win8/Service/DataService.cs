using SentenceGame.Portable.Model;
using SentenceGame.Portable.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;

namespace SentenceGame.Win8.Service
{
    public class SentenceService : ISentenceService
    {
        private const string XmlExt = ".xml";
        private ObservableCollection<Domain> domains = new ObservableCollection<Domain>();

        public async Task<IList<Domain>> GetDomains()
        {
            try
            {

                StorageFolder sf = await ApplicationData.Current.LocalFolder.GetFolderAsync("Data");
                IReadOnlyList<StorageFolder> sf2 = await sf.GetFoldersAsync();

                foreach (StorageFolder folder in sf2)
                {
                    // czytanie lekcji
                    IList<Lesson> lessonsList = new List<Lesson>();

                    IReadOnlyList<StorageFile> allFiles = await folder.GetFilesAsync();
                    var lessonsFiles = allFiles.Where(x => x.DisplayName.Contains("Lesson"));

                    foreach (StorageFile file in lessonsFiles)
                    {
                        var xmlStream = await FileIO.ReadTextAsync(file);

                        XDocument doc = XDocument.Parse(xmlStream);

                        var data = from query in doc.Descendants("Lesson")
                                   select new Lesson
                                   {
                                       Title = query.Element("Title").ToString(),
                                       Description = query.Element("Description").ToString(),
                                       ImagePath = query.Element("ImagePath").ToString()
                                   };
                        lessonsList.Add((Lesson)data);
                    }

                    // czytanie pliku kategorii
                    string fileName = folder.Name + XmlExt;

                    StorageFile cat = await folder.GetFileAsync(fileName);

                    var xmlStream2 = await FileIO.ReadTextAsync(cat);

                    XDocument doc2 = XDocument.Parse(xmlStream2);

                    var data2 = from query in doc2.Descendants("Category")
                                select new Domain
                                {
                                    Title = query.Element("Title").ToString(),
                                    Description = query.Element("Description").ToString(),
                                    ImagePath = query.Element("ImagePath").ToString(),
                                    Lessons = lessonsList
                                };

                    // czytanie

                    domains.Add((Domain)data2);
                }
            }
            catch (Exception ex)
            {

            }
            //StorageFolder sfq = await sf.GetFolderAsync("Questionss");
            //StorageFile st = await sfq.GetFileAsync(FileName);

            //var xmlStream = await FileIO.ReadTextAsync(st);

            //XDocument doc = XDocument.Parse(xmlStream);

            //var data = from query in doc.Descendants("question")
            //           select new Question
            //           {
            //               Id = (int)query.Element("id"),
            //               Text = (string)query.Element("text"),
            //               AnswerCorrect = (int)query.Element("answearcorrect"),
            //               AnswerIncorrect = (int)query.Element("answearincorrect")
            //           };

            return await Task.FromResult(domains);
        }

        public async Task<Domain> GetDomain(string title)
        {
            return await Task.FromResult(domains.Single(x => x.Title.Equals(title)));
        }
    }
}
