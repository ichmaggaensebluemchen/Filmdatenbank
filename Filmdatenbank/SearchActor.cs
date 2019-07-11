using System;
using System.Collections.Generic;

namespace Filmdatenbank
{
    /// <summary>
    /// Ermittelt 
    /// </summary>
    class SearchActor : SearchBase
    {
        public SearchActor(DataImport movProData, string Filter):base(movProData)
        {
            List<int> selActors = PatternName(Filter);
            Dictionary<int, List<int>> foo = ActorsAndThereMovies(selActors);
            Print(foo, Filter);
        }

        //Gibt eine Liste vom Schauspieler-IDs zurück in deren Namensangaben das mit 'Filter' angegebene Wort vorkommt
        public List<int> PatternName(string filter)
        {
            List<int> filteredActors = new List<int>();
            foreach (var film in MovProData.ActorsDic)
            {
                if (film.Value.Contains(filter))
                {
                    filteredActors.Add(film.Key);
                }
            }
            return filteredActors;  //Liste von Schauspielern deren Name das Suchwort enthält
        }

        //Gibt eine Liste vom Film IDs zurück in welchen ein bestimmter Schauspieler gespielt hat
        public List<int> OneActorManyMovies(int actor)
        {
            List<int> movies = new List<int>();
            foreach (var actorMovie in MovProData.ActorMoviesConList)
            {
                if (actorMovie.ActorID == actor)
                {
                    movies.Add(actorMovie.MovieID);
                }
            }
            return movies;  //Liste von Film-IDs in denen ein bestimmter Schauspieler gespielt hat
        }

        //Sammelt für alle gefundene Namens-ID die Film-IDs ein
        public Dictionary<int, List<int>> ActorsAndThereMovies(List<int> filteredActors)
        {
            Dictionary<int, List<int>> actorsAndThereMoviesDic = new Dictionary<int, List<int>>();
            foreach (var item in filteredActors)
            {
                actorsAndThereMoviesDic.Add(item, OneActorManyMovies(item));
            }
            return actorsAndThereMoviesDic;
        }

        //Ausgabe der gefundenen Schauspieler und aller Filme in denen Sie mitgespielt haben
        public void Print(Dictionary<int, List<int>> actorsAndThereMoviesDic, string filter)
        {
            Console.WriteLine("Die Suche nach '{0}' ergab {1} Treffer:", filter, actorsAndThereMoviesDic.Count);
            Console.WriteLine("-----------------------------------------------------------");
            foreach (var item in actorsAndThereMoviesDic)
            {
                Console.WriteLine("---------------{0}------------", MovProData.ActorsDic[item.Key]);
                PrintMovies(item.Value);
            }
        }
    }
}
