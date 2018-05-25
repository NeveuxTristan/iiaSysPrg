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

        /**
         * Remplis la file d'attente
         */
        public void refillAttente()
        {
            nbClientAttente = r.Next(0, 8);
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

        /**
         * Annonce l'arrivé d'un bus dans la statoin
         * Si le bus est plein et des clients veulent sortir, on attend qu'un client sorte
         * sinon on essaye de faire rentrer les clients de la station
         * Si un client ne peut pas rentrer c'est que le bus est plein
         * On léve donc la sécurité
         */
        public void busArrive()
        {
            bool secu = true;
            Client cl;
            Console.WriteLine("----- STATION N°" + id + " ----- CLIENTS EN ATTENTE (" + nbClientAttente + ") -----");
            Thread.Sleep(2500);
            while (secu && nbClientAttente > 0 )
            {
                if (bus.isFull() && bus.nbClientAttendSortie() >0 )
                {
                    Thread.Sleep(1000);  
                    continue;
                }
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