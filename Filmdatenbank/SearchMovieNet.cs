using System;
using System.Collections.Generic;

namespace Filmdatenbank
{
    public class SearchMovieNet : SearchBase
    {
        public SearchMovieNet(DataImport movProData, string movieIDstring):base(movProData)
        {
            int movieIDint;
            int.TryParse(movieIDstring, out movieIDint);
            //Liste aller Schauspieler IDs die in einem Film mitgewirkt haben
            HashSet<int> selActors = ActorOrMovieList(movieIDint, false);
            Dictionary<int, HashSet<int>> selMovies = ActorsIDsAndThereMoviesIDs(selActors);
            HashSet<int> uniqueMovies = unique(selMovies);

            PrintNet(selActors, "Schauspieler:", (Object)MovProData.ActorsDic);
            PrintNet(uniqueMovies, "Filme:", (Object)MovProData.MoviesDic);
        }
    }
}