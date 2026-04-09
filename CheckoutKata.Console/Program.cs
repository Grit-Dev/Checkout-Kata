using CheckoutKata.Models;

namespace CheckoutKata.ConsoleApp
{
    public class Program
    {
        private readonly List<PricingRule> _pricingRules;

        private readonly List<string> _scannedItems = new();

        public class CheckoutTest
        {
            private readonly List<PricingRule> _pricingRules;

            private readonly List<string> _scannedItems = new();

            public CheckoutTest(List<PricingRule> pricingRules)
            {
                // Save the pricing rules for use in calculating totals
                _pricingRules = pricingRules;
            }

            public void Scan(string item)
            {
                // Add each scanned item to the list
                _scannedItems.Add(item);
            }
        }

        public void Scan(string item)
        {
            // Add each scanned item to the list
            _scannedItems.Add(item);
        }

        public static void Main(string[] args)
        {
            // temporary testing area
            var rules = new List<PricingRule>
            {
                new PricingRule("A", 50, 3, 130),
                new PricingRule("B", 30, 2, 45),
                new PricingRule("C", 20, null, null),
                new PricingRule("D", 15, null, null)
            };

            var checkout = new CheckoutTest(rules);

        }
    }
}