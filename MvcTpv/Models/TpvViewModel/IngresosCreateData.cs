using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTpv.Models.TpvViewModel
{
    public class IngresosCreateData
    {
        public Input Input { get; set; }

        public IEnumerable<InputDetail> InputDetails { get; set; }
    }
}
