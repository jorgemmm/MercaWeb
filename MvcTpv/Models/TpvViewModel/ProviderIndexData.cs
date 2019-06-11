using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTpv.Models.TpvViewModel
{
    public class ProviderIndexData
    {
        
        public IEnumerable<Provider> Providers { get; set; }

         
        public IEnumerable<Input> Inputs { get; set; }

        public IEnumerable<InputDetail> InputDetails { get; set; }

    }
}