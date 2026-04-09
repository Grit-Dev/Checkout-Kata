namespace CheckoutKata.Models
{
    public class PricingRule
    {
        // Note: SKU = Stock keeping units
        public string ItemCode { get; set; }

        public int UnitPrice { get; set; }

        //Special offer price (e.g. 3 for 130)
        public int? SpecialPriceQuantity { get; set; }

        // Total price for special offer quantity
        public int? SpecialPriceAmount { get; set; }


        public PricingRule(string itemCode, int unitPrice, int? specialPriceQuantity = null, int? specialPriceAmount = null)
        {

            if (string.IsNullOrWhiteSpace(itemCode))
            {
                throw new ArgumentException("Item code cannot be null or empty.", nameof(itemCode));
            }

            if(unitPrice <= 0)
            {
                throw new ArgumentException("Unit price cannot be negative.", nameof(unitPrice));
            }

            if(specialPriceQuantity.HasValue != specialPriceAmount.HasValue)
            {
                throw new ArgumentException("Both special price quantity and amount must be provided together.");
            }

            if (specialPriceQuantity.HasValue && specialPriceQuantity.Value <= 0)
            {
                throw new ArgumentException("Special price quantity must be greater than zero.", nameof(specialPriceQuantity));
            }

            if(specialPriceAmount.HasValue && specialPriceAmount.Value <= 0)
            {
                throw new ArgumentException("Special price amount cannot be negative.", nameof(specialPriceAmount));
            }


            this.ItemCode = itemCode;
            this.UnitPrice = unitPrice;
            this.SpecialPriceQuantity = specialPriceQuantity;
            this.SpecialPriceAmount = specialPriceAmount;
        }
    }
}