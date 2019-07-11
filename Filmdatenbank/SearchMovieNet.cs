using System;

namespace Filmdatenbank
{
    internal class SearchMovieNet : SearchBase
    {
        public SearchMovieNet(DataImport movProData):base(movProData)
        {
            //MovProData = movProData;
        }

        internal object Filter(string searchTypArg)
        {
            throw new NotImplementedException();
        }

        internal void Print(object selMovieNet, string searchTypArg)
        {
            throw new NotImplementedException();
        }
    }
}