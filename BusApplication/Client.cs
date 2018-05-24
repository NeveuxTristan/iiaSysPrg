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
                
            }
        }
    }
}