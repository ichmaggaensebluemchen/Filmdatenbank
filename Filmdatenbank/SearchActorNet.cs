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
            //Sammlung aller Filme IDs in denen ein Schauspieler mitgewirkt hat
            HashSet<int> selMovies = MovProData.ActorMoviesDic[actorIDint];

            //Alle Schauspieler die den gesammelten Filmen mitgespielt haben
            HashSet<int> allActorsMoviesNet = new HashSet<int>();
            foreach (var movieID in selMovies)
            {
                foreach (var item in MovProData.MovieActorsDic[movieID])
                {
                    allActorsMoviesNet.Add(item);
                }
            }


            PrintNet(selMovies, "Filme:", (Object)MovProData.MoviesDic);
            PrintNet(allActorsMoviesNet, "Schauspieler:", (Object)MovProData.ActorsDic);
        }
    }
}