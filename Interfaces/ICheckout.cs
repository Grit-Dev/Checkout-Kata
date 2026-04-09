namespace CheckoutKata.Interfaces
{
    public interface ICheckout
    {
        // Stock Keeping Units
        public void ScanSku(string sku);

        public int GetTotalPrice();
    }
}
