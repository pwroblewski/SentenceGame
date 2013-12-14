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
        IList<Domain> domains = new List<Domain>();

        private const string XmlExt = ".xml";
        private const string imgUri = "ms-appx:///Data/";

        public async Task<IList<Domain>> GetDomains()
        {
            //IList<Domain> domains = new List<Domain>();

            try
            {
                var sf = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Data");

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

                        var data = (from query in doc.Descendants("Lesson")
                                    select new Lesson
                                    {
                                        Title = (string)query.Element("Title"),
                                        Description = (string)query.Element("Description"),
                                        ImagePath = imgUri + (string)query.Element("ImagePath"),
                                        LessonPath = folder.Name + "/" + file.Name
                                    }).First<Lesson>();
                        lessonsList.Add(data);
                    }

                    // czytanie pliku kategorii
                    string fileName = folder.Name + XmlExt;

                    StorageFile cat = await folder.GetFileAsync(fileName);

                    var xmlStream2 = await FileIO.ReadTextAsync(cat);

                    XDocument doc2 = XDocument.Parse(xmlStream2);

                    var data2 = (from query in doc2.Descendants("Category")
                                 select new Domain
                                 {
                                     Title = (string)query.Element("Title"),
                                     Description = (string)query.Element("Description"),
                                     ImagePath = imgUri + (string)query.Element("ImagePath"),
                                     Lessons = lessonsList
                                 }).First<Domain>();

                    // czytanie

                    domains.Add(data2);
                }
            }
            catch (Exception ex)
            {

            }

            return await Task.FromResult(domains);
        }

        public async Task<Domain> GetDomain(string title)
        {
            //IList<Domain> domains = await GetDomains();
            return await Task.FromResult(domains.Single(x => x.Title.Equals(title)));
        }

        public async Task<IList<Sentence>> GetSentences(string lessonPath)
        {
            string categoryFolder = lessonPath.Split('/')[0];
            string lessonFile = lessonPath.Split('/')[1];

            StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Data");
            StorageFolder folder2 = await folder.GetFolderAsync(categoryFolder);
            StorageFile file = await folder2.GetFileAsync(lessonFile);

            var xmlStream = await FileIO.ReadTextAsync(file);

            XDocument doc = XDocument.Parse(xmlStream);


            var data = from query in doc.Descendants("Sentence")
                       select new Sentence
                       {
                           Text = (string)query.Element("Text"),
                           Translation = (string)query.Element("Translation"),
                           ImagePath = ""
                       };

            return await Task.FromResult((IList<Sentence>)data.ToList());
        }
    }
}
