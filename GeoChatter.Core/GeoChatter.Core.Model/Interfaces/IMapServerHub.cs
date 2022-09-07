using GeoChatter.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GuessServerApiInterfaces
{
    public interface IMapServerHub
    {
        public Task<int> SendGuessToClients(Guess guess);


    }  
    public interface IMapClientHub
    {
        public void ReceiveMapOptions(MapOptions mapOptions);


    }
}
