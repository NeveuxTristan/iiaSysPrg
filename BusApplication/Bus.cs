using System;
using System.Data;
using System.Threading;

namespace BusApplication
{
    public class Bus
    {
        private int nbPlacesTotal;
        private Semaphore porteEntree;
        private Semaphore porteSortie;

        private bool isDriving;
        private int clientAttendSortie;
        private int nbPlacesOccupee;
        private Ligne l;
        private bool continuer = true;
        private Random r;

        private Boolean busSansVoyageur;

        private bool nextStationIsTerminus = false;

        private Thread th;
        private Thread descentIntel;

        public Bus(int nbPlacesTotal)
        {
            this.nbPlacesTotal = nbPlacesTotal;
            porteEntree = new Semaphore(1, 1);
            // Initialisation à 2 pour symboliser les 2 portes.
            porteSortie = new Semaphore(2, 2);
            r = new Random();
            clientAttendSortie = 0;
            isDriving = false;
        }

        public void setLigne(Ligne l)
        {
            this.l = l;
            th = new Thread(comportement);
            th.Start();
        }

        public Semaphore entree()
        {
            return porteEntree;
        }

        public Semaphore sortie()
        {
            return porteSortie;
        }

        public bool isFull()
        {
            return nbPlacesOccupee == nbPlacesTotal;
        }

        public void comportement()
        {
            while (continuer)
            {
                // 0 = ALLER -  1 = RETOUR 
                for (int i = 0; i < 2; i++)
                {
                    if (i == 0)
                    {
                        Console.WriteLine("---- CHEMIN DU BUS ALLER -----");
                    }

                    foreach (var s in l.getStations())
                    {
                        if ((i == 0 && s.getId() == l.getStations().Count) || (i == 1 && s.getId() == 1))
                        {
                            Console.WriteLine("La prochaine station est le terminus.");
                            nextStationIsTerminus = true;
                            if (i == 1)
                            {
                                busSansVoyageur = true;
                            }
                        }

                        if (driveToNextStation(s))
                        {
                            if (!busSansVoyageur)
                            {
                                s.busArrive();
                                while (clientAttendSortie > 0 && (nbPlacesOccupee + s.getNbClient() < nbPlacesTotal))
                                {
                                    Thread.Sleep(2000);
                                }
                            }
                            else
                            {
                                Thread.Sleep(3000);
                            }
                        }
                    }

                    if (i == 0)
                    {
                        Console.WriteLine("---- CHEMIN DU BUS RETOUR -----");
                        l.revertLigne();
                        Thread.Sleep(2500);
                    }
                    else
                    {
                        continuer = false;
                    }
                }
            }
            Console.WriteLine("---- FIN DE BUS SIMULATOR 2018 -----");
        }

        /**
         * Renvoi true si le bus s'arrête.
         */
        private bool driveToNextStation(Station s)
        {
            if (!isDriving)
            {
                Console.WriteLine("Le bus démarre.");
            }
            isDriving = true;
            Thread.Sleep(r.Next(4000, 8000));
            if (clientAttendSortie == 0 && s.getNbClient() == 0)
            {
                Console.WriteLine("Le bus ne s'arrête pas à la station n°"+s.getId());
                return false;
            }

            isDriving = false;
            Console.WriteLine("Le bus s'arrête.");
            return true;
        }

        public bool enterCl(Client cl)
        {
            if (busSansVoyageur)
            {
                Console.WriteLine("TERMINUS FINAL, TOUT LE MONDE DESCENT, PERSONNE NE MONTE.");
                return false;
            }

            porteEntree.WaitOne();
            if (nbPlacesOccupee - clientAttendSortie < nbPlacesTotal)
            {
                Console.WriteLine("Client n°" + cl.getId() + " rentre dans le bus.");
                Thread.Sleep(2000);
                if (!cl.titreDeTransport())
                {
                    Console.WriteLine("Client n°" + cl.getId() + " achète un titre de transport.");
                    Thread.Sleep(r.Next(3000));
                }

                increment();
                porteEntree.Release();
                return true;
            }

            Console.WriteLine("Le bus est plein !");
            return false;
        }

        public void descentCl(Client cl)
        {
            Thread thread = new Thread(() => descentCpt(cl));
            thread.Start();
        }

        private void descentCpt(Client cl)
        {
            porteSortie.WaitOne();
            Thread.Sleep(r.Next(1000, 2500));
            Console.WriteLine("Client n°" + cl.getId() + " sort du bus.");
            clientAttendSortie--;
            decrement();
            porteSortie.Release();
        }

        public bool isBusDriving()
        {
            return isDriving;
        }

        private void increment()
        {
            Interlocked.Increment(ref nbPlacesOccupee);
        }

        private void decrement()
        {
            Interlocked.Decrement(ref nbPlacesOccupee);
        }

        public void clWantToOut(int id)
        {
            Console.WriteLine("Client n°" + id + " veut sortir du bus à la prochaine station.");
            clientAttendSortie++;
        }

        public bool isNextStationIsTerminus()
        {
            return nextStationIsTerminus;
        }

        public int nbClientAttendSortie()
        {
            return clientAttendSortie;
        }
    }
}