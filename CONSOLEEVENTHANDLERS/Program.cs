using System;

namespace CONSOLEEVENTHANDLERS
{
    public class PriceChangedEventArgs : EventArgs
    {
        public readonly decimal LastPrice, NewPrice;
        public PriceChangedEventArgs(decimal lastPrice, decimal newPrice)
        {
            LastPrice = lastPrice;
            NewPrice = newPrice;
        }
    }

    public class Stock
    {
        string symbol;
        decimal price;

        public Stock(string symbol)
        {
            this.symbol = symbol;
        }

        public event EventHandler<PriceChangedEventArgs> PriceChanged;
        protected virtual void OnPriceChanged(PriceChangedEventArgs e)
        {
            if (PriceChanged != null)
                PriceChanged(this, e);
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                if (price == value) return;
                OnPriceChanged(new PriceChangedEventArgs(price, value));

                price = value;
            }
        }
    }

    class Program
    {
      
        static void Main(string[] args)
        {
            Stock stock = new Stock("THPW");
            stock.Price = 27.10M;

            stock.PriceChanged += stock_PriceChanged;
            stock.Price = 31.59M;
            Console.ReadLine();
        }

        private static void stock_PriceChanged(object sender, PriceChangedEventArgs e)
        {
            if((e.NewPrice - e.LastPrice) / e.LastPrice > 0.1M)
                Console.WriteLine("Alert, 10% price increase!");
        }
    }
}
