using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTpv.Models.TpvViewModel
{
    public class ProductIndexData
    {
        
        public IEnumerable<Product> Products { get; set; }

         
        public IEnumerable<SaleDetail> SaleDetails { get; set; }

        public IEnumerable<InputDetail> InputDetails { get; set; }

    }
}