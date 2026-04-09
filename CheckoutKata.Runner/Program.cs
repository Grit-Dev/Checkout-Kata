using CheckoutKata.Interfaces;
using CheckoutKata.Models;
using System.Data;
using System.Text;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace CheckoutKata.Runner
{
    //TODO: Move into the service later:

    public class CheckoutTest
    {
        private readonly List<PricingRule> _pricingRules;

        private readonly List<string> _scannedItems = new();

        public CheckoutTest(List<PricingRule> pricingRules)
        {
            _pricingRules = pricingRules;
        }

        //TODO: Testing basic behaviour
        public void Scan(string itemCode)
        {
            _scannedItems.Add(itemCode);
        }

        //TODO: Remove after - Temp
        public List<string> GetScannedItems()
        {
            return _scannedItems;
        }

        // TODO: First pass - just total unit prices before handling special offers
        public int GetTotalPrice()
        {
            int totalPrice = 0;
            
            foreach(var pricingRuleCode in _pricingRules)
            {
                int quantityOfItemsScanned = 0;

                // Count the number of times the item appears in the scanned items
                foreach(var scannedItem in _scannedItems)
                {
                    if (scannedItem == pricingRuleCode.ItemCode)
                    {
                        quantityOfItemsScanned++;
                    }
                }

                // apply special offer price where configured, otherwise apply standard unit pricing
                if (pricingRuleCode.SpecialPriceQuantity.HasValue &&
                    pricingRuleCode.SpecialPriceAmount.HasValue &&
                    pricingRuleCode.SpecialPriceQuantity.Value > 0) // guard against divide-by-zero
                {
                    int offerQuantity = pricingRuleCode.SpecialPriceQuantity.Value;
                    int offerPrice = pricingRuleCode.SpecialPriceAmount.Value;

                    // Check how many times the special offer can be applied?
                    int numberOfOfferApplications = quantityOfItemsScanned / offerQuantity;

                    // Check how many items remain outside the offer
                    int remainingItemCount = quantityOfItemsScanned % offerQuantity;

                    // apply discounted total for offer groups
                    totalPrice = totalPrice + (numberOfOfferApplications * offerPrice);

                    // apply normal pricing for leftover items
                    totalPrice = totalPrice + (remainingItemCount * pricingRuleCode.UnitPrice);
                }
                else
                {
                    // no special price? => apply standard unit pricing
                    totalPrice = totalPrice + (quantityOfItemsScanned * pricingRuleCode.UnitPrice);
                }

            }

            return totalPrice;
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            // ItemCode = "A"
            // UnitPrice = 50
            // SpecialPriceQuantity = 3
            // SpecialPriceAmount = 130
            /*            var rules = new List<PricingRule>
                        {
                            new PricingRule("A", 50, 3, 130),
                            new PricingRule("B", 30, 2, 45),
                            new PricingRule("C", 20, null, null),
                            new PricingRule("D", 15, null, null)
                        };

                        Console.WriteLine("Runner project is working.");


                        // manually testing behaviour before implementing full checkout service
                        var checkout = new CheckoutTest(rules);

                        checkout.Scan("A");
                        checkout.Scan("B");
                        checkout.Scan("A");

                        Console.WriteLine("Scanned items so far:");

                        foreach (var item in checkout.GetScannedItems())
                        {
                            Console.WriteLine(item);
                        }

                        Console.WriteLine("Expected total (no specials yet): 130");

                        var total = checkout.GetTotalPrice();

                        Console.WriteLine($"Actual total: {total}");

                        //Test 2: 

                        var rules2 = new List<PricingRule>
                        {
                            new PricingRule("A", 50, 3, 130),
                            new PricingRule("B", 30, 2, 45),
                            new PricingRule("C", 20),
                            new PricingRule("D", 15)
                        };

                        var checkout2 = new CheckoutTest(rules2);

                        checkout2.Scan("C");
                        checkout2.Scan("D");
                        checkout2.Scan("C");

                        Console.WriteLine();
                        Console.WriteLine("Test 2 - constructor without special pricing");

                        Console.WriteLine("Items scanned: C, D, C");

                        // expected calculation:
                        // C = 20
                        // D = 15
                        // C = 20
                        // total = 55

                        Console.WriteLine("Expected total: 55");

                        var total2 = checkout2.GetTotalPrice();

                        Console.WriteLine($"Actual total: {total2}");*/

            // Special pricing:

            // Test 3 - special pricing for

            // pricing configuration used for special pricing tests
            var rules = new List<PricingRule>
            {
                new PricingRule("A", 50, 3, 130),
                new PricingRule("B", 30, 2, 45),
                new PricingRule("C", 20),
                new PricingRule("D", 15)
            };
            var checkout3 = new CheckoutTest(rules);

            checkout3.Scan("A");
            checkout3.Scan("A");
            checkout3.Scan("A");

            Console.WriteLine();
            Console.WriteLine("pricing for A");
            Console.WriteLine("Items scanned: A, A, A");
            Console.WriteLine("Expected total: 130");

            var total3 = checkout3.GetTotalPrice();

            Console.WriteLine($"Actual total: {total3}");


            // Test 4 - special pricing should work regardless of scan order
            var checkout4 = new CheckoutTest(rules);

            checkout4.Scan("B");
            checkout4.Scan("A");
            checkout4.Scan("B");

            Console.WriteLine();
            Console.WriteLine("pricing works regardless of scan order");
            Console.WriteLine("Items scanned: B, A, B");
            Console.WriteLine("Expected total: 95");

            var total4 = checkout4.GetTotalPrice();

            Console.WriteLine($"Actual total: {total4}");


            // Test 5 - special pricing with remaining items
            var checkout5 = new CheckoutTest(rules);

            checkout5.Scan("A");
            checkout5.Scan("A");
            checkout5.Scan("A");
            checkout5.Scan("A");

            Console.WriteLine();
            Console.WriteLine("pricing plus one remaining item");
            Console.WriteLine("Items scanned: A, A, A, A");
            Console.WriteLine("Expected total: 180");

            var total5 = checkout5.GetTotalPrice();

            Console.WriteLine($"Actual total: {total5}");


            // Test 6 - special pricing applied multiple times
            var checkout6 = new CheckoutTest(rules);

            checkout6.Scan("A");
            checkout6.Scan("A");
            checkout6.Scan("A");
            checkout6.Scan("A");
            checkout6.Scan("A");
            checkout6.Scan("A");

            Console.WriteLine();
            Console.WriteLine("pricing applied multiple times");
            Console.WriteLine("Items scanned: A, A, A, A, A, A");
            Console.WriteLine("Expected total: 260");

            var total6 = checkout6.GetTotalPrice();

            Console.WriteLine($"Actual total: {total6}");

        }
    }
}
