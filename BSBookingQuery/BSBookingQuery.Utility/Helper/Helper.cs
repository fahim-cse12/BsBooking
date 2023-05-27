using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSBookingQuery.Utility.Helper
{
    public class Helper
    {
        public static ResponseModel Response(bool success, string message, dynamic output)
        {
            return new ResponseModel()
            {
                success = success,
                message = message,
                output = output
            };
        }
    }
}
