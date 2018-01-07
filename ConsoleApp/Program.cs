using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;

namespace ConsoleApp
{
    class Program
    {
        static int wholesaler = 0;
        static int dealer = 0;
         
        static void Main(string[] args)
        {
            var wholesalers = new List<Wholesale> //Grossister
            {
                new Wholesale {CustomerNo=0, CompanyName="Platekompaniet Øst", Address="Karl Johans Gate 10", Zipcode=1234, City="Oslo", Phone=12345678, Contact="Petter Hansen", District="Østlandet", NoOfDealers=1},
                new Wholesale {CustomerNo=1, CompanyName="Platekompaniet Trondheim", Address="Trondheimsgate 1", Zipcode=4321, City="Trondheim", Phone=12345678, Contact="Ida Olsen", District="Trøndelag", NoOfDealers=3},
                new Wholesale {CustomerNo=2, CompanyName="Retro-vinyl Vest", Address="Fanavegen 175", Zipcode=2222, City="Bergen", Phone=12345678, Contact="John Nordmann", District="Vestlandet", NoOfDealers=2}
            };

            var dealers = new List<Dealer> //Forhandlere
            {
                new Dealer {CustomerNo=0, CompanyName="Platekompaniet Byporten Shopping", Address="Oslogate 54", Zipcode=1234, City="Oslo", Phone=12345678, Contact="Ola Nordmann", Wholesale=0 },
                new Dealer {CustomerNo=1, CompanyName="Platekompaniet Sentrum", Address="Trondheimsgate 710", Zipcode=4321, City="Trondheim", Phone=12345678, Contact="Kari Olsen", Wholesale=1 },
                new Dealer {CustomerNo=2, CompanyName="Platekompaniet Torget", Address="Abels gate 320", Zipcode=4321, City="Trondheim", Phone=12345678, Contact="Petra Iversen", Wholesale=1 },
                new Dealer {CustomerNo=3, CompanyName="Platekompaniet Solsiden", Address="Bredegate 5", Zipcode=4321, City="Trondheim", Phone=12345678, Contact="John Olsen", Wholesale=1 },
                new Dealer {CustomerNo=4, CompanyName="Retro-Vinyl Brygga", Address="Bergensveien 34", Zipcode=2222, City="Bergen", Phone=12345678, Contact="Per Larsen", Wholesale=2 },
                new Dealer {CustomerNo=5, CompanyName="Retro-Vinyl Arkaden", Address="Stavangerveien 99", Zipcode=3333, City="Stavanger", Phone=12345678, Contact="Mia Hansen", Wholesale=2 }
            };

            var sales = new List<Sale> //Salg
            {
                new Sale {Dealer=0, CustomerName="Kunde1", Price=539.90},
                new Sale {Dealer=0, CustomerName="Kunde2", Price=123.32},
                new Sale {Dealer=1, CustomerName="Kunde3", Price=258.00},
                new Sale {Dealer=1, CustomerName="Kunde4", Price=99.50},
                new Sale {Dealer=1, CustomerName="Kunde5", Price=299.99},
            };

            ChooseWholesaler(wholesalers, dealers, sales);
        }

        //Velg grossist
        static void ChooseWholesaler(List<Wholesale> wholesalers, List<Dealer> dealers, List<Sale> sales)
        {
            Console.WriteLine("Grossister:");
            foreach (var item in wholesalers)
            {
                Console.WriteLine("{0} {1}, Adresse: {2}, {3} {4}, TLF: {5}, Bedriftskontakt: {6}, Distrikt: {7}, Antall forhandlere: {8}", item.CustomerNo, item.CompanyName, item.Address, item.Zipcode, item.City, item.Phone, item.Contact, item.District, item.NoOfDealers);
            }
            Boolean valid = false;
            while (!valid)
            {
                Console.Write("Velg en grossist: ");
                string value = Console.ReadLine();

                if (int.TryParse(value, out wholesaler) && wholesaler >= 0 && wholesaler < wholesalers.Count)
                {
                    valid = true;
                    foreach (var item in wholesalers)
                    {
                        if (item.CustomerNo == wholesaler)
                        {
                            Console.WriteLine("Du valgte følgende grossist: {0}", item.CompanyName);
                            WholesalerMenu(wholesalers, dealers, sales);
                        }
                    }
                }
                else
                    Console.WriteLine("Du må velge en grossist for å fortsette.");
            }
        }

