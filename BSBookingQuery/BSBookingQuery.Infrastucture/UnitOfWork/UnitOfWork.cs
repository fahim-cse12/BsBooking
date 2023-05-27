using BSBookingQuery.Domain.Entities;
using BSBookingQuery.Domain.IRepository;
using BSBookingQuery.Domain.IUnitOfWork;
using BSBookingQuery.Infrastucture.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSBookingQuery.Infrastucture.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BSDbContext _dbContext;
        private IDbContextTransaction dbContextTransaction;

        private IRepository<Hotel> _hotelRepository;
        private IRepository<Comment> _commentRepository;
        private IRepository<Reply> _replyRepository;

        public UnitOfWork(BSDbContext dbContext)
        {
            this._dbContext = dbContext;    
        }


        public IRepository<Hotel> HotelRepository 
        {
            get
            {
                if (this._hotelRepository == null)
                    this._hotelRepository = new Repository<Hotel>(_dbContext);
                return this._hotelRepository;
            }
        }

        public IRepository<Comment> CommentRepository
        {
            get
            {
                if (this._commentRepository == null)
                    this._commentRepository = new Repository<Comment>(_dbContext);
                return this._commentRepository;
            }
        }
        public IRepository<Reply> ReplyRepository
        {
            get
            {
                if (this._replyRepository == null)
                    this._replyRepository = new Repository<Reply>(_dbContext);
                return this._replyRepository;
            }
        }


        public void BeginTransaction()
        {
            dbContextTransaction = _dbContext.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (dbContextTransaction != null)
            {
                dbContextTransaction.Commit();
            }
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void RollbackTransaction()
        {
            if (dbContextTransaction != null)
            {
                dbContextTransaction.Rollback();
            }
        }

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {

            return await _dbContext.SaveChangesAsync();
        }
    }
}
