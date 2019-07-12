using System;
using System.Collections.Generic;

namespace Filmdatenbank
{
    public class SearchActorNet : SearchBase
    {
        public SearchActorNet(DataImport movProData, string actorIDstring) : base (movProData)
        {
            int actorIDint;
            int.TryParse(actorIDstring, out actorIDint);
            //Liste aller Film IDs in ein Schauspieler mitgewirkt hat
            HashSet<int> selMovies = ActorOrMovieList(actorIDint, true);
            Dictionary<int, HashSet<int>> selActors = ActorsIDsAndThereMoviesIDs(selMovies);
            HashSet<int> uniqueActors = unique(selActors);

            PrintNet(selMovies, "Filme:", (Object)MovProData.MoviesDic);
            PrintNet(uniqueActors, "Schauspieler:", (Object)MovProData.ActorsDic);
        }
    }
}