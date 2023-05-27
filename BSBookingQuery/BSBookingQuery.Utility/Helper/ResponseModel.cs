using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSBookingQuery.Utility.Helper
{
    public class ResponseModel
    {
        public bool success { get; set; }
        public string message { get; set; }
        public dynamic output { get; set; }
    }  
   
}
