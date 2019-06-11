using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace MvcTpv.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public enum Estado
    {
        Emitido, Facturado, Cobrada, Anulada
    }

   public enum Comprobante{
       Recibo, Comanda, Factura, Otro
   }

    public class Sale
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }   // LLave primaria          

      
        [DataType(DataType.DateTime)]
        
        [Display(Name = "Fecha y Hora Venta")] //Autogenerado en el controller cuando create, no binding
        public DateTime Fecha_Hora { get; set; }


       // [Column("Tipo Comprobante")]
        [Display(Name = "Tipo Comprobante")]
        public Comprobante? Tipo_Comprobante { get; set; }

        //[Column("Serie Comprobante")]
        [Display(Name = "Serie Comprobante")]
        public string Serie_comprobante { get; set; }

       // [Column("Numero Comprobante")]
        [Display(Name = "Numero Comprobante")]
        public string Num_comprobante { get; set; }      
        

        //[Required, Display(Name = "Precio Unitario")]
        //public double? UnitPriceSale { get; set; }

        //[ScaffoldColumn(false)]
        [Display(Name = "Total Venta")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        // [Range(0.00, 1000000.00)]
        public decimal? TotalSale { get; set; }   //la suma de todos los totales parciales de los detalles        
                                                 // TotalSale+ = SaleDetail.Cantidad*(SaleDetail.PrecioVenta-Daletail.Descuento)

        
        [Display(Name = "IGIC que se añade al PVP")]
        // [Range(0.00, 10.00)]
        public decimal? Impuesto { get; set; }     //IGIC o IVA 
                                                  // [Required, Display(Name = "Descuento")]
                                                  //public decimal Discount { get; set; }


        [Display(Name = "Estado Venta")]
        public Estado? Estado { get; set; }      // emitida, facturada, cobrada, etc...

      

        //Vables navegación   

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }
        public int SaleManID { get; set; }
        public SaleMan SaleMan { get; set; }

        //public ICollection<Product> Product { get; set; }
        //public int ProductID { get; set; }
        //public Product Product { get; set; }
        public ICollection<SaleDetail> SaleDetails { get; set; }
    }
}
