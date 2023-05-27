using BSBookingQuery.BL.IService;
using BSBookingQuery.BL.Service;
using BSBookingQuery.Domain.IRepository;
using BSBookingQuery.Domain.IUnitOfWork;
using BSBookingQuery.Infrastucture.Repository;
using BSBookingQuery.Infrastucture.UnitOfWork;

namespace BSBookingQuery.API.Helper
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<ICommentReplyService, CommentReplyService>();

            // Add any additional services you need here
        }
    }
}
