using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace SentenceGame.Portable.Helpers
{
    public static class ExtensionMethods
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
        {
            var col = new ObservableCollection<T>();
            foreach (var cur in enumerable)
            {
                col.Add(cur);
            }

            return col;
        }
    }
}
