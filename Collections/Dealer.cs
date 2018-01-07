using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Dealer : ICustomer //Forhandler
    {
        public int CustomerNo { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public int Zipcode { get; set; }
        public string City { get; set; }
        public int Phone { get; set; }
        public string Contact { get; set; }
        public string District { get; set; }
        public int Wholesale { get; set; } //Referanse til grossist

        Sale s = new Sale();

        //Oppretter et salg.
        public void AddSale(List<Sale> list, int d, String c, double p) 
        {
            if (c == null || c == "") c = "Ukjent";
            s.Dealer = d;
            s.CustomerName = c;
            s.Price = p;
            list.Add(s);
        }

        //Lager liste over alle salgene denne forhandleren har hatt.
        public List<Sale> DealerSales(List<Sale> sale, int dealer)
        {
            var dealersales = new List<Sale>();
            foreach (var item in sale)
            {
                if (item.Dealer == dealer)
                    dealersales.Add(item);
            }
            return dealersales;
        }

        //Lister ut salg som denne forhandleren har hatt.
        public void ListSales(List<Sale> sale, int dealer)
        {
            var dealerSales = DealerSales(sale, dealer);
            if (dealerSales.Count == 0)
                Console.WriteLine("Forhandleren har ingen registrerte salg.");
            else
                foreach (var item in dealerSales)
                    Console.WriteLine("{0}: {1} NOK", item.CustomerName, item.Price);
        }

        //Summerer alle salgene som denne forhandleren har hatt.
        public double SumSales(List<Sale> sale, int dealer)
        {
            double sum = 0;
            foreach (var item in DealerSales(sale, dealer))
            {
                if (item.Dealer == dealer)
                    sum += item.Price;
            }
            return sum;
        }
    }
}
