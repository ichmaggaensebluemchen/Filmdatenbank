using Microsoft.VisualStudio.TestTools.UnitTesting;
using Filmdatenbank;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Filmdatenbank.Tests
{
    [TestClass()]
    public class DataImportTests
    {
        [TestMethod()]
        public void DataImportTest()
        {
            //Arrange
            string path = @"C:\Users\Rolf\source\repos\Filmdatenbank\Filmdatenbank\Daten\movieproject2019.db";
            //Act
            DataImport MPD = new DataImport(path);
            //Assert
            int SummeTrue = MPD.ActorsDic.Count + MPD.MoviesDic.Count + MPD.DirectorsDic.Count + MPD.ActorMoviesConList.Count + MPD.DirectorMoviesConList.Count;
            int SummeFalse = MPD.MoviesErrorList.Count + MPD.ActorMovieErrorList.Count + MPD.DirectorMovieErrorList.Count;
            int AnzahlÜberschriften = 5;

            Debug.WriteLine(string.Format("Anzahl Schauspieler              : {0,6}", MPD.ActorsDic.Count.ToString()));
            Debug.WriteLine(string.Format("Anzahl Filme                     : {0,6}", MPD.MoviesDic.Count.ToString()));
            Debug.WriteLine(string.Format("Anzahl Regieseure                : {0,6}", MPD.DirectorsDic.Count.ToString()));
            Debug.WriteLine(string.Format("Anzahl Schauspieler / Filme      : {0,6}", MPD.ActorMoviesConList.Count.ToString()));
            Debug.WriteLine(string.Format("Anzahl Regieseure / Filme        : {0,6}", MPD.DirectorMoviesConList.Count.ToString()));
            Debug.WriteLine(string.Format("Summe                            : {0,6}", SummeTrue.ToString())); ;
            Debug.WriteLine("");
            Debug.WriteLine(string.Format("Mehrfache Filme                  : {0,6}", MPD.MoviesErrorList.Count.ToString()));
            Debug.WriteLine(string.Format("Mehrfache Schauspieler / Filme   : {0,6}", MPD.ActorMovieErrorList.Count.ToString()));
            Debug.WriteLine(string.Format("Mehrfache Regieseure / Filme     : {0,6}", MPD.DirectorMovieErrorList.Count.ToString()));
            Debug.WriteLine(string.Format("Summe                            : {0,6}", SummeFalse.ToString()));
            Debug.WriteLine("");
            Debug.WriteLine(string.Format("Anzahl Überschriften             : {0,6}", AnzahlÜberschriften.ToString()));
            Debug.WriteLine("");
            Debug.WriteLine(string.Format("Gesamtsumme                      : {0,6}", (SummeTrue + SummeFalse + 5).ToString()));

            Assert.IsTrue(SummeTrue + SummeFalse + 5 == 88809);
        }
    }
}