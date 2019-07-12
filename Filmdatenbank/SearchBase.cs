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
        public HashSet<int> PatternBase(string filter)
        {
            HashSet<int> filteredList = new HashSet<int>();
            foreach (var item in MovProData.MoviesDic)
            {
                if (item.Value.Movie_Title.Contains(filter))
                {
                    filteredList.Add(item.Key);
                }
            }
            return filteredList;  //Liste von Schauspielern deren Name das Suchwort enthält
        }

        //Gibt eine Liste von Film IDs zurück in welchen ein bestimmter Schauspieler gespielt hat (bei Actor ist False)
        //Gibt eine Liste von Schauspieler IDs zurück welche in einem Film mitgespielt haben (bei Actor ist True)
        public HashSet<int> ActorOrMovieList(int actorOrMovieID, bool IsActor)
        {
            HashSet<int> actorsOrMovies = new HashSet<int>();
            if (IsActor)
            {
                foreach (var actorMovieCon in MovProData.ActorMoviesConList)
                {
                    if (actorMovieCon.ActorID == actorOrMovieID)
                    {
                        actorsOrMovies.Add(actorMovieCon.MovieID);
                    }
                }
            }
            else
            {
                foreach (var actorMovieCon in MovProData.ActorMoviesConList)
                {
                    if (actorMovieCon.MovieID == actorOrMovieID)
                    {
                        actorsOrMovies.Add(actorMovieCon.ActorID);
                    }
                }

            }
            return actorsOrMovies;
        }

        //Sammelt für alle gefundene Namens-ID die Film-IDs ein
        public Dictionary<int, HashSet<int>> ActorsIDsAndThereMoviesIDs(HashSet<int> selMovies)
        {
            Dictionary<int, HashSet<int>> oneActorManyMovies = new Dictionary<int, HashSet<int>>();
            foreach (var movie in selMovies)
            {
                foreach (var item in MovProData.ActorMoviesConList)
                {
                    if (item.MovieID == movie)
                    {
                        if (!oneActorManyMovies.ContainsKey(movie))
                        {
                            oneActorManyMovies.Add(movie, ActorOrMovieList(movie, false));
                        }
                    }
                }
            }
            return oneActorManyMovies;
        }

        public void PrintMovies(HashSet<int> movies)
        {
            foreach (var movie in movies)
            {
                Console.WriteLine("ID {0} - {1}", movie, MovProData.MoviesDic[movie].Movie_Title);
                Console.WriteLine("{0}", MovProData.MoviesDic[movie].Movie_Plot);
                Console.WriteLine();
            }
        }

        public void PrintNet(HashSet<int> selection, string headline, object dicObj)
        {
            bool isMovie = dicObj is Dictionary<int, Movie>;

            Console.Write(headline);
            foreach (var item in selection)
            {
                if (isMovie)
                {
                    var dic = dicObj as Dictionary<int, Movie>;
                    Console.Write(" {0},", dic[item].Movie_Title);
                }
                else
                {
                    var dic = dicObj as Dictionary<int, string>;
                    Console.Write(" {0},", dic[item]);
                }
            }
            Console.Write("\b \b\n");
        }

        //Erstellt aus der Liste der Schauspieler und ihren jeweiligen Filme eine Filmliste ohne Doppelte Filme
        public HashSet<int> unique(Dictionary<int, HashSet<int>> selMovies)
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

        public DataImport MovProData { get; set; }
    }
}