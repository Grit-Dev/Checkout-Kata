using CheckoutKata.Models;

namespace CheckoutKata.Tests.Validation
{
    public class PricingRuleValidationTests
    {
        [Fact]
        public void PricingRule_WithNullItemCode_ShouldThrowException()
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
        public void PricingRule_WithWhiteSpaceItemCode_ShouldThrowException()
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
            Assert.Throws<ArgumentException>(() => new PricingRule("B", -10));
        }

        [Fact]
        public void PricingRule_WithZeroSpecialPriceQuantity_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PricingRule("C", 50, 0, 130));
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

        [Fact]
        public void PricingRuleWithNegativeSpecialPriceAmount_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PricingRule("A", 50, 3, -10));
        }

        [Fact]
        public void PricingRule_WithSpecialPriceQuantityButNoAmount_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PricingRule("A", 50, 3, null));
        }

        [Fact]
        public void PricingRule_WithSpecialPriceAcmountButNoQuantity_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PricingRule("A", 50, null, 130));
        }

        [Fact]
        public void PricingRule_WithValidStandPrice_ShouldCreateRuleSuccessfully()
        {
            // Act
            var rule = new PricingRule("C", 20);

            // Assert
            Assert.NotNull(rule);
            Assert.Equal("C", rule.ItemCode);
            Assert.Equal(20, rule.UnitPrice);
            Assert.Null(rule.SpecialPriceQuantity);
            Assert.Null(rule.SpecialPriceAmount);
        }

        [Fact]
        public void PricingRule_WithValidSpecialPrice_ShouldCreateRuleSuccessfully()
        {
            // Act
            var rule = new PricingRule("A", 50, 3, 130);
            // Assert
            Assert.NotNull(rule);
            Assert.Equal("A", rule.ItemCode);
            Assert.Equal(50, rule.UnitPrice);
            Assert.Equal(3, rule.SpecialPriceQuantity);
            Assert.Equal(130, rule.SpecialPriceAmount);

        }
    }
}
