using CheckoutKata.Models;
using CheckoutKata.Services;

namespace CheckoutKata.Tests
{
    public class CheckoutValidationTests
    {
        // Checkout cannot be created without pricing rules.
        // Since pricing rules are required for calculating totals,
        [Fact]
        public void Checkout_NullPricingRules_ThrowsArgumentNullException()
        {
            // Arrange
            List<PricingRule>? pricingRules = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Checkout(pricingRules!));
        }

        [Fact]
        public void ScanKu_WithNullValue_ShouldThrowException()
        {
            // Arrange
            var checkout = new Checkout(new List<PricingRule>());

            // Act & Assert
            Assert.Throws<ArgumentException>(() => checkout.ScanSku(null!));
        }

        [Fact]
        public void Checkout_NullPricingRules_ThrowsArgumentException()
        {
            // Arrange
            var checkout = new Checkout(new List<PricingRule>());

            // Act & Assert
            Assert.Throws<ArgumentException>(() => checkout.ScanSku(""));
        }

        //[Fact]
        //public void Checkout_NullPricingRules_ThrowsArgumentException()
        //{
        //    // Arrange
        //    // Act
        //    // Assert
        //}

        //[Fact]
        //public void Checkout_NullPricingRules_ThrowsArgumentException()
        //{
        //    // Arrange
        //    // Act
        //    // Assert
        //}

        //[Fact]
        //public void Checkout_NullPricingRules_ThrowsArgumentException()
        //{
        //    // Arrange
        //    // Act
        //    // Assert
        //}
    }
}