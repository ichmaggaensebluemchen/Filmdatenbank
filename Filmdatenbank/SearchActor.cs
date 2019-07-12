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
            HashSet<int> selActors = PatternName(Filter);
            Dictionary<int, HashSet<int>> foo = ActorsIDsAndThereMoviesIDs(selActors);
            Print(foo, Filter);
        }

        //Gibt eine Liste vom Schauspieler-IDs zurück in deren Namensangaben das mit 'Filter' angegebene Wort vorkommt
        public HashSet<int> PatternName(string filter)
        {
            HashSet<int> filteredActors = new HashSet<int>();
            foreach (var film in MovProData.ActorsDic)
            {
                if (film.Value.Contains(filter))
                {
                    filteredActors.Add(film.Key);
                }
            }
            return filteredActors;  //Liste von Schauspielern deren Name das Suchwort enthält
        }

        //Ausgabe der gefundenen Schauspieler und aller Filme in denen Sie mitgespielt haben
        public void Print(Dictionary<int, HashSet<int>> actorsAndThereMoviesDic, string filter)
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
