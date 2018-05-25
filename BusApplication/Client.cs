using System;
using System.Threading;

namespace BusApplication
{
    public class Client
    {
        private static int ID_REFERENCE = 1;

        private int id;
        private Bus b;
        private bool possedeTitre;
        private bool continuer = true;

        private bool wantDescent = false;

        private Thread thClient;

        private Random r = new Random();

        public Client(Bus b)
        {
            this.b = b;
            id = ID_REFERENCE++;
            thClient = new Thread(comportement);
            possedeTitre = r.Next(0, 10) > 5;
            Thread.Sleep(10);
            thClient.Start();
        }

        public bool titreDeTransport()
        {
            return possedeTitre;
        }

        private void comportement()
        {
            while (continuer)
            {
                if (b.isBusDriving())
                {
                    Thread.Sleep(100);
                    if (!wantDescent)
                    {
                        wantDescent = r.Next(10000) > 8000;
                        if (b.isNextStationIsTerminus() || wantDescent)
                        {
                            b.clWantToOut(getId());
                            wantDescent = true;
                        }

                        Thread.Sleep(2000);
                    }
                }
                else
                {
                    if (wantDescent)
                    {
                        b.descentCl(this);
                        Thread.Sleep(r.Next(1000));
                        continuer = false;
                    }
                }
            }
        }

        public int getId()
        {
            return id;
        }

        public void terminate()
        {
            continuer = false;
            thClient.Abort();
            Console.WriteLine("Le client n°"+id+", n'as pas pu monter dans le bus plein...");
        }
    }
}