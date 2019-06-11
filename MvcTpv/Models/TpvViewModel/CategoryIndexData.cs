using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTpv.Models.TpvViewModel
{
    public class CategoryIndexData
    {
        
        //public Category Category { get; set; }
       
        public IEnumerable<Category> Categories { get; set; }

         
        //Cada categoria asociada una lista de productos  
         public IEnumerable<Product> Products { get; set; }


        //     //Cada PRoducto asociado
        //     //los siguiente detalles
            // public IEnumerable<SaleDetail> SaleDetails { get; set; }

            // public IEnumerable<InputDetail> InputDetails { get; set; }


        // //y a una lista de Provider
        // public IEnumerable<Provider> Providers { get; set; }

        //     //Cada Provider
        //     //una lista de Inputs
        //     public IEnumerable<Input> Inputs { get; set; }
    }
}