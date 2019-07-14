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
            HashSet<int> selActors = MovProData.MovieActorsDic[movieIDint];

            //Alle Filme in denen die gesammelten Schauspieler mitgespielt haben
            HashSet<int> allMoviesActorsNet = new HashSet<int>();
            foreach (var actorID in selActors)
            {
                foreach (var item in MovProData.ActorMoviesDic[actorID])
                {
                    allMoviesActorsNet.Add(item);
                }
            }

            PrintNet(selActors, "Schauspieler:", (Object)MovProData.ActorsDic);
            PrintNet(allMoviesActorsNet, "Filme:", (Object)MovProData.MoviesDic);
        }
    }
}