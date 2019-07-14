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

        //Sammelt für alle gefundene Schauspieler-IDs die Film-IDs ein
        public Dictionary<int, HashSet<int>> MoviesIDsAndThereActorsIDs(HashSet<int> selActors)
        {
            //Auflistung aller Schauspieler mit den Filmen in denen sie mitgespielt haben
            Dictionary<int, HashSet<int>> oneActorManyMoviesDic = new Dictionary<int, HashSet<int>>();
            //Schleife über alle gefundenen Schauspieler
            foreach (var actor in selActors)
            {
                oneActorManyMoviesDic.Add(actor, MovProData.ActorMoviesDic[actor]);
            }

            return oneActorManyMoviesDic;
        }

        public void PrintMovieDetails(HashSet<int> movies)
        {
            foreach (var movie in movies)
            {
                Console.WriteLine("ID:                {0}", movie);
                Console.WriteLine("Titel           :  {0}", MovProData.MoviesDic[movie].Movie_Title);
                Console.WriteLine("Kurzbeschreibung:  {0}", MovProData.MoviesDic[movie].Movie_Plot);
                Console.WriteLine("Genre:             {0}", MovProData.MoviesDic[movie].Genre_Name);
                Console.WriteLine("Erscheinungsdatum: {0}", MovProData.MoviesDic[movie].Movie_Released);
                Console.WriteLine("IMDB Wertung:      {0}", MovProData.MoviesDic[movie].Movie_imdVotes);
                Console.WriteLine("IMDB Platzierung:  {0}", MovProData.MoviesDic[movie].Movie_ImdbRatinge);
                //Console.WriteLine("Regieseur:  {0}", MovProData.MovieDirectorsDic[movie].);
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