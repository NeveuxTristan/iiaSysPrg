namespace BusApplication
{
    public class Porte
    {

        public Porte()
        {
            isOpen = false;
        }

        private bool isOpen;

        public void ouverture()
        {
            isOpen = true;
        }

        public void fermeture()
        {
            isOpen = false;
        }

        public bool isDoorOpen()
        {
            return isOpen;
        }
        
    }
}