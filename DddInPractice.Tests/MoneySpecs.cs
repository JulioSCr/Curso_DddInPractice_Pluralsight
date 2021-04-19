using System;
using DddInPractice.Logic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace dddinpractice.tests
{
    [TestClass]
    public class MoneySpecs
    {
        [TestMethod]
        public void Sum_of_two_moneys_produces_correct_result()
        {
            // Arrange
            Money money1 = new Money(1, 2, 3, 4, 5, 6);
            Money money2 = new Money(1, 2, 3, 4, 5, 6);

            // Act
            Money sum = money1 + money2;

            // Assert
            sum.OneCentCount.Should().Be(2);
            sum.TenCentCount.Should().Be(4);
            sum.QuarterCount.Should().Be(6);
            sum.OneDollarCount.Should().Be(8);
            sum.FiveDollarCount.Should().Be(10);
            sum.TwentyDollarCount.Should().Be(12);
        }

        [TestMethod]
        public void Two_money_instances_do_not_equal_if_contain_different_money_amounts()
        {
            Money dollar = new Money(0, 0, 0, 1, 0, 0);
            Money hundredCents = new Money(100, 0, 0, 0, 0, 0);

            dollar.Should().NotBe(hundredCents);
            dollar.GetHashCode().Should().NotBe(hundredCents.GetHashCode());
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow(-1, 0, 0, 0, 0, 0)]
        [DataRow(0, -2, 0, 0, 0, 0)]
        [DataRow(0, 0, -3, 0, 0, 0)]
        [DataRow(0, 0, 0, -4, 0, 0)]
        [DataRow(0, 0, 0, 0, -5, 0)]
        [DataRow(0, 0, 0, 0, 0, -6)]
        public void Cannot_create_money_with_negative_value(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount
        )
        {
            Action action = () => new Money(
                oneCentCount,
                tenCentCount,
                quarterCount,
                oneDollarCount,
                fiveDollarCount,
                twentyDollarCount
            );

            action.Should().Throw<InvalidOperationException>();
        }
    }
}
