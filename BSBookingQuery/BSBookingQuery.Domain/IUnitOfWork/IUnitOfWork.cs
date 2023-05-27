using BSBookingQuery.Domain.Entities;
using BSBookingQuery.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSBookingQuery.Domain.IUnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<Hotel> HotelRepository { get; }
        IRepository<Comment> CommentRepository { get; }
        IRepository<Reply> ReplyRepository { get; }
        Task<int> SaveAsync();       
        int Save();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
