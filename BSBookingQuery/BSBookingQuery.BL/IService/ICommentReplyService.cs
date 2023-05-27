using BSBookingQuery.Utility.Helper;
using BSBookingQuery.Utility.Models;

namespace BSBookingQuery.BL.IService
{
    public interface ICommentReplyService
    {
        Task<ResponseModel> SubmitComment(CommentModel commentModel);
        Task<ResponseModel> UpdateComment(CommentModel commentModel);
        Task<ResponseModel> DeleteComment(int id);
        Task<ResponseModel> SubmitReply(ReplyModel replyModel);
        Task<ResponseModel> UpdateReply(ReplyModel replyModel);
        Task<ResponseModel> DeleteReply(int id);

    }
}
