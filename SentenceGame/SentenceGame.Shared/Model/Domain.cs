using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentenceGame.Portable.Model
{
    public class Domain
    {
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public ObservableCollection<Lesson> Lessons { get; set; }

        public Domain()
        { }
    }
}
