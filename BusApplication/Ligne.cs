using System.Collections.Generic;
using System.Threading;

namespace BusApplication
{
    public class Ligne
    {
        private List<Station> stations;
        private int nbStations;

        public Ligne(int nbStations)
        {
            this.nbStations = nbStations;
            stations = new List<Station>();
            Thread th;
            Station s;
            for (int i = 0; i < nbStations; i++)
            {
                s = new Station();
                th = new Thread(s.vieStation);
                stations.Add(s);
                th.Start();
            }
        }

        public Station getStation(int id)
        {
            foreach (Station s in stations)
            {
                if (s.getId() == id)
                {
                    return s;
                }
            }
            return null;
        }
        
    }
}