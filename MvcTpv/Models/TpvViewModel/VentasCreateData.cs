using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTpv.Models.TpvViewModel
{

public class VentasCreateData
    {
        
        public Sale Sale { get; set; }

        public IEnumerable<SaleDetail> SaleDetails { get; set; }

        

    }


}