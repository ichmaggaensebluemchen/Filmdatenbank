using System.Collections.Generic;

namespace Filmdatenbank
{
    public class ActorMovies
    {
        public ActorMovies(int actorID, int movieID)
        {
            this.ActorID = actorID;
            this.MovieID = movieID;
        }
        public int ActorID { get; set; }
        public int MovieID { get; set; }
    }
}