using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcTpv.Models
{
    using Data;
    using Data;
    public class Product
    {
        [ScaffoldColumn(false)]
        public int ProductID { get; set; }

        [Required, StringLength(100), Display(Name = "Nombre")]
        public string ProductName { get; set; }

        [Required, StringLength(10000), Display(Name = "Descripcion de Producto"), DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string Codigo { get; set; }
        
        [Display(Name="Stock Actual")]
        public int Stock { get; set; } //Actualizar con cantidad de input detail Stock= Stock+ inputDetail.Cant
                                       //Actualizar con cantidad de  saleDetail Stock = Stock-SaleDetails.Cant
     

      //public int StockUpdate(int Cantidad , bool Compra)
      //  {

      //      if (Compra) return Stock += Cantidad;
      //      else return Stock -= Cantidad;
      //  }

      //  public int StockUpdateInput(int ID)
      //  {
      //      foreach (var item in InputDetails)
      //      {
      //         if (item.ProductID==ID) Stock = Stock + item.Cantidad;
      //      }

      //      return Stock;
            
      //  }
      //  public int StockUpdateSale(int ID)
      //  {
      //      foreach (var item in SaleDetails)
      //      {
      //          if (item.ProductID == ID) Stock = Stock - item.Cantidad;
      //      }
      //      return Stock;

      //  }



        public int? CategoryID { get; set; }
        public Category Category { get; set; }
       

        public ICollection<SaleDetail> SaleDetails { get; set; }
        public ICollection<InputDetail> InputDetails { get; set; }
    }
}