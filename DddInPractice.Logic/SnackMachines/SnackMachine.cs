using System;
using System.Linq;
using System.Collections.Generic;
using DddInPractice.Logic.Common;
using DddInPractice.Logic.SharedKernel;
using static DddInPractice.Logic.SharedKernel.Money;

namespace DddInPractice.Logic.SnackMachines
{
    public class SnackMachine : AggregateRoot
    {
        public Money MoneyInside { get; private set; }
        public decimal MoneyInTransaction { get; private set; }
        protected IList<Slot> Slots { get; private set; }

        public SnackMachine()
        {
            MoneyInside = None;
            MoneyInTransaction = 0m;
            Slots = new List<Slot>
            {
                new Slot(this, 1),
                new Slot(this, 2),
                new Slot(this, 3)
            };
        }

        private Slot GetSlot(int position)
        {
            return Slots.Single(x => x.Position == position);
        }

        public SnackPile GetSnackPile(int position)
        {
            return GetSlot(position).SnackPile;
        }

        public void InsertMoney(Money money)
        {
            Money[] coinsAndNotes = { Cent, TenCent, Quarter, Dollar, FiveDollar, TwentyDollar };
            if (!coinsAndNotes.Contains(money))
                throw new InvalidOperationException();

            MoneyInTransaction += money.Amount;
            MoneyInside += money;
        }

        public void ReturnMoney()
        {
            Money moneyToReturn = MoneyInside.Allocate(MoneyInTransaction);
            MoneyInside -= moneyToReturn;
            MoneyInTransaction = 0m;
        }

        public string CanBuySnack(int position)
        {
            SnackPile snackPile = GetSnackPile(position);

            if (snackPile.Quantity == 0)
                return "The snack pile is empty";

            if (MoneyInTransaction < snackPile.Price)
                return "Not enough money";

            if (!MoneyInside.CanAllocate(MoneyInTransaction - snackPile.Price))
                return "Not enough to change";

            return string.Empty;
        }

        public void BuySnack(int position)
        {
            if (CanBuySnack(position) != string.Empty)
                throw new InvalidOperationException();

            Slot slot = GetSlot(position);
            slot.SnackPile = slot.SnackPile.SubtractOne();

            Money change = MoneyInside.Allocate(MoneyInTransaction - slot.SnackPile.Price);
            MoneyInside -= change;
            MoneyInTransaction = 0m;
        }

        public void LoadSnacks(int position, SnackPile snackPile)
        {
            Slot slot = Slots.Single(x => x.Position == position);
            slot.SnackPile = snackPile;
        }

        public void LoadMoney(Money money)
        {
            MoneyInside += money;
        }
    }
}
