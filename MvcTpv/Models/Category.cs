using System.Collections.Generic;


namespace MvcTpv.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Category
    {
        [ScaffoldColumn(false)]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryID { get; set; }

        [Required, StringLength(100), Display(Name = "Nombre")]
        public string CategoryName { get; set; }

        [Display(Name = "Descripcion de Categoria")]
        public string Description { get; set; }

        //Vbles navegacion
        public ICollection<Product> Products { get; set; }


        public ICollection<Provider> Provider { get; set; }
    }
}
