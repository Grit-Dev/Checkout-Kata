using CheckoutKata.Interfaces;
using CheckoutKata.Models;

namespace CheckoutKata.Services
{
    public class Checkout : ICheckout
    {
        private readonly List<PricingRule> _pricingRules;

        // Stock Keeping Units
        private readonly List<string> _scannedSkus = new();

        public Checkout(List<PricingRule> pricingRules)
        {
            _pricingRules = pricingRules ??
                throw new ArgumentNullException(nameof(pricingRules),
                "Pricing rules must be provided when creating a checkout instance.");
        }

        public int GetTotalPrice()
        {
            int totalPrice = 0;

            foreach (var pricingRuleCode in _pricingRules)
            {
                int quantityOfItemsScanned = 0;

                // Count the number of times the item appears in the scanned items
                foreach (var scannedItem in _scannedSkus)
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
        public void ScanSku(string sku)
        {
            // Stock Keeping Units
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException("Stock Keeping Units cannot be null or empty.", nameof(sku));
            }

            // Developer note:
            // The kata defines SKUs as capital letters (A, B, C, D).
            // There is slight ambiguity whether lowercase values such as "a" should be accepted.
            // Lowercase could represent a different identifier or unit of measurement in a real system.
            // For now, validation is case-sensitive.
            // If business rules required case-insensitive matching, we could normalise input like this:
            // sku = sku.ToUpperInvariant();

            bool skuExists = _pricingRules.Any(rule => rule.ItemCode == sku);

            if(!skuExists)
            {
                throw new ArgumentException($"The SKU '{sku}' does not exist in the pricing rules.", nameof(sku));
            }

            _scannedSkus.Add(sku);
        }
    }
}
