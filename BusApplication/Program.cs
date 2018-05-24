using System;

namespace BusApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue sur BUS SIMULATOR 2018");
            int nbPlaces = 0;
            int tailleLigne = 0;
            while (tailleLigne < 1)
            {
                Console.WriteLine("Veuillez saisir la taille de la ligne de BUS :");
                bool success = int.TryParse(Console.ReadLine(), out tailleLigne);
                if (!success)
                {
                    Console.WriteLine("Saisie invalide, veuillez réessayer.");
                    tailleLigne = 0;
                }
            }
            while (nbPlaces < 1)
            {
                Console.WriteLine("Veuillez saisir la taille du BUS :");
                bool success = int.TryParse(Console.ReadLine(), out nbPlaces);
                if (!success)
                {
                    Console.WriteLine("Saisie invalide, veuillez réessayer.");
                    nbPlaces = 0;
                }
            }

            Bus b = new Bus(nbPlaces);
            Ligne l = new Ligne(tailleLigne,b);

        }
    }
}