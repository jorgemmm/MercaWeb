using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTpv.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class InputDetail
    {
        


        
        [ScaffoldColumn(false)]
        public int InputDetailID { get; set; }

        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        [Display(Name = "Precio De Compra incl. Iva Prov.")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Range(0.00, 100000.00)]
        public decimal? PNETO { get; set; }   //Incluidos los impuestos

        //[Display(Name = "Margen de Beneficio")]
        //public double? Margen { get; set; }       //Como UI

        [Display(Name = "Precio Unitario Incl. Margen y costes")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Range(0.00, 100000.00)]
        public decimal? PVP { get; set; }  //incluide

        [Display(Name="Total Parcial")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Range(0.00, 100000.00)]
         public decimal TotalParcial
        {
                get { return   Cantidad * (decimal) PNETO; }
        }
    
        //Nav Prop
        public int ProductID { get; set; }
        public Product Product { get; set; }

        public int InputID { get; set; }
        public Input Input { get; set; }



    }
}
