using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTpv.Models.TpvViewModel
{

        public class VentasIndexData
        {
  
         public IEnumerable<Sale> Sales { get; set; }

         public IEnumerable<SaleDetail> SaleDetails { get; set; }  

         public Product Product { get; set; }   

        }
    
}