using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTpv.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class SaleDetail
    {
        [ScaffoldColumn(false)]
        public int SaleDetailID { get; set; }

        [Required, Display(Name = "Cantidad")]
        [Range(0, 100000)]
        public int Cantidad { get; set; }

        [Display(Name = "Precio de Venta")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Range(0.00, 1000000.00)]
        public decimal PVP { get; set; }  

        [Display(Name = "Descuento")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Range(0.00, 100000.00)]
        public decimal Descuento { get; set; }

        [Display(Name="Total Parcial")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Range(0.00, 100000.00)]
        public decimal TotalParcial
        {
            get { return  Cantidad * (PVP-Descuento) ; }
        }

        //Nav Prop

        public int SaleID { get; set; }
        public Sale Sale { get; set; }

        public int ProductID { get; set; }
        public Product Product { get; set; }
    }
}
