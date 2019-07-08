using System.Collections.Generic;

namespace Filmdatenbank
{
    public class ActorMovies
    {
        private int result;
        private int result2;

        public ActorMovies(int result, int result2)
        {
            this.result = result;
            this.result2 = result2;
        }

        public int ActorID { get; set; }
        public int MovieID { get; set; }
    }
}