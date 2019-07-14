using System;
using System.Collections.Generic;

namespace Filmdatenbank
{
    public class SearchMovie: SearchBase
    {
        public SearchMovie(DataImport movProData, string Filter):base(movProData)
        {
            HashSet<int> selMovies = PatternBase(Filter);
            Print(selMovies, Filter);
        }
        /// <summary>
        /// Gibt eine Liste mit Film IDs aus in denen 'Filter' im movie_title vorkommt
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public void Print(HashSet<int> movies, string filter)
        {
            Console.WriteLine("Die Suche nach '{0}' ergab {1} Treffer:", filter, movies.Count);
            Console.WriteLine("-----------------------------------------------------------");
            PrintMovieDetails(movies);
        }
    }
}