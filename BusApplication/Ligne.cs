using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BusApplication
{
    public class Ligne
    {
        private List<Station> stations;
        private int nbStations;

        public Bus bus;

        public Ligne(int nbStations, Bus bus)
        {
            Console.WriteLine("Création d'une ligne de " + nbStations + " stations");
            this.nbStations = nbStations;
            this.bus = bus;
            stations = new List<Station>();
            Thread th;
            Station s;
            for (int i = 0; i < nbStations; i++)
            {
                s = new Station(bus);
                th = new Thread(s.vieStation);
                stations.Add(s);
                th.Start();
            }
        }

        public Station getStationAtPosition(int id)
        {
            return stations.ElementAt(id);
        }

        public void revertLigne()
        {
            List<Station> stationsRevert = new List<Station>();
            for (int i = stations.Count - 1; i <= 0; i--)
            {
                Station s = stations.ElementAt(i);
                s.refillAttente();
                stationsRevert.Add(s);
            }
            stations.Clear();
            stations = stationsRevert;
        }
    }
}