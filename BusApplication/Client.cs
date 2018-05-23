using System;
using System.Threading;

namespace BusApplication
{
    public class Client
    {
        private static int ID_REFERENCE = 1;

        private int id;
        private bool possedeTitre;

        private Random r = new Random();

        public Client()
        {
            id = ID_REFERENCE++;
            possedeTitre = r.Next(0, 10) > 5;
            Thread.Sleep(10);
        }

        public bool titreDeTransport()
        {
            return possedeTitre;
        }
    }
}