using System;
using System.Collections.Generic;
using System.Linq;
using static DddInPractice.Logic.Money;

namespace DddInPractice.Logic
{
    public class SnackMachine : AggregateRoot
    {
        public Money MoneyInside { get; private set; }
        public Money MoneyInTransaction { get; private set; }
        protected IList<Slot> Slots { get; private set; }

        public SnackMachine()
        {
            MoneyInside = None;
            MoneyInTransaction = None;
            Slots = new List<Slot>
            {
                new Slot(this, 1),
                new Slot(this, 2),
                new Slot(this, 3)
            };
        }

        public SnackPile GetSnackPile(int position)
        {
            return Slots.Single(x => x.Position == position).SnackPile;
        }

        public void InsertMoney(Money money)
        {
            Money[] coinsAndNotes = { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
            if(!coinsAndNotes.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money;
        }

        public void ReturnMoney()
        {
            MoneyInTransaction = None;
        }

        public void BuySnack(int position)
        {
            Slot slot = Slots.Single(x => x.Position == position);
            slot.SnackPile = slot.SnackPile.SubtractOne();
            MoneyInside += MoneyInTransaction;
            MoneyInTransaction = None;
        }

        public void LoadSnacks(int position, SnackPile snackPile)
        {
            Slot slot = Slots.Single(x => x.Position == position);
            slot.SnackPile = snackPile;
        }
    }
}
