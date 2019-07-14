using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filmdatenbank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Filmdatenbank.Tests
{
    [TestClass()]
    public class DataImportTests
    {
        //Arrange
        private string path = @"C:\Users\Rolf\source\repos\Filmdatenbank\Filmdatenbank\Daten\movieproject2019.db";
        //[TestMethod()]
        //public void DataImportTest()
        //{
        //    //Act
        //    DataImport MPD = new DataImport(path);
        //    //Assert
        //    int SummeTrue = MPD.ActorsDic.Count + MPD.MoviesDic.Count + MPD.DirectorsDic.Count + MPD.ActorsMoviesConList.Count + MPD.DirectorMoviesConList.Count;
        //    int SummeFalse = MPD.MoviesErrorList.Count + MPD.ActorMovieErrorList.Count + MPD.DirectorMovieErrorList.Count;
        //    int AnzahlÜberschriften = 5;
        //    Debug.WriteLine(string.Format("Anzahl Schauspieler              : {0,6}", MPD.ActorsDic.Count.ToString()));
        //    Debug.WriteLine(string.Format("Anzahl Filme                     : {0,6}", MPD.MoviesDic.Count.ToString()));
        //    Debug.WriteLine(string.Format("Anzahl Regieseure                : {0,6}", MPD.DirectorsDic.Count.ToString()));
        //    Debug.WriteLine(string.Format("Anzahl Schauspieler / Filme      : {0,6}", MPD.ActorsMoviesConList.Count.ToString()));
        //    Debug.WriteLine(string.Format("Anzahl Regieseure / Filme        : {0,6}", MPD.DirectorMoviesConList.Count.ToString()));
        //    Debug.WriteLine(string.Format("Summe                            : {0,6}", SummeTrue.ToString())); ;
        //    Debug.WriteLine("");
        //    Debug.WriteLine(string.Format("Mehrfache Filme                  : {0,6}", MPD.MoviesErrorList.Count.ToString()));
        //    Debug.WriteLine(string.Format("Mehrfache Schauspieler / Filme   : {0,6}", MPD.ActorMovieErrorList.Count.ToString()));
        //    Debug.WriteLine(string.Format("Mehrfache Regieseure / Filme     : {0,6}", MPD.DirectorMovieErrorList.Count.ToString()));
        //    Debug.WriteLine(string.Format("Summe                            : {0,6}", SummeFalse.ToString()));
        //    Debug.WriteLine("");
        //    Debug.WriteLine(string.Format("Anzahl Überschriften             : {0,6}", AnzahlÜberschriften.ToString()));
        //    Debug.WriteLine("");
        //    Debug.WriteLine(string.Format("Gesamtsumme                      : {0,6}", (SummeTrue + SummeFalse + 5).ToString()));
        //    Assert.IsTrue(SummeTrue + SummeFalse + 5 == 88809);
        //}

        [TestMethod]
        public void ActorsCountTest()
        {
            //Arrange
            HashSet<int> actorIDHashSet = new HashSet<int>();
            Dictionary<int, string> ActorsDic = new Dictionary<int, string>();
            int actorID;
            Regex rgxActor = new Regex('"' + "([0-9]*)" + '"' + "," + '"' + " *(.*?) *" + '"');
            string SearchStringMoviesData = "New_Entity: \"movie_id\",\"movie_title\",\"movie_plot\",\"genre_name\",\"movie_released\",\"movie_imdbVotes\",\"movie_imdbRating\"";
            StreamReader reader = new StreamReader(path);

            var line = reader.ReadLine();
            line = reader.ReadLine();
            while (line != SearchStringMoviesData)
            {
                Match match = rgxActor.Match(line);
                int.TryParse(match.Groups[1].Value, out actorID);
                ActorsDic.Add(actorID, match.Groups[2].Value);
                actorIDHashSet.Add(actorID);
                line = reader.ReadLine();
            }
            Debug.WriteLine("Gesamtzahl Einträge: {0}", ActorsDic.Count);
            Debug.WriteLine("Gesamtzahl eindeutiger Schauspieler: {0}", actorIDHashSet.Count);

            //Assert
            Assert.IsTrue(ActorsDic.Count == actorIDHashSet.Count);
        }

        [TestMethod]
        public void MouviesCountTest()
        {
            //Arrange
            List<String> MoviesErrorList = new List<String>();
            Dictionary<int, Movie> MoviesDic = new Dictionary<int, Movie>();
            int movieID;
            Regex rgxMovie = new Regex('"' + "([0-9]*)" + '"' + "," + '"' + " *(.*?) *" + '"' + "," + '"' + " *(.*?) *" + '"' + "," + '"' + " *(.*?) *" + '"' + "," + '"' + " *(.*?) *" + '"' + "," + '"' + " *(.*?) *" + '"' + "," + '"' + " *(.*?) *" + '"');
            string SearchStringMoviesData = "New_Entity: \"movie_id\",\"movie_title\",\"movie_plot\",\"genre_name\",\"movie_released\",\"movie_imdbVotes\",\"movie_imdbRating\"";
            string SearchStringDirectorsData = "New_Entity: \"director_id\",\"director_name\"";
            StreamReader reader = new StreamReader(path);

            var line = reader.ReadLine();
            while (line != SearchStringMoviesData) { line = reader.ReadLine(); }
            line = reader.ReadLine();
            while (line != SearchStringDirectorsData)
            {
                Match match = rgxMovie.Match(line);
                Movie movieSet = new Movie { Movie_Title = match.Groups[2].Value, Movie_Plot = match.Groups[3].Value, Genre_Name = match.Groups[4].Value, Movie_Released = match.Groups[5].Value, Movie_imdVotes = match.Groups[6].Value, Movie_ImdbRatinge = match.Groups[7].Value };
                var test = int.TryParse(match.Groups[1].Value, out movieID);
                if (MoviesDic.ContainsKey(movieID))
                {
                    MoviesErrorList.Add(match.Value);
                }
                else
                {
                    MoviesDic.Add(movieID, movieSet);
                }
                line = reader.ReadLine();
            }
            Debug.WriteLine("Gesamtzahl Einträge: {0}", MoviesDic.Count + MoviesErrorList.Count);
            Debug.WriteLine("Gesamtzahl mehrfach vorkommender Filme: {0}", MoviesErrorList.Count);
            Debug.WriteLine("Gesamtzahl eindeutiger Filme: {0}", MoviesDic.Count);

            //Assert
            Assert.IsFalse(MoviesDic.Count == MoviesErrorList.Count);
        }


        [TestMethod]
        public void DirectorsCountTest()
        {
            //Arrange
            HashSet<int> directorsIDHashSet = new HashSet<int>();
            Dictionary<int, string> DirectorsDic = new Dictionary<int, string>();
            int directorID;
            Regex rgxDirector = new Regex('"' + "([0-9]*)" + '"' + "," + '"' + " *(.*?) *" + '"');
            string SearchStringDirectorsData = "New_Entity: \"director_id\",\"director_name\"";
            string SearchStringAchtorsMoviesData = "New_Entity: \"actor_id\",\"movie_id\"";
            StreamReader reader = new StreamReader(path);

            var line = reader.ReadLine();
            while (line != SearchStringDirectorsData) { line = reader.ReadLine(); }
            line = reader.ReadLine();
            while (line != SearchStringAchtorsMoviesData)
            {
                Match match = rgxDirector.Match(line);
                int.TryParse(match.Groups[1].Value, out directorID);
                DirectorsDic.Add(directorID, match.Groups[2].Value);
                directorsIDHashSet.Add(directorID);
                line = reader.ReadLine();
            }
            Debug.WriteLine("Gesamtzahl Einträge: {0}", DirectorsDic.Count);
            Debug.WriteLine("Gesamtzahl eindeutiger Regiseure: {0}", directorsIDHashSet.Count);

            //Assert
            Assert.IsTrue(DirectorsDic.Count == directorsIDHashSet.Count);
        }

        [TestMethod]
        public void ActorsIDMoviesIDCountsTest()
        {
            //Arrange
            HashSet<int> actorIDHashSet = new HashSet<int>();
            HashSet<int> movieIDHashSet = new HashSet<int>();
            List<DirectorMovies> ActorMoviesConList = new List<DirectorMovies>();
            int actorID, movieID;
            Regex rgxActorMovie = new Regex('"' + "([0-9]*)" + '"' + "," + '"' + " *(.*?) *" + '"');
            string SearchStringAchtorsMoviesData = "New_Entity: \"actor_id\",\"movie_id\"";
            string SearchStringDirectorsMoviesData = "New_Entity: \"director_id\",\"movie_id\"";
            StreamReader reader = new StreamReader(path);
            var line = reader.ReadLine();
            while (line != SearchStringAchtorsMoviesData) { line = reader.ReadLine(); }
            line = reader.ReadLine();
            while (line != SearchStringDirectorsMoviesData)
            {
                Match match = rgxActorMovie.Match(line);
                int.TryParse(match.Groups[1].Value, out actorID);
                int.TryParse(match.Groups[2].Value, out movieID);
                ActorMoviesConList.Add(new DirectorMovies(actorID, movieID));
                actorIDHashSet.Add(actorID);
                movieIDHashSet.Add(movieID);
                line = reader.ReadLine();
            }
            //Assert
            Debug.WriteLine("Gesamtzahl Einträge: {0}", ActorMoviesConList.Count);
            Debug.WriteLine("Gesamtzahl eindeutiger Schauspieler: {0}", actorIDHashSet.Count);
            Debug.WriteLine("Gesamtzahl eindeutiger Filme: {0}", movieIDHashSet.Count);

            Assert.IsFalse(ActorMoviesConList.Count == actorIDHashSet.Count);
        }

        [TestMethod]
        public void DirectorsIDMoviesIDCountsTest()
        {
            //Arrange
            HashSet<int> directorIDHashSet = new HashSet<int>();
            HashSet<int> movieIDHashSet = new HashSet<int>();
            List<DirectorMovies> DirectorMoviesConList = new List<DirectorMovies>();
            int directorID, movieID;
            Regex rgxDirectorMovie = new Regex('"' + "([0-9]*)" + '"' + "," + '"' + " *(.*?) *" + '"');
            string SearchStringDirectorsMoviesData = "New_Entity: \"director_id\",\"movie_id\"";
            StreamReader reader = new StreamReader(path);
            var line = reader.ReadLine();
            while (line != SearchStringDirectorsMoviesData) { line = reader.ReadLine(); }
            line = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                Match match = rgxDirectorMovie.Match(line);
                int.TryParse(match.Groups[1].Value, out directorID);
                int.TryParse(match.Groups[2].Value, out movieID);
                DirectorMoviesConList.Add(new DirectorMovies(directorID, movieID));
                directorIDHashSet.Add(directorID);
                movieIDHashSet.Add(movieID);
                line = reader.ReadLine();
            }
            //Assert
            Debug.WriteLine("Gesamtzahl Einträge: {0}", DirectorMoviesConList.Count);
            Debug.WriteLine("Gesamtzahl eindeutiger Regiseure: {0}", directorIDHashSet.Count);
            Debug.WriteLine("Gesamtzahl eindeutiger Filme: {0}", movieIDHashSet.Count);

            Assert.IsFalse(DirectorMoviesConList.Count == directorIDHashSet.Count);
        }
    }
}