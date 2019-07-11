using System;
using System.Collections.Generic;

namespace Filmdatenbank
{
    public class SearchMovie: SearchBase
    {
        public SearchMovie(DataImport movProData, string Filter):base(movProData)
        {
            List<int> selMovies = PatternBase(Filter);
            Print(selMovies, Filter);
        }
        /// <summary>
        /// Gibt eine Liste mit Film IDs aus in denen 'Filter' im movie_title vorkommt
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        // Wird momentan nicht verwendet wegen Auslagerung in Basisklasse. Wenn sich am Ende zeigt, dass die Methode nur einmal verwendet wird kann diese hier wieder aktiviert werden und in der BAsisklasse gelöscht werden
        //public List<int> PatternThis(string filter)
        //{
        //    List<int> filteredMovies = new List<int>();
        //    foreach (var film in MovProData.MoviesDic)
        //    {
        //        if (film.Value.Movie_Title.Contains(filter))
        //        {
        //            filteredMovies.Add(film.Key);
        //        }
        //    }
        //    return filteredMovies;
        //}

        public void Print(List<int> movies, string filter)
        {
            Console.WriteLine("Die Suche nach '{0}' ergab {1} Treffer:", filter, movies.Count);
            Console.WriteLine("-----------------------------------------------------------");
            PrintMovies(movies);
        }
    }
}