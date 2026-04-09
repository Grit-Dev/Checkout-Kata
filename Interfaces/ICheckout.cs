using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.Interfaces
{
    public interface ICheckout
    {
        // Item = SKU: Stock Keeping Units
        public void GetScannedItems(string item);

        public int GetTotalPrice();
    }
}
