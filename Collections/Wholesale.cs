using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Wholesale : ICustomer
    {
        public int CustomerNo { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public int Zipcode { get; set; }
        public string City { get; set; }
        public int Phone { get; set; }
        public string Contact { get; set; }
        public string District { get; set; }
        public int NoOfDealers { get; set; }

        //Lister alle denne grossistens forhandlere.
        public void ListAllDealers(List<Dealer> list, int wholesale)
        {
            if (list.Count <= 0)
                Console.WriteLine("Grossisten har ingen registrerte forhandlere.");
            else
                foreach (var item in list)
                    if (item.Wholesale == wholesale)
                        Console.WriteLine("{0} {1}, Adresse: {2}, {3} {4}, TLF: {5}, Bedriftskontakt: {6}", item.CustomerNo, item.CompanyName, item.Address, item.Zipcode, item.City, item.Phone, item.Contact);
        }

        //Summerer alt salget som er gjort av alle denne grossistens forhandlere.
        public double TotalSales(List<Dealer> dealer, int wholesale, List<Sale> sale)
        {
            Dealer d = new Dealer();
            if (dealer == null) return 0;

            double sum = 0;
            foreach (var item in dealer)
            {
                if (item.Wholesale == wholesale)
                    sum += d.SumSales(sale, item.CustomerNo);
            }
            return sum;
        }
    }
}
