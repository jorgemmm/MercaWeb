using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MvcTpv.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Input
    {

        [ScaffoldColumn(false)]
        public int InputID { get; set; }

              
        //[Required] //lo autogenera el controller
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd} at {0:t}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de alta sistema")]
        public DateTime Fecha_hora { get; set; }

        [Column("Tipo Comprobante")]
        [Display(Name = "Tipo Comprobante")]
        public string Tipo_Comprobante { get; set; }

        [Column("Serie Comprobante")]
        [Display(Name = "Serie Comprobante")]
        public string serie_comprobante { get; set; }

        [Column("Numero Comprobante")]
        [Display(Name = "Numero Comprobante")]
        public string num_comprobante { get; set; }

        //[Required]
        [Display(Name = "IVA/IGIC que se añade al Precio Neto")]
        [DataType(DataType.Currency)]
        [Range(0.00, 100.00)]   
        public decimal Impuesto { get; set; }

        //[Required, Display(Name = "Total Venta")]
        [Display(Name = "Total Ingreso")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Range(0.00, 1000000.00)]
        public decimal TotalInput { get; set;}   //la suma de todos los totales parciales de los detalles        
                                                 // TotalInput+ = InputDetail.Cantidad*InputDetail.PrecioVenta

        

        //Nav Prop 
        public int ProviderID { get; set; }
        public Provider Provider { get; set; }
        
        public ICollection<InputDetail> InputDetails { get; set; }

    }
}
