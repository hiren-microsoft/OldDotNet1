namespace LegacyOrderProcessor
{
    public class DiscountEngine
    {
        private readonly decimal _highValueThreshold;
        private readonly decimal _highValueDiscountPercent;

        public DiscountEngine(decimal highValueThreshold, decimal highValueDiscountPercent)
        {
            _highValueThreshold = highValueThreshold;
            _highValueDiscountPercent = highValueDiscountPercent;
        }

        public decimal Apply(decimal amount)
        {
            if (amount >= _highValueThreshold)
            {
                return amount - (amount * _highValueDiscountPercent / 100m);
            }

            return amount;
        }
    }
}
