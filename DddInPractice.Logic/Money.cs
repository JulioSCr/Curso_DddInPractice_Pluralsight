namespace DddInPractice.Logic
{
    public sealed class Money : ValueObject<Money>
    {
        public int OneCentCount { get; set; }
        public int TenCentCount { get; set; }
        public int QuarterCount { get; set; }
        public int OneDollarCount { get; set; }
        public int FiveDollarCount { get; set; }
        public int TwentyDollarCount { get; set; }

        public Money(
            int oneCentCount,
            int tenCentCount,
            int quarterCount,
            int oneDollarCount,
            int fiveDollarCount,
            int twentyDollarCount
        )
        {
            OneCentCount = oneCentCount;
            TenCentCount = tenCentCount;
            QuarterCount = quarterCount;
            OneDollarCount = oneDollarCount;
            FiveDollarCount = fiveDollarCount;
            TwentyDollarCount = twentyDollarCount;
        }

        public static Money operator +(Money money1, Money money2)
        {
            Money sum = new Money(
                money1.OneCentCount + money2.OneCentCount,
                money1.TenCentCount + money2.TenCentCount,
                money1.QuarterCount + money2.QuarterCount,
                money1.OneDollarCount + money2.OneDollarCount,
                money1.FiveDollarCount + money2.FiveDollarCount,
                money1.TwentyDollarCount + money2.TwentyDollarCount
            );
            return sum;
        }

        protected override bool EqualsCore(Money other)
        {
            return OneCentCount == other.OneCentCount
                && TenCentCount == other.TenCentCount
                && QuarterCount == other.QuarterCount
                && OneDollarCount == other.OneDollarCount
                && FiveDollarCount == other.FiveDollarCount
                && TwentyDollarCount == other.TwentyDollarCount;
        }

        protected override int GetHashCodeCore()
        {
            unchecked
            {
                int hashCode = OneCentCount;
                hashCode = (hashCode * 397) ^ TenCentCount;
                hashCode = (hashCode * 397) ^ QuarterCount;
                hashCode = (hashCode * 397) ^ OneDollarCount;
                hashCode = (hashCode * 397) ^ FiveDollarCount;
                hashCode = (hashCode * 397) ^ TwentyDollarCount;
                return hashCode;
            }
        }

        // public static Money operator =(Money money) 
        // {
        //     Money equal = new Money(
        //         money.OneCentCount,
        //         money.TenCentCount,
        //         money.QuarterCount,
        //         money.OneDollarCount,
        //         money.FiveDollarCount,
        //         money.TwentyDollarCount
        //     );
        //     return equal;
        // }
    }
}