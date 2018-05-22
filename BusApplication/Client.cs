using System;

namespace BusApplication
{
    public class Client
    {
        private static int ID_REFERENCE = 1; 
        
        private int id;
        private bool possedeTitre;

        public Client()
        {
            id = ID_REFERENCE++;
            Random r = new Random();
            possedeTitre = r.Next(5000) < r.Next(5000);
        }

        public bool titreDeTransport()
        {
            return possedeTitre;
        }
        
    }
}