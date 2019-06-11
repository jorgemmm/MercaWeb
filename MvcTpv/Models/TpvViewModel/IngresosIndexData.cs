using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTpv.Models.TpvViewModel
{
    public class IngresosIndexData
    {
        //public int PageIndex { get; private set; }
        
            
            
        //public int PageIndex { get; set; }
        //public int TotalPages { get; set; }






        public IEnumerable<Input> Inputs { get; set; }
    

        public IEnumerable<InputDetail> InputDetails { get; set; }
        

        // public IEnumerable<Product> Products { get; set; }

        public Product Product { get; set; }

        //public IEnumerable<Input> paging(IEnumerable<Input> inputs, int pageSize, int? page)
        //{
        //    //PageIndex = 1;

        //    var count = Inputs.Count();

        //    TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        //    PageIndex = pageSize;


        //    inputs = inputs.Skip((PageIndex - 1) * pageSize).Take(pageSize).ToList();

        //    return inputs;

        //}


        //public void paging(int pageSize, int? page)
        //{
        //    //PageIndex = 1;

        //    var count = Inputs.Count();

        //    TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        //    PageIndex = pageSize;

        //    if (Inputs != null)
        //    {
        //        Inputs = Inputs.Skip((PageIndex - 1) * pageSize).Take(pageSize).ToList();
        //    }



        //}


        //public bool HasPreviousPage
        //{
        //    get
        //    {
        //        return (PageIndex > 1);
        //    }
        //}

        //public bool HasNextPage
        //{
        //    get
        //    {
        //        return (PageIndex < TotalPages);
        //    }
        //}




    }
}