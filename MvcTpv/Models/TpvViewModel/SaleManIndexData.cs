using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTpv.Models.TpvViewModel
{
    public class SaleManIndexData
    {
        
        public IEnumerable<SaleMan> SaleMans { get; set; }

         
        public IEnumerable<Sale> Sales { get; set; }

        public IEnumerable<SaleDetail> SaleDetails { get; set; }

    }
}