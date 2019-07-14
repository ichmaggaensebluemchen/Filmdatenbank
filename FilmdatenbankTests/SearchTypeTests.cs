using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filmdatenbank.Tests
{
    [TestClass()]
    public class SearchTypeTests
    {
        //Arrange
        private DataImport MPD = new DataImport(@"C:\Users\Rolf\source\repos\Filmdatenbank\Filmdatenbank\Daten\movieproject2019.db");

        [TestMethod]
        public void FilmeSuchen()
        {
            //Act
            SearchMovie movieSearch = new SearchMovie(MPD, "Matrix");
            //Assert
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void SchauspielerSuchen()
        {
            SearchActor actorSearch = new SearchActor(MPD, "Smith");
            //Assert
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void FilmnetzwerkAnzeigen()
        {
            SearchMovieNet searchMovieNet = new SearchMovieNet(MPD, "4899");
            //Assert
            Assert.IsTrue(true);
        }
        [TestMethod]
        public void SchauspielernetzwerkAnzeigen()
        {
            SearchActorNet searchActorNet = new SearchActorNet(MPD, "17562");
            //Assert
            Assert.IsTrue(true);
        }

    }
}
