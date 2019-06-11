using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTpv.Models.TpvViewModel
{
    public class CustomerIndexData
    {
        
        public IEnumerable<Customer> Customers { get; set; }

         
        public IEnumerable<Sale> Sales { get; set; }

        public IEnumerable<SaleDetail> SaleDetails { get; set; }

    }
}