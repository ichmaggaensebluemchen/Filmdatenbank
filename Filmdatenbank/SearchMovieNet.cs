using System;
using System.Collections.Generic;

namespace Filmdatenbank
{
    internal class SearchMovieNet : SearchBase
    {
        public SearchMovieNet(DataImport movProData, string movieIDstring):base(movProData)
        {
            int movieIDint;
            int.TryParse(movieIDstring, out movieIDint);
            List<int> selActors = GetActorsIDsInMovie(movieIDint);
            //Suche alle Filme in denen die Schauspieler mitgespielt haben
            Dictionary<int, List<int>> selMovies = ActorsIDsAndThereMoviesIDs(selActors);
            HashSet<int> uniqueMovies = unique(selMovies);

            PrintActors(selActors);
            PrintMovies(uniqueMovies);
        }

        //Erstellt aus der Liste der Schauspieler und ihren jeweiligen Filme   eine Filmliste ohne Doppelte Filme
        private static HashSet<int> unique(Dictionary<int, List<int>> selMovies)
        {
            HashSet<int> uniqueMovies = new HashSet<int>();
            foreach (var Movie in selMovies)
            {
                foreach (var item in Movie.Value)
                {
                    uniqueMovies.Add(item);
                }
            }

            return uniqueMovies;
        }

        //Sucht alle Schauspieler die in einem Film mitspielen
        public List<int> GetActorsIDsInMovie(int movieID)
        {
            List<int> actorsInMovieList = new List<int>();
            foreach (var item in MovProData.ActorMoviesConList)
            {
                if (item.MovieID == movieID)
                {
                    actorsInMovieList.Add(item.ActorID);
                }
            }
            return actorsInMovieList;
        }

        void PrintActors(List<int> selActors)
        {
            Console.Write("Schauspieler:");
            foreach (var actor in selActors)
            {
                Console.Write(" {0},", MovProData.ActorsDic[actor]);
            }
            Console.Write("\b \b\n");
        }

        void PrintMovies(HashSet<int> test)
        {
            Console.Write("Filme:");
            foreach (var movie in test)
            {
                Console.Write(" {0},", MovProData.MoviesDic[movie].Movie_Title);
            }
            Console.Write("\b \b\n");
        }
    }
}