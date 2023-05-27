using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSBookingQuery.Utility.Models
{
    public class HotelDetailsMapper
    {
        public HotelModel hotelModel { get; set; }   
        public List<CommentModel> commentModels { get; set; }
        public List<ReplyModel> replyModels { get; set; }
    }
}
