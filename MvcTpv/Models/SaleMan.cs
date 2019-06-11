using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTpv.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class SaleMan
    {
        [ScaffoldColumn(false)]
        public int SaleManID { get; set; }

        [Required]
        [Column("First Name")]
        [Display(Name = "Nombre o Razón Social")]
        [StringLength(50)]
        public string FirstMidName { get; set; }

        //[Required]
        [Display(Name = "Apellidos")]
        [StringLength(50)]
        public string LastName { get; set; }


        [Required] //lo autogenera el controller
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de alta sistema")]
        public DateTime HighDate { get; set; }
       

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Tipo Documento")]
        public string TipoDocumento { get; set; }

        [Display(Name = "Numero Documento")]
        public string NumDocumento { get; set; }

        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
