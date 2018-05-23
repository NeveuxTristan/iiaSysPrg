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

        public Station()
        {
            id = ID_REFERENCE++;
            nbClientAttente = Math.Max(r.Next(10), r.Next(5));
        }

        public void refillAttente()
        {
            nbClientAttente = Math.Max(r.Next(10), r.Next(5));
        }

        public void vieStation()
        {
            while (isRunning)
            {
                Thread.Sleep(3000);
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