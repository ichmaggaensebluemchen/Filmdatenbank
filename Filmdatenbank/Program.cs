using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace Filmdatenbank
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args[0] != String.Empty)
            {
                string path = @"C:\Users\Rolf\source\repos\Filmdatenbank\Filmdatenbank\Daten\movieproject2019.db";
                DataImport MovProData = new DataImport(path);
                string arg = args[0].ToString();
                Match searchArgument = Regex.Match(arg, "--(.*?)=(.*?)$");
                string searchTyp = searchArgument.Groups[1].Value;
                string searchTypArg = searchArgument.Groups[2].Value;
                switch (searchTyp)
                {
                    case "filmsuche":
                        SearchMovie movieSearch = new SearchMovie(MovProData);
                        var selMovies = movieSearch.Filter(searchTypArg);
                        movieSearch.Print(selMovies, searchTypArg);
                        break;
                    case "schauspielersuche":
                        SearchActor actorSearch = new SearchActor(MovProData);
                        var selActors = actorSearch.Filter(searchTypArg);
                        actorSearch.Print(selActors, searchTypArg);
                        break;
                    case "filmnetzwerk":
                        movieNetSearch(searchTypArg);
                        break;
                    case "schauspielernetzwerk":
                        movieNetSearch(searchTypArg);
                        break;
                    default:
                        defaultSearch(searchTypArg);
                        break;
                }
            }
        }
        private static void actorSearch(string searchTypArg)
        {
            int testc = 0;
        }
        private static void movieNetSearch(string searchTypArg)
        {
            int testc = 0;
        }
        private static void actorNetSearch(string searchTypArg)
        {
            int testc = 0;
        }
        private static void defaultSearch(string searchTypArg)
        {
            int testc = 0;
        }

    }
}
