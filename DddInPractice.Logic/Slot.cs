namespace DddInPractice.Logic
{
    public class Slot : Entity
    {
        public SnackPile SnackPile { get; set; }
        public SnackMachine SnackMachine { get; protected set; }
        public int Position { get; protected set; }

        protected Slot() { }

        public Slot(SnackMachine snackMachine, int position) : this()
        {
            SnackPile = new SnackPile(null, 0, 0m);
            SnackMachine = snackMachine;
            Position = position;
        }
    }
}