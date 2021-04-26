using DddInPractice.Logic;
using static DddInPractice.Logic.Money;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DddInPractice.Tests
{
    [TestClass]
    public sealed class SnackMachineSpecs
    {
        [TestMethod]
        public void Return_money_empties_money_in_transaction()
        {
            var snackMachine = new SnackMachine();
            snackMachine.InsertMoney(Dollar);

            snackMachine.ReturnMoney();

            snackMachine.MoneyInTransaction.Amount.Should().Be(0m);
        }

        [TestMethod]
        public void Inserted_money_goes_to_money_in_transaction()
        {
            var snackMachine = new SnackMachine();

            snackMachine.InsertMoney(Cent);
            snackMachine.InsertMoney(Dollar);

            snackMachine.MoneyInTransaction.Amount.Should().Be(1.01m);
        }

        [TestMethod]
        public void Cannot_insert_more_than_one_coin_or_note_at_a_time()
        {
            var snackMachine = new SnackMachine();
            var twoCent = Cent + Cent;

            Action action = () => snackMachine.InsertMoney(twoCent);

            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void BuySnack_trades_insert_money_for_a_snack()
        {
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(new Snack("Some Snack"), 10, 1m));
            snackMachine.InsertMoney(Dollar);

            snackMachine.BuySnack(1);

            snackMachine.MoneyInTransaction.Should().Be(None);
            snackMachine.MoneyInside.Amount.Should().Be(1m);
            snackMachine.GetSnackPile(1).Quantity.Should().Be(9);
        }

        [TestMethod]
        public void Cannot_make_purchase_when_there_is_no_snacks()
        {
            var snackMachine = new SnackMachine();

            Action action = () => snackMachine.BuySnack(1);

            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void Cannot_make_purchase_if_not_enough_money_inserted()
        {
            var snackMachine = new SnackMachine();
            snackMachine.LoadSnacks(1, new SnackPile(new Snack("Some snack"), 1, 2m));
            snackMachine.InsertMoney(Dollar);

            Action action = () => snackMachine.BuySnack(1);

            action.Should().Throw<InvalidOperationException>();
        }
    }
}