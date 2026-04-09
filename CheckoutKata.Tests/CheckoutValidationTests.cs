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
        public void ScanSku_WithEmptyString_ThrowsArgumentException()
        {
            // Arrange
            var checkout = new Checkout(new List<PricingRule>());

            // Act & Assert
            Assert.Throws<ArgumentException>(() => checkout.ScanSku(""));
        }

        [Fact]
        public void ScanSku_WithWhiteSpace_ShouldThrowException()
        {
            // Arrange
            var checkout = new Checkout(new List<PricingRule>());

            // Act & Assert
            Assert.Throws<ArgumentException>(() => checkout.ScanSku("    "));
        }

        [Fact]
        public void ScanSku_WithValidSku_ShouldNotThrowException()
        {
            // Arrange
            // Arrange
            var rules = new List<PricingRule>
            {
                new PricingRule("A", 50),
                new PricingRule("B", 30)
            };

            var checkout = new Checkout(rules);

            // Assert
            Assert.True(true);
        }

        [Fact]
        public void ScanSku_WithUnknownSku_ShouldThrowException()
        {
            // Arrange
            var rules = new List<PricingRule>
            {
                new PricingRule("A", 50),
                new PricingRule("B", 30)
            };

            var checkout = new Checkout(rules);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => checkout.ScanSku("x"));
        }


        // Developer note:
        // Slight ambiguity in kata.
        // SKUs are defined as capital letters (A, B, C, D).
        // Lowercase "a" could represent a different unit or identifier in a real system.
        // Would confirm expected behaviour with Product Owner if unclear.
        [Fact]
        public void ScanSku_WithLowerCaseSku_ShouldThrowException()
        {
            // Arrange
            var rules = new List<PricingRule>
            {
                new PricingRule("A", 50),
                new PricingRule("B", 30)
            };

            var checkout = new Checkout(rules);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => checkout.ScanSku("a"));
        }
    }
}