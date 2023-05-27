using BSBookingQuery.BL.IService;
using BSBookingQuery.Utility.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BSBookingQuery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private IHotelService _hotelService;
        public HotelController(IHotelService hotelservice)
        {
            _hotelService = hotelservice;
        }

        [HttpGet]
        [Route("getallhotel")]
        public async Task<IActionResult> GetAllHotel()
        {
            var response = await _hotelService.GetAllHotel();
            if (response.success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.message);
            }
        }

        [HttpPost]
        [Route("savehotel")]
        public async Task<IActionResult> SaveHotel(HotelModel hotelModel)
        {
            var response = await _hotelService.SaveHotel(hotelModel);
            if (response.success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.message);
            }
        }
        [HttpPost]
        [Route("updatehotel")]
        public async Task<IActionResult> UpdateHotel(HotelModel hotelModel)
        {
            var response = await _hotelService.UpdateHotel(hotelModel);
            if (response.success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.message);
            }
        }
        [HttpDelete]
        [Route("deletehotel/{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            var response = await _hotelService.DeleteHotel(id);
            if (response.success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.message);
            }
        }
        [HttpGet]
        [Route("gethotelbyid/{id}")]
        public async Task<IActionResult> GetHotelDetilsById(int id)
        {
            var response = await _hotelService.GetHotelDetailsById(id);
            if (response.success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.message);
            }
        }
        [HttpGet]
        [Route("searchhotel")]
        public async Task<IActionResult> SearchHotel(string? location, int? minRating, int? maxRating, string? name)
        {
            var response = await _hotelService.SearchHotel(location, minRating, maxRating, name);
            if (response.success)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response.message);
            }
        }

    }
}
