using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    interface ICustomer
    {
        int CustomerNo { get; set; }
        string CompanyName { get; set; }
        string Address { get; set; }
        int Zipcode { get; set; }
        string City { get; set; }
        int Phone { get; set; }
        string Contact { get; set; }
    }
}
