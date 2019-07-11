using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Filmdatenbank
{
    public class DataImport
    {
        
    public DataImport(string path)
        {
            int result, result2;

            using (StreamReader reader = new StreamReader(path))
            {
                Regex rgxActor = new Regex('"' + "([0-9]*)" + '"' + "," + '"' + "*(.*?) *" + '"');
                Regex rgxMovie = new Regex('"' + "([0-9]*)" + '"' + "," + '"' + "*(.*?) *" + '"' + "," + '"' + "*(.*?) *" + '"' + "," + '"' + "*(.*?) *" + '"' + "," + '"' + "*(.*?) *" + '"' + "," + '"' + "*(.*?) *" + '"' + "," + '"' + "*(.*?) *" + '"');
                Regex rgxDirector = new Regex('"' + "([0-9]*)" + '"' + "," + '"' + "*(.*?) *" + '"');
                Regex rgxActorMovie = new Regex('"' + "([0-9]*)" + '"' + "," + '"' + "*(.*?) *" + '"');
                Regex rgxDirectorMovie = new Regex('"' + "([0-9]*)" + '"' + "," + '"' + "*(.*?) *" + '"');

                string SearchStringActorsData = "New_Entity: \"actor_id\",\"actor_name\"";
                string SearchStringMoviesData = "New_Entity: \"movie_id\",\"movie_title\",\"movie_plot\",\"genre_name\",\"movie_released\",\"movie_imdbVotes\",\"movie_imdbRating\"";
                string SearchStringDirectorsData = "New_Entity: \"director_id\",\"director_name\"";
                string SearchStringAchtorsMoviesData = "New_Entity: \"actor_id\",\"movie_id\"";
                string SearchStringDirectorsMoviesData = "New_Entity: \"director_id\",\"movie_id\"";

                var line = reader.ReadLine();
                line = reader.ReadLine();
                while (line != SearchStringMoviesData)
                {
                    Match match = rgxActor.Match(line);
                    int.TryParse(match.Groups[1].Value, out result);
                    ActorsDic.Add(result, match.Groups[2].Value);
                    line = reader.ReadLine();
                }
                line = reader.ReadLine();
                while (line != SearchStringDirectorsData)
                {
                    Match match = rgxMovie.Match(line);
                    Movie movieSet = new Movie { Movie_Title = match.Groups[2].Value, Movie_Plot = match.Groups[3].Value, Genre_Name = match.Groups[4].Value, Movie_Released = match.Groups[5].Value, Movie_imdVotes = match.Groups[6].Value, Movie_ImdbRatinge = match.Groups[7].Value };
                    var test = int.TryParse(match.Groups[1].Value, out result);
                    if (MoviesDic.ContainsKey(result))
                    {
                        MoviesErrorList.Add(match.Value);
                    }
                    else
                    {
                        MoviesDic.Add(result, movieSet);
                    }
                    line = reader.ReadLine();
                }

                line = reader.ReadLine();
                while (line != SearchStringAchtorsMoviesData)
                {
                    Match match = rgxDirector.Match(line);
                    int.TryParse(match.Groups[1].Value, out result);
                    DirectorsDic.Add(result, match.Groups[2].Value);
                    line = reader.ReadLine();
                }

                line = reader.ReadLine();
                while (line != SearchStringDirectorsMoviesData)
                {
                    if (reader.EndOfStream) break;
                    Match match = rgxActorMovie.Match(line);
                    int.TryParse(match.Groups[1].Value, out result);
                    int.TryParse(match.Groups[2].Value, out result2);
                    ActorMoviesConList.Add(new ActorMovies(result, result2));
                    line = reader.ReadLine();
                }
                line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    Match match = rgxDirectorMovie.Match(line);
                    int.TryParse(match.Groups[1].Value, out result);
                    int.TryParse(match.Groups[2].Value, out result2);
                    DirectorMoviesConList.Add(new DirectorMovies(result, result2));
                    line = reader.ReadLine();
                }
            }
        }
        public Dictionary<int, string> ActorsDic { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, Movie> MoviesDic { get; set; } = new Dictionary<int, Movie>();
        public Dictionary<int, string> DirectorsDic { get; set; } = new Dictionary<int, string>();
        public List<ActorMovies> ActorMoviesConList { get; set; } = new List<ActorMovies>();
        public List<DirectorMovies> DirectorMoviesConList { get; set; } = new List<DirectorMovies>();
        public List<String> MoviesErrorList = new List<String>();
        public List<string> ActorMovieErrorList = new List<string>();
        public List<string> DirectorMovieErrorList = new List<string>();

    }
}
