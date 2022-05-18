using DddInPractice.Logic.Atms;
using static DddInPractice.Logic.SharedKernel.Money;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

namespace DddInPractice.Tests
{
    [TestClass]
    public class AtmSpecs
    {
        [TestMethod]
        public void Take_money_exchanges_money_with_commission()
        {
            Atm atm = new Atm();
            atm.LoadMoney(Dollar);

            atm.TakeMoney(1m);

            atm.MoneyInside.Amount.Should().Be(0m);
            atm.MoneyCharged.Should().Be(1.01m);
        }

        [TestMethod]
        public void Comission_is_at_least_one_cent()
        {
            Atm atm = new Atm();
            atm.LoadMoney(Cent);

            atm.TakeMoney(0.01m);

            atm.MoneyCharged.Should().Be(0.02m);
        }

        [TestMethod]
        public void Commission_is_rounded_up_to_the_next_cent()
        {
            Atm atm = new Atm();
            atm.LoadMoney(Dollar + TenCent);

            atm.TakeMoney(1.1m);

            atm.MoneyCharged.Should().Be(1.12m);
        }
    }
}