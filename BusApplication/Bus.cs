using System;
using System.Threading;

namespace BusApplication
{
    public class Bus
    {
        private int nbPlacesTotal;
        private Semaphore porteEntree;
        private Semaphore porteSortie;

        private bool isDriving;
        private bool clientAttendSortie;
        private int nbPlacesOccupee;
        private Ligne l;
        private bool continuer = true;
        private Random r;

        private Thread th;

        public Bus(int nbPlacesTotal)
        {
            this.nbPlacesTotal = nbPlacesTotal;
            porteEntree = new Semaphore(1, 1);
            porteSortie = new Semaphore(2, 2);
            r = new Random();

            clientAttendSortie = false;
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
                // ALLER - RETOUR 
                for (int i = 0; i < 2; i++)
                {
                    foreach (var s in l.getStations())
                    {
                        driveToNextStation();
                        if (s.getNbClient() > 0)
                        {
                            
                        }
                        else
                        {
                            if (clientAttendSortie)
                            {
                            
                            }
                        }


                    }
                    l.revertLigne();
                }
            }
        }

        private void driveToNextStation()
        {
            isDriving = true;
            Thread.Sleep(r.Next(5000));
            isDriving = false;
        }

        public bool enterCl(Client cl)
        {
            porteEntree.WaitOne();
            Thread.Sleep(2500);
            if (!cl.titreDeTransport())
            {
                
            }

            porteEntree.Release();
        }
        
        
        
        
    }
}