using CheckoutKata.Models;
using CheckoutKata.Services;

namespace CheckoutKata.Tests.Pricing
{
    public class CheckoutPriceTests
    {
        [Fact]
        public void GetTotalPrice_NoItemsScanned_ReturnsZero()
        {
            // Arrange
            var rules = new List<PricingRule>
            {
                new PricingRule("A", 50, 3, 130),
                new PricingRule("B", 30, 2, 45),
                new PricingRule("C", 20),
                new PricingRule("D", 15)
            };
            var checkout = new Checkout(rules);

            // Act
            int totalPrice = checkout.GetTotalPrice();

            // Assert
            Assert.Equal(0, totalPrice);
        }

        [Fact]
        public void GetTotalPrice_SingleItemWithoutSpecialPrice_ReturnsUnitPrice()
        {
            // Arrange
            var rules = new List<PricingRule>
            {
                new PricingRule("A", 50, 3, 130),
                new PricingRule("B", 30, 2, 45),
                new PricingRule("C", 20),
                new PricingRule("D", 15)
            };
            var checkout = new Checkout(rules);
            checkout.ScanSku("C");

            // Act
            int totalPrice = checkout.GetTotalPrice();

            // Assert
            Assert.Equal(20, totalPrice);
        }

        [Fact]
        public void GetTotalPrice_MultipleItemsWithSpecialPrice_ReturnsCombinedUnitPrice()
        {
            // Arrange
            var rules = new List<PricingRule>
            {
                new PricingRule("A", 50, 3, 130),
                new PricingRule("B", 30, 2, 45),
                new PricingRule("C", 20),
                new PricingRule("D", 15)
            };
            var checkout = new Checkout(rules);
            checkout.ScanSku("A");
            checkout.ScanSku("A");
            checkout.ScanSku("A");
            checkout.ScanSku("B");
            checkout.ScanSku("B");
            checkout.ScanSku("C");

            // Act
            int totalPrice = checkout.GetTotalPrice();

            // Assert
            // A: 3 for 130, B: 2 for 45, C: 1 for 20 => Total = 130 + 45 + 20 = 195
            Assert.Equal(195, totalPrice);

        }
    }
}
