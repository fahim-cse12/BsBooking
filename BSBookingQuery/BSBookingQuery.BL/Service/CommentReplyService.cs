using BSBookingQuery.BL.IService;
using BSBookingQuery.Domain.Entities;
using BSBookingQuery.Domain.IUnitOfWork;
using BSBookingQuery.Utility.Helper;
using BSBookingQuery.Utility.Models;

namespace BSBookingQuery.BL.Service
{
    public class CommentReplyService : ICommentReplyService
    {
        private readonly IUnitOfWork _iunitOfWork;
        public CommentReplyService(IUnitOfWork unitorwork)
        {
            _iunitOfWork = unitorwork;
        }

        public async Task<ResponseModel> DeleteComment(int id)
        {
            try
            {
                if (id != null)
                {
                    var result = await _iunitOfWork.CommentRepository.DeleteAsync(id);
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
                Print("DeleteComment", ex.Message + " | Inner Exception: " + Convert.ToString(ex.InnerException));
                return Helper.Response(false, "Something Went Wrong !", null);
            }
        }

        public async Task<ResponseModel> DeleteReply(int id)
        {
            try
            {
                if (id != null)
                {
                    var result = await _iunitOfWork.ReplyRepository.DeleteAsync(id);
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
                Print("DeleteReply", ex.Message + " | Inner Exception: " + Convert.ToString(ex.InnerException));
                return Helper.Response(false, "Something Went Wrong !", null);
            }
        }

        public async Task<ResponseModel> SubmitComment(CommentModel commentModel)
        {
            try
            {
                if (commentModel == null)
                {
                    return Helper.Response(false, "Comment should not be null to save", null);

                }

                Comment comment = new Comment
                {
                    Id = 0,
                    Message = commentModel.Message,
                    CommenterName = commentModel.CommenterName, 
                    HotelId = commentModel.HotelId, 
                };
                _iunitOfWork.BeginTransaction();
                _iunitOfWork.CommentRepository.Insert(comment);
                await _iunitOfWork.SaveAsync();
                _iunitOfWork.CommitTransaction();

                return Helper.Response(true, "Hotel has been saved successfully", comment.Id);


            }
            catch (Exception ex)
            {
                Print("SubmitComment", ex.Message + " | Inner Exception: " + Convert.ToString(ex.InnerException));
                _iunitOfWork.RollbackTransaction();
                return Helper.Response(false, "Something Went Wrong !", null);
            }
        }

        public async Task<ResponseModel> SubmitReply(ReplyModel replyModel)
        {
            try
            {
                if (replyModel == null)
                {
                    return Helper.Response(false, "Comment should not be null to save", null);

                }

                Reply reply = new Reply
                {
                    Id = 0,
                    Message = replyModel.Message,
                    ReplierName = replyModel.ReplierName,
                    CommentId = replyModel.CommentId
                };
                _iunitOfWork.BeginTransaction();
                _iunitOfWork.ReplyRepository.Insert(reply);
                await _iunitOfWork.SaveAsync();
                _iunitOfWork.CommitTransaction();
                return Helper.Response(true, "Hotel has been saved successfully", reply.Id);


            }
            catch (Exception ex)
            {
                Print("SubmitComment", ex.Message + " | Inner Exception: " + Convert.ToString(ex.InnerException));
                _iunitOfWork.RollbackTransaction();
                return Helper.Response(false, "Something Went Wrong !", null);
            }
        }

        public async Task<ResponseModel> UpdateComment(CommentModel commentModel)
        {
            try
            {
                if (commentModel == null)
                {
                    return Helper.Response(false, "Comment should not be null to save", null);

                }

                Comment comment = new Comment
                {
                    Id = commentModel.Id,
                    Message = commentModel.Message,
                    CommenterName = commentModel.CommenterName,
                    HotelId = commentModel.HotelId,
                };
                _iunitOfWork.BeginTransaction();
                _iunitOfWork.CommentRepository.Update(comment);
                await _iunitOfWork.SaveAsync();
                _iunitOfWork.CommitTransaction();

                return Helper.Response(true, "Hotel has been saved successfully", comment.Id);


            }
            catch (Exception ex)
            {
                Print("UpdateComment", ex.Message + " | Inner Exception: " + Convert.ToString(ex.InnerException));
                _iunitOfWork.RollbackTransaction();
                return Helper.Response(false, "Something Went Wrong !", null);
            }
        }

        public async Task<ResponseModel> UpdateReply(ReplyModel replyModel)
        {
            try
            {
                if (replyModel == null)
                {
                    return Helper.Response(false, "Comment should not be null to save", null);

                }

                Reply reply = new Reply
                {
                    Id = replyModel.Id,
                    Message = replyModel.Message,
                    ReplierName = replyModel.ReplierName,
                    CommentId = replyModel.CommentId
                };
                _iunitOfWork.BeginTransaction();
                _iunitOfWork.ReplyRepository.Update(reply);
                await _iunitOfWork.SaveAsync();
                _iunitOfWork.CommitTransaction();
                return Helper.Response(true, "Hotel has been saved successfully", reply.Id);


            }
            catch (Exception ex)
            {
                Print("SubmitComment", ex.Message + " | Inner Exception: " + Convert.ToString(ex.InnerException));
                _iunitOfWork.RollbackTransaction();
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
