using CheckoutKata.Models;

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

    }
}
