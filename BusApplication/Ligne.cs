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

            this.bus.setLigne(this);
        }

        public List<Station> getStations()
        {
            return stations;
        }

        public void revertLigne()
        {
            List<Station> stationsRevert = new List<Station>();

            // ON inverse le sens de la ligne - on ignore donc la station actuelle càd la dernière station.
            foreach (var st in stations)
            {
                if (st.getId() != stations.Count)
                {
                    st.refillAttente();
                    stationsRevert.Add(st);
                }
                else
                {
                    st.shutdown();
                }
            }
            stations.Clear();
            stationsRevert.Reverse();
            stations = stationsRevert;
        }
    }
}