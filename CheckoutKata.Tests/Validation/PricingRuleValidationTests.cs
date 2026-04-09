using CheckoutKata.Models;
using System.Xml.Serialization;

namespace CheckoutKata.Tests.Validation
{
    public class PricingRuleValidationTests
    {
        [Fact]
        public void pricingRule_WithNullItemCode_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PricingRule(null!, 50));
        }

        [Fact]
        public void PricingRule_WithEmptyItemCode_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PricingRule("", 50));
        }

        [Fact]
        public void pricingRule_WithWhiteSpaceItemCode_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PricingRule("   ", 50));
        }

        [Fact]
        public void PricingRule_WithZeroUnitPrice_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PricingRule("A", 0));
        }

        [Fact]
        public void PricingRule_withNegativeUnitPrice_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PricingRule("A", -10));
        }

        [Fact]
        public void PricingRule_WithZeroSpecialPriceQuantity_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PricingRule("A", 50, 0, 130));
        }

        [Fact]
        public void PricingRule_WithNegativeSpecialPriceQuantity_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PricingRule("A", 50, -3, 130));
        }

        [Fact]
        public void PricingRule_WithZeroSpecialPriceAmount_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PricingRule("A", 50, 3, 0));
        }

    }
}
