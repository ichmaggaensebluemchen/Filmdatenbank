using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmdatenbank
{
    class SearchActor
    {
        private DataImport movProData;
        private List<int> filteredActors = new List<int>();
        private List<int> filteredActorMovies = new List<int>();

        public SearchActor(DataImport movProData)
        {
            this.movProData = movProData;
        }

        public List<int> Filter(string filter)
        {
            foreach (var film in movProData.DirectorsDic)
            {
                if (film.Value.Contains(filter))
                {
                    filteredActors.Add(film.Key);
                }
            }
            return filteredActors;
        }

        public List<int> OneActorManyMovies(int actor)
        {
            List<int> movies = new List<int>();
            foreach (var actorMovie in movProData.ActorMoviesConList)
            {
                if (actorMovie.ActorID==actor)
                {
                    movies.Add(actorMovie.MovieID);
                }
            }


            return movies;
        }
        public void Print(List<int> actors, string filter)
        {
            Console.WriteLine("Die Suche nach '{0}' ergab {1} Treffer:", filter, actors.Count);
            Console.WriteLine("-----------------------------------------------------------");
            foreach (var actor in actors)
            {
                //Console.WriteLine("ID {0} - {1}", actor, movProData.MoviesDic[actor].Movie_Title);
                Console.WriteLine("ID {0} - {1}", actor, movProData.MoviesDic[actor].Movie_Title);
                Console.WriteLine("{0}", movProData.MoviesDic[actor].Movie_Plot);
                Console.WriteLine();
            }
        }
    }
}
