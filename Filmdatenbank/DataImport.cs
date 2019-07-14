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
            int key, value;

            using (StreamReader reader = new StreamReader(path))
            {
                Regex rgxActor = new Regex          ('"' + "([0-9]*)" + '"' + "," + '"' + " *(.*?) *" + '"');
                Regex rgxMovie = new Regex          ('"' + "([0-9]*)" + '"' + "," + '"' + " *(.*?) *" + '"' + "," + '"' + " *(.*?) *" + '"' + "," + '"' + " *(.*?) *" + '"' + "," + '"' + " *(.*?) *" + '"' + "," + '"' + " *(.*?) *" + '"' + "," + '"' + " *(.*?) *" + '"');
                Regex rgxDirector = new Regex       ('"' + "([0-9]*)" + '"' + "," + '"' + " *(.*?) *" + '"');
                Regex rgxActorMovie = new Regex     ('"' + "([0-9]*)" + '"' + "," + '"' + " *(.*?) *" + '"');
                Regex rgxDirectorMovie = new Regex  ('"' + "([0-9]*)" + '"' + "," + '"' + " *(.*?) *" + '"');

                string SearchStringActorsData =             "New_Entity: \"actor_id\",\"actor_name\"";
                string SearchStringMoviesData =             "New_Entity: \"movie_id\",\"movie_title\",\"movie_plot\",\"genre_name\",\"movie_released\",\"movie_imdbVotes\",\"movie_imdbRating\"";
                string SearchStringDirectorsData =          "New_Entity: \"director_id\",\"director_name\"";
                string SearchStringAchtorsMoviesData =      "New_Entity: \"actor_id\",\"movie_id\"";
                string SearchStringDirectorsMoviesData =    "New_Entity: \"director_id\",\"movie_id\"";

                //Schauspieler einlesen 1:1
                var line = reader.ReadLine();
                line = reader.ReadLine();
                while (line != SearchStringMoviesData)
                {
                    Match match = rgxActor.Match(line);
                    int.TryParse(match.Groups[1].Value, out key);
                    ActorsDic.Add(key, match.Groups[2].Value);
                    line = reader.ReadLine();
                }

                //Filme einlesen 1:n
                line = reader.ReadLine();
                while (line != SearchStringDirectorsData)
                {
                    Match match = rgxMovie.Match(line);
                    Movie movieSet = new Movie { Movie_Title = match.Groups[2].Value, Movie_Plot = match.Groups[3].Value, Genre_Name = match.Groups[4].Value, Movie_Released = match.Groups[5].Value, Movie_imdVotes = match.Groups[6].Value, Movie_ImdbRatinge = match.Groups[7].Value };
                    var test = int.TryParse(match.Groups[1].Value, out key);
                    if (!MoviesDic.ContainsKey(key))
                    {
                        MoviesDic.Add(key, movieSet);
                    }
                    line = reader.ReadLine();
                }

                //Regieseure einlesen 1:1
                line = reader.ReadLine();
                while (line != SearchStringAchtorsMoviesData)
                {
                    Match match = rgxDirector.Match(line);
                    int.TryParse(match.Groups[1].Value, out key);
                    DirectorsDic.Add(key, match.Groups[2].Value);
                    line = reader.ReadLine();
                }

                //Schauspieler/Filme einlesen m:n
                line = reader.ReadLine();
                while (line != SearchStringDirectorsMoviesData)
                {
                    Match match = rgxActorMovie.Match(line);
                    int.TryParse(match.Groups[1].Value, out key);
                    int.TryParse(match.Groups[2].Value, out value);
                    ActorsMoviesConList.Add(new ActorMovies(key, value));

                    //Schauspieler auslesen und in ActorMovies einsetzen
                    KeyValues(ActorMoviesDic, key, value);

                    //Film auslesen und in MovieActors einsetzen
                    KeyValues(MovieActorsDic, value, key);

                    line = reader.ReadLine();
                }

                //Regieseure/Filme einlesen m:n
                line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    Match match = rgxDirectorMovie.Match(line);
                    int.TryParse(match.Groups[1].Value, out key);
                    int.TryParse(match.Groups[2].Value, out value);
                    DirectorsMoviesConList.Add(new DirectorMovies(key, value));

                    //Regiseur auslesen und in DirectorMovies einsetzen
                    KeyValues(DirectorMoviesDic, key, value);

                    //Film auslesen und in MovieDirectors einsetzen
                    KeyValues(MovieDirectorsDic, value, key);

                    line = reader.ReadLine();
                }
            }
        }

        //Fügt HashSet Werte einem Dictionary HashSetValue hinzu d.h. 1:n -> ein n hinzufügen
        private void KeyValues(Dictionary<int, HashSet<int>> dic, int key, int value)
        {
            if (!dic.ContainsKey(key))
            {
                var MovieSet = new HashSet<int>();
                MovieSet.Add(value);
                dic.Add(key, MovieSet);
            }
            else
            {
                dic[key].Add(value);
            }
        }

        public Dictionary<int, string> ActorsDic { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, Movie> MoviesDic { get; set; } = new Dictionary<int, Movie>();
        public Dictionary<int, string> DirectorsDic { get; set; } = new Dictionary<int, string>();
        public List<ActorMovies> ActorsMoviesConList { get; set; } = new List<ActorMovies>();
        public Dictionary<int, HashSet<int>> ActorMoviesDic { get; set; } = new Dictionary<int, HashSet<int>>();
        public Dictionary<int, HashSet<int>> MovieActorsDic { get; set; } = new Dictionary<int, HashSet<int>>();
        public List<DirectorMovies> DirectorsMoviesConList { get; set; } = new List<DirectorMovies>();
        public Dictionary<int, HashSet<int>> DirectorMoviesDic { get; set; } = new Dictionary<int, HashSet<int>>();
        public Dictionary<int, HashSet<int>> MovieDirectorsDic { get; set; } = new Dictionary<int, HashSet<int>>();

    }
}
