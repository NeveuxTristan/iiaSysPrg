using System.Reflection;

namespace BusApplication
{
    public class Bus
    {
        private int nbPlacesTotal;
        private Porte porteAvant;
        private Porte porteCentrale;
        private Porte porteArriere;

        private bool isDriving;
        private bool clientAttendSortie;
        private int nbPlacesOccupee;

        public Bus(int nbPlacesTotal)
        {
            this.nbPlacesTotal = nbPlacesTotal;
            porteArriere = new Porte();
            porteAvant = new Porte();
            porteCentrale = new Porte();

            clientAttendSortie = false;
            isDriving = false;
        }
        
        
    }
}