using BSBookingQuery.BL.IService;
using BSBookingQuery.Domain.Entities;
using BSBookingQuery.Domain.IUnitOfWork;
using BSBookingQuery.Utility.Helper;
using BSBookingQuery.Utility.Models;
using System.Data;
using System.Data.SqlClient;

namespace BSBookingQuery.BL.Service
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _iunitOfWork;
        public HotelService(IUnitOfWork unitorwork)
        {
            _iunitOfWork = unitorwork;
        }

        public async Task<ResponseModel> DeleteHotel(int id)
        {
            try
            {
                if (id != null)
                {
                    var result = await _iunitOfWork.HotelRepository.DeleteAsync(id);

                    if (result)
                    {
                        return Helper.Response(true, "Deleted successfully ", id);
                    }
                    else
                    {
                        return Helper.Response(false, "Data has not deleted", id);
                    }
                }
                else
                {
                    return Helper.Response(false, "Hotel id should not be null", null);
                }
            }
            catch (Exception ex)
            {
                Print("DeleteHotel", ex.Message + " | Inner Exception: " + Convert.ToString(ex.InnerException));
                return Helper.Response(false, "Something Went Wrong !", null);
            }
        }

        public async Task<ResponseModel> GetAllHotel()
        {
            try
            {
                var allHotel = await _iunitOfWork.HotelRepository.GetAllAsync();
                if (allHotel.Count > 0)
                {
                    return Helper.Response(true, string.Empty, allHotel);
                }
                else
                {
                    return Helper.Response(false, "Data not found", null);
                }
            }
            catch (Exception ex)
            {
                Print("GetAllHotel", ex.Message + " | Inner Exception: " + Convert.ToString(ex.InnerException));
                return Helper.Response(false, "Something Went Wrong !", null);
            }
        }

        public async Task<ResponseModel> GetHotelDetailsById(int id)
        {
            try
            {
                HotelDetailsMapper mapper = new HotelDetailsMapper();
                var hotelInfo = await _iunitOfWork.HotelRepository.GetByIdAsync(id);
                if (hotelInfo != null)
                {
                    HotelModel hotelModel = new HotelModel
                    {
                        Id = hotelInfo.Id,
                        Name = hotelInfo.Name,
                        Location = hotelInfo.Location,
                        Rating = hotelInfo.Rating
                    };
                    mapper.hotelModel = hotelModel;

                    var commentsListOfThisHotel = await _iunitOfWork.CommentRepository.GetManyAsync(i => i.HotelId == hotelInfo.Id);
                    if (commentsListOfThisHotel.Count() > 0)
                    {
                        foreach (var item in commentsListOfThisHotel)
                        {
                            CommentModel commentsModel = new CommentModel();
                            commentsModel.Id = item.Id;
                            commentsModel.Message = item.Message;
                            commentsModel.CommenterName = item.CommenterName;
                            commentsModel.HotelId = item.HotelId;

                            var allreply = await _iunitOfWork.ReplyRepository.GetManyAsync(x => x.CommentId == item.Id);
                            if (allreply != null || allreply.Count() > 0)
                            {
                                foreach (var reply in allreply)
                                {
                                    ReplyModel replyModel = new ReplyModel
                                    {
                                        Id = reply.Id,
                                        Message = reply.Message,
                                        CommentId = reply.CommentId
                                    };

                                    mapper.replyModels.Add(replyModel);
                                }

                            }

                            mapper.commentModels.Add(commentsModel);

                        }

                    }

                    return Helper.Response(true, string.Empty, mapper);
                }
                else
                {
                    return Helper.Response(false, "Data not found", null);
                }
            }
            catch (Exception ex)
            {
                Print("GetHotelDetailsById", ex.Message + " | Inner Exception: " + Convert.ToString(ex.InnerException));
                return Helper.Response(false, "Something Went Wrong !", null);
            }
        }

        public async Task<ResponseModel> SaveHotel(HotelModel hotelModel)
        {
            try
            {

                if (hotelModel == null)
                {
                    return Helper.Response(false, "Model should not be null to save", null);

                }

                Hotel hotel = new Hotel
                {
                    Id = 0,
                    Location = hotelModel.Location,
                    Name = hotelModel.Name,
                    Rating = hotelModel.Rating
                };
                _iunitOfWork.BeginTransaction();
                _iunitOfWork.HotelRepository.Insert(hotel);
                await _iunitOfWork.SaveAsync();
                _iunitOfWork.CommitTransaction();

                return Helper.Response(true, "Hotel has been saved successfully", hotel.Id);


            }
            catch (Exception ex)
            {
                Print("SaveHotel", ex.Message + " | Inner Exception: " + Convert.ToString(ex.InnerException));
                _iunitOfWork.RollbackTransaction();
                return Helper.Response(false, "Something Went Wrong !", null);
            }
        }

        public async Task<ResponseModel> UpdateHotel(HotelModel hotelModel)
        {
            try
            {
                if (hotelModel == null)
                {
                    return Helper.Response(false, "Model should not be null to save", null);

                }

                Hotel hotel = new Hotel
                {
                    Id = hotelModel.Id,
                    Location = hotelModel.Location,
                    Name = hotelModel.Name,
                    Rating = hotelModel.Rating
                };
                _iunitOfWork.BeginTransaction();
                _iunitOfWork.HotelRepository.Update(hotel);
                await _iunitOfWork.SaveAsync();
                _iunitOfWork.CommitTransaction();
                return Helper.Response(true, "Hotel has been Modified successfully", hotel.Id);


            }
            catch (Exception ex)
            {
                Print("UpdateHotel", ex.Message + " | Inner Exception: " + Convert.ToString(ex.InnerException));
                _iunitOfWork.RollbackTransaction();
                return Helper.Response(false, "Something Went Wrong !", null);
            }
        }
       
        public async Task<ResponseModel> SearchHotel(string? location,int? minRating, int? maxRating, string? name)
        {
            try
            {
                List<HotelModel> hotelModellist = new List<HotelModel>();
              
                var hotels = _iunitOfWork.HotelRepository.SearchHotels(location, minRating, maxRating, name);

                if (hotels.Count() > 0)
                {
                    foreach (var item in hotels)
                    {
                        HotelModel hotelModel = new HotelModel
                        {
                            Id = item.Id,
                            Location = item.Location,
                            Name = item.Name,
                            Rating = item.Rating
                        };
                        hotelModellist.Add(hotelModel);
                    }

                    return Helper.Response(true, string.Empty, hotelModellist);
                }
                else
                {
                    return Helper.Response(false, "Hotel not found for this criteria", null);
                }
            }
            catch (Exception ex)
            {
                Print("SearchHotel", ex.Message + " | Inner Exception: " + Convert.ToString(ex.InnerException));
              
                return Helper.Response(false, "Something Went Wrong !", null);
            }

        }

        #region Error
        private static void Print(string method
             , string msg)
        {
            ErrorLogs.PrintError("HotelService"
                , method
                , msg);
        }

       

        #endregion Error
    }
}
