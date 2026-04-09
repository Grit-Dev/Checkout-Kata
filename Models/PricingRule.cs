using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.Models
{
    public class PricingRule
    {
        // Note: SKU = Stock keeping units
        public string ItemCode { get; set; }

        public int UnitPrice { get; set; }

        //Special offer price (e.g. 3 for 130)
        public int? SpecialPriceQuantity { get; set; }

        // Total price for speical offer quantity
        public int? SpecialPriceAmount { get; set; }

        
        public PricingRule(string ItemCode, int unitPrice, int? specialPriceQuantity, int? specialPriceAmount) 
        {
            this.ItemCode = ItemCode;
            this.UnitPrice = unitPrice;
            this.SpecialPriceQuantity = specialPriceQuantity;
            this.SpecialPriceAmount = specialPriceAmount;
        }


    }
}
