namespace Filmdatenbank
{
    public class DirectorMovies
    {
        private int result;
        private int result2;

        public DirectorMovies(int result, int result2)
        {
            this.result = result;
            this.result2 = result2;
        }
        public int DirectorID { get; set; }
        public int MovieID { get; set; }
    }
}