        //Grossistmeny
        static void WholesalerMenu(List<Wholesale> wholesalers, List<Dealer> dealers, List<Sale> sales)
        {
            Wholesale w = new Wholesale();
            Boolean valid = false;
            while (!valid)
            {
                Console.WriteLine("Velg en av følgende alternativer:");
                Console.WriteLine("1. List alle forhandlere.");
                Console.WriteLine("2. Sum av grossistens salg.");
                Console.WriteLine("3. Velg ny grossist.");
                string userInput = Console.ReadLine();
                int menuChoice;
                if (int.TryParse(userInput, out menuChoice) && menuChoice > 0 && menuChoice < 4)
                {
                    valid = true;
                    switch (menuChoice)
                    {
                        case 1:
                            w.ListAllDealers(dealers, wholesaler);
                            ChooseDealer(dealers);
                            DealerMenu(wholesalers, dealers, sales);
                            break;
                        case 2:
                            foreach (var item in wholesalers)
                            {
                                if (item.CustomerNo == wholesaler)
                                {
                                    Console.WriteLine("Totalt salg for {0}: {1} NOK", item.CompanyName, w.TotalSales(dealers, wholesaler, sales));
                                 }
                            }
                            Console.ReadKey();
                            WholesalerMenu(wholesalers, dealers, sales);
                            break;
                        case 3:
                            ChooseWholesaler(wholesalers, dealers, sales);
                            break;
                    }
                }
                else
                    Console.WriteLine("Du må velge fra listen. Prøv igjen.");
            }
        }

        //Velg forhandler
        static void ChooseDealer(List<Dealer> list)
        {
            Boolean valid = false;

            while (!valid)
            {
                Console.WriteLine("Velg en av forhandlerne (Kundenummer): ");
                string userInput = Console.ReadLine();
                foreach (var item in list)
                {
                    if ((int.TryParse(userInput, out dealer)) && (item.Wholesale == wholesaler) && (item.CustomerNo == dealer))
                    {
                        Console.WriteLine("Du har valgt forhandler nr. {0}", dealer);
                        valid = true;
                    }
                }
            }
        }

        //Forhandlermeny
        static void DealerMenu(List<Wholesale> wholesalers, List<Dealer> dealers, List<Sale> sales)
        {
            Boolean valid = false;
            while (!valid)
            {
                Console.WriteLine("Velg en av følgende alternativer:");
                Console.WriteLine("1. Registrer salg.");
                Console.WriteLine("2. List salg.");
                Console.WriteLine("3. Velg ny grossist.");
                string userInput = Console.ReadLine();
                int menuChoice;
                if (int.TryParse(userInput, out menuChoice) && menuChoice > 0 && menuChoice < 4)
                {
                    valid = true;
                    Dealer d = new Dealer();
                    switch (menuChoice)
                    {
                        case 1:
                            Console.Write("Kundens navn: ");
                            String costumer = Console.ReadLine();

                            Console.Write("Pris: ");
                            double price;
                            if (double.TryParse(Console.ReadLine(), out price))
                            {
                                d.AddSale(sales, dealer, costumer, price);
                                Console.WriteLine("Salget er nå registrert.");
                            }
                            else
                                Console.WriteLine("Salget ble ikke registrert. Du må skrive inn en gyldig pris.");
                            DealerMenu(wholesalers, dealers, sales);
                            break;
                        case 2:
                            d.ListSales(sales, dealer);
                            Console.ReadKey();
                            DealerMenu(wholesalers, dealers, sales);
                            break;
                        case 3:
                            ChooseWholesaler(wholesalers, dealers, sales);
                            break;
                    }
                }
                else
                    Console.WriteLine("Du må velge fra listen. Prøv igjen.");
            }
        }
    }
}
