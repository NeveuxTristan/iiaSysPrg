using System;
using System.Threading;

namespace BusApplication
{
    public class Station
    {
        private static int ID_REFERENCE = 1;

        private Boolean isRunning = true;
        private int id;
        private int nbClientAttente;
        private Random r = new Random();

        private Bus bus;

        public Station(Bus bus)
        {
            this.bus = bus;
            id = ID_REFERENCE++;
            refillAttente();

            Console.WriteLine("Station n°" + id + " créée avec " + nbClientAttente + " personnes en attentes.");
        }

        public void refillAttente()
        {
            nbClientAttente = r.Next(0,8);
            Thread.Sleep(10);
        }

        public void vieStation()
        {
            while (isRunning)
            {
                Thread.Sleep(2000);
                if (r.Next(1000) < r.Next(500))
                {
                    nbClientAttente++;
                }
            }
        }

        public void shutdown()
        {
            isRunning = false;
        }

        public int getId()
        {
            return id;
        }

        public int getNbClient()
        {
            return nbClientAttente;
        }
    }
}