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
            //Liste aller Filme in der der Schauspieler mitgewirkt hat
            List<int> selMovies = OneActorManyMovies(actorIDint);
            Dictionary<int, List<int>> selActors = ActorsIDsAndThereMoviesIDs(selMovies);

            PrintMovies(selMovies);
            PrintActors(selActors);

        }

        void PrintMovies(List<int> selMovies)
        {
            Console.Write("Filme:");
            foreach (var movie in selMovies)
            {
                Console.Write(" {0},", MovProData.MoviesDic[movie].Movie_Title);
            }
            Console.Write("\b \b\n");
        }

        void PrintActors(Dictionary<int, List<int>> selActors)
        {
            Console.Write("Schauspieler:");
            foreach (var actor in selActors)
            {
                foreach (var item in actor.Value)
                {
                    Console.Write(" {0},", MovProData.ActorsDic[item]);
                }
            }
            Console.Write("\b \b\n");
        }


    }
}