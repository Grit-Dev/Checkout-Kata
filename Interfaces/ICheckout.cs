using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckoutKata.Interfaces
{
    public interface ICheckout
    {
        public void Scan(string item);

        public int GetTotalPrixe();
    }
}
