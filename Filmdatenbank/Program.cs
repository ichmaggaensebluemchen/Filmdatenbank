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
                //string path = @"C:\Users\rolf\Source\Repos\ichmaggaensebluemchen\Filmdatenbank\Filmdatenbank\Daten\movieproject2019.db";
                DataImport MovProData = new DataImport(path);              
                string arg = args[0].ToString();
                Match searchArgument = Regex.Match(arg, "--(.*?)=(.*?)$");
                string searchTyp = searchArgument.Groups[1].Value;
                string searchTypArg = searchArgument.Groups[2].Value;
                switch (searchTyp)
                {
                    case "filmsuche":
                        SearchMovie movieSearch = new SearchMovie(MovProData, searchTypArg);
                        break;
                    case "schauspielersuche":
                        SearchActor actorSearch = new SearchActor(MovProData, searchTypArg);
                        break;
                    case "filmnetzwerk":
                        SearchMovieNet searchMovieNet = new SearchMovieNet(MovProData, searchTypArg);
                        break;
                    case "schauspielernetzwerk":
                        SearchActorNet searchActorNet = new SearchActorNet(MovProData, searchTypArg);
                        break;
                    default:
                        defaultSearch(searchTypArg);
                        break;
                }
                Console.WriteLine("\nBeenden Sie das Programm mit 'Enter'");
                Console.Read();
            }
        }
        private static void defaultSearch(string searchTypArg)
        {
            Console.WriteLine("Die Angaben in der Komandzeile entsprechen keiner gültige Abfrage!");
            Console.WriteLine("\nBeispiele für gültige Abfragen sind:");
            Console.WriteLine("Filme suchen:                    --filmsuche=\"Matrix\"");
            Console.WriteLine("Schauspieler suchen:             --schauspielersuche=\"Smith\"");
            Console.WriteLine("Filmnetzwerk anzeigen:           --filmnetzwerk=4899");
            Console.WriteLine("Schauspielernetzwerk anzeigen:   --schauspielernetzwerk=17562");
            Console.WriteLine("\nBeenden Sie das Programm mit 'Enter' und versuchen SIe es erneut!");
            Console.Read();
        }

    }
}
