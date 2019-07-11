using System;
using System.Collections.Generic;

namespace Filmdatenbank
{
    public class SearchBase
    {
        public SearchBase(DataImport movProData)
        {
            MovProData = movProData;
        }

        //Gibt eine Liste vom IDs zurück in deren Namensangaben das mit 'Filter' angegebene Wort vorkommt
        public List<int> PatternBase(string filter)
        {
            List<int> filteredList = new List<int>();
            foreach (var item in MovProData.MoviesDic)
            {
                if (item.Value.Movie_Title.Contains(filter))
                {
                    filteredList.Add(item.Key);
                }
            }
            return filteredList;  //Liste von Schauspielern deren Name das Suchwort enthält
        }

        public void PrintMovies(List<int> movies)
        {
            foreach (var movie in movies)
            {
                Console.WriteLine("ID {0} - {1}", movie, MovProData.MoviesDic[movie].Movie_Title);
                Console.WriteLine("{0}", MovProData.MoviesDic[movie].Movie_Plot);
                Console.WriteLine();
            }
        }

        public DataImport MovProData { get; set; }
    }
}