using System;
using System.Collections.Generic;

namespace Filmdatenbank
{
    public class SearchMovie
    {
        private DataImport movProData;
        private List<int> filteredMovies = new List<int>();

        public SearchMovie(DataImport movProData)
        {
            this.movProData = movProData;
        }

        public List<int> Filter(string filter)
        {
            foreach (var film in movProData.MoviesDic)
            {
                if (film.Value.Movie_Title.Contains(filter))
                {
                    filteredMovies.Add(film.Key);
                }
            }
            return filteredMovies;
        }

        public void Print(List<int> movies, string filter)
        {
            Console.WriteLine("Die Suche nach '{0}' ergab {1} Treffer:", filter, movies.Count);
            Console.WriteLine("-----------------------------------------------------------");
            foreach (var movie in movies)
            {
                Console.WriteLine("ID {0} - {1}", movie, movProData.MoviesDic[movie].Movie_Title);
                Console.WriteLine("{0}", movProData.MoviesDic[movie].Movie_Plot);
                Console.WriteLine();
            }
        }
    }
}