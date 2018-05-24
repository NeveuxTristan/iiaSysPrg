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
                Thread.Sleep(5000);
                if (r.Next(1000) < r.Next(200))
                {
                    increment();
                }
            }
        }

        private void increment()
        {
            Interlocked.Increment(ref nbClientAttente);
        }
        
        private void decrement()
        {
            Interlocked.Decrement(ref nbClientAttente);
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

        public void busArrive()
        {
            bool secu = true;
            Client cl ;
            Console.WriteLine("----- STATION N°"+id+" ----- CLIENTS EN ATTENTE ("+nbClientAttente+") -----");
            Console.WriteLine("Le bus est arrivé à la station n°"+id);
            Thread.Sleep(2500);
            while (secu && nbClientAttente > 0 || bus.nbClientAttendSortie() >0)
            {
                cl = new Client(bus);
                if (!bus.enterCl(cl))
                {
                    cl.terminate();
                    secu = false;
                }
                else
                {
                    decrement();
                    Thread.Sleep(500);
                    if (nbClientAttente == 0)
                    { 
                        secu = false;
                    }
                }
            }
        }
    }
}