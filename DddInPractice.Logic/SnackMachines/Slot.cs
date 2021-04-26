using DddInPractice.Logic.Common;
using static DddInPractice.Logic.SnackMachines.SnackPile;

namespace DddInPractice.Logic.SnackMachines
{
    public class Slot : Entity
    {
        public SnackPile SnackPile { get; set; }
        public SnackMachine SnackMachine { get; protected set; }
        public int Position { get; protected set; }

        protected Slot() { }

        public Slot(SnackMachine snackMachine, int position) : this()
        {
            SnackMachine = snackMachine;
            Position = position;
            SnackPile = Empty;
        }
    }
}