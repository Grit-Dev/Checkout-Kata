using CheckoutKata.Models;
using CheckoutKata.Services;

namespace CheckoutKata.Tests.Pricing
{
    public class CheckoutPricingTests
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
        public void GetTotalPrice_ThreeAs_AppliesSpecialPrice()
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

            // Act
            int totalPrice = checkout.GetTotalPrice();

            // Assert
            // A: 3 for 130 => Total = 130
            Assert.Equal(130, totalPrice);
        }

        [Fact]
        public void GetTotalPrice_TwoBs_AppliesSpecialPrice()
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
            checkout.ScanSku("B");
            checkout.ScanSku("B");

            // Act
            int totalPrice = checkout.GetTotalPrice();

            // Assert
            // B: 2 for 45 => Total = 45
            Assert.Equal(45, totalPrice);
        }

        [Fact]
        public void GetTotalPrice_ItemsScannedInAnyOrder_AppliesCorrectPricing()
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
            checkout.ScanSku("B");
            checkout.ScanSku("A");
            checkout.ScanSku("C");
            checkout.ScanSku("A");
            checkout.ScanSku("B");
            checkout.ScanSku("A");

            // Act
            int totalPrice = checkout.GetTotalPrice();

            // Assert
            // A: 3 for 130, B: 2 for 45, C: 1 for 20 => Total = 130 + 45 + 20 = 195
            Assert.Equal(195, totalPrice);
        }

        [Fact]
        public void GetTotalPrice_FourAs_AppliesSpecialPriceForThreePlusUnitPriceForOne()
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
            checkout.ScanSku("A");

            // Act
            int totalPrice = checkout.GetTotalPrice();

            // Assert
            // A: 3 for 130 + 1 for 50 => Total = 130 + 50 = 180
            Assert.Equal(180, totalPrice);
        }

        [Fact]
        public void GetTotalPrice_SixAs_AppliesSpecialPriceTwice()
        {
            // Arrange
            var checkout = new Checkout(new List<PricingRule>
            {
                new PricingRule("A", 50, 3, 130),
                new PricingRule("B", 30, 2, 45),
                new PricingRule("C", 20),
                new PricingRule("D", 15)
            });

            // Act
            checkout.ScanSku("A");
            checkout.ScanSku("A");
            checkout.ScanSku("A");
            checkout.ScanSku("A");
            checkout.ScanSku("A");
            checkout.ScanSku("A");

            var totalPrice = checkout.GetTotalPrice();

            //Assert
            // A: 3 for 130 + 3 for 130 => Total = 130 + 130 = 260
            Assert.Equal(260, totalPrice);

        }

        [Fact]
        public void GetTotalPrice_MultipleItemsWithSpecialPrices_ReturnsCorrectTotal()
        {
            // Arrange
            var checkout = new Checkout(new List<PricingRule>
            {
                new PricingRule("A", 50, 3, 130),
                new PricingRule("B", 30, 2, 45),
                new PricingRule("C", 20),
                new PricingRule("D", 15)
            });

            // Act
            checkout.ScanSku("A");
            checkout.ScanSku("A");
            checkout.ScanSku("A");
            checkout.ScanSku("B");
            checkout.ScanSku("B");
            checkout.ScanSku("C");
            checkout.ScanSku("D");

            var totalPrice = checkout.GetTotalPrice();

            //Assert
            // A: 3 for 130, B: 2 for 45, C: 1 for 20, D: 1 for 15 => Total = 130 + 45 + 20 + 15 = 210
            Assert.Equal(210, totalPrice);
        }

        [Fact]
        public void GetTotalPrice_SingleA_UsesUnitPriceWhenSpecialPriceQuantityNotMet()
        {
            // Arrange
            var checkout = new Checkout(new List<PricingRule>
            {
                new PricingRule("A", 50, 3, 130),
                new PricingRule("B", 30, 2, 45),
                new PricingRule("C", 20),
                new PricingRule("D", 15)
            });

            // Act
            checkout.ScanSku("A");

            var totalPrice = checkout.GetTotalPrice();

            //Assert
            // A: 1 for 50 => Total = 50
            Assert.Equal(50, totalPrice);
        }

        [Fact]
        public void GetTotalPrice_TwoAs_UsesUnitPriceWhenSpecialPriceQuantityNotMet()
        {
            // Arrange
            var checkout = new Checkout(new List<PricingRule>
            {
                new PricingRule("A", 50, 3, 130),
                new PricingRule("B", 30, 2, 45),
                new PricingRule("C", 20),
                new PricingRule("D", 15)
            });

            // Act
            checkout.ScanSku("A");
            checkout.ScanSku("A");
            var totalPrice = checkout.GetTotalPrice();

            //Assert
            // A: 2 for 100 => Total = 100
            Assert.Equal(100, totalPrice);
        }

        [Fact]
        public void GetTotalPrice_SingleB_UsesUnitPriceWhenSpecialPriceQuantityNotMet()
        {
            // Arrange
            var checkout = new Checkout(new List<PricingRule>
            {
                new PricingRule("A", 50, 3, 130),
                new PricingRule("B", 30, 2, 45),
                new PricingRule("C", 20),
                new PricingRule("D", 15)
            });
            // Act
            checkout.ScanSku("B");
            var totalPrice = checkout.GetTotalPrice();

            //Assert
            // B: 1 for 30 => Total = 30
            Assert.Equal(30, totalPrice);
        }

        [Fact]
        public void GetTotalPrice_ThreeBs_AppliesSpecialPriceForTwoPlusUnitPriceForOne()
        {
            // Arrange
            var checkout = new Checkout(new List<PricingRule>
            {
                new PricingRule("A", 50, 3, 130),
                new PricingRule("B", 30, 2, 45),
                new PricingRule("C", 20),
                new PricingRule("D", 15)
            });
            // Act
            checkout.ScanSku("B");
            checkout.ScanSku("B");
            checkout.ScanSku("B");
            var totalPrice = checkout.GetTotalPrice();

            //Assert
            // B: 2 for 45 + 1 for 30 => Total = 45 + 30 = 75
            Assert.Equal(75, totalPrice);
        }
    }
}