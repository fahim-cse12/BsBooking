using BSBookingQuery.Utility.Helper;
using BSBookingQuery.Utility.Models;

namespace BSBookingQuery.BL.IService
{
    public interface IHotelService
    {
        Task<ResponseModel> GetHotelDetailsById(int id);
        Task<ResponseModel> GetAllHotel();
        Task<ResponseModel> SaveHotel(HotelModel hotelModel);
        Task<ResponseModel> UpdateHotel(HotelModel hotelModel);
        Task<ResponseModel> DeleteHotel(int id);
        Task<ResponseModel> SearchHotel(string? location, int? minRating, int? maxRating, string? name);
    }
}
