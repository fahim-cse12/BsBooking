using BSBookingQuery.BL.IService;
using BSBookingQuery.Utility.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSBookingQuery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentReplyController : ControllerBase
    {
        private ICommentReplyService _commentService;
        public CommentReplyController(ICommentReplyService comment)
        {
            _commentService = comment;
        }

        [HttpPost]
        [Route("submitcomment")]
        public async Task<IActionResult> SubmitComment(CommentModel commentModel)
        {
            var response = await _commentService.SubmitComment(commentModel);
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
        [Route("updatecomment")]
        public async Task<IActionResult> UpdateComment(CommentModel commentModel)
        {
            var response = await _commentService.UpdateComment(commentModel);
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
        [Route("deletecomment/{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var response = await _commentService.DeleteComment(id);
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
        [Route("submitreply")]
        public async Task<IActionResult> SubmitReply(ReplyModel replyModel)
        {
            var response = await _commentService.SubmitReply(replyModel);
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
        [Route("updatereply")]
        public async Task<IActionResult> UpdateReply(ReplyModel replyModel)
        {
            var response = await _commentService.UpdateReply(replyModel);
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
        [Route("deletereply/{id}")]
        public async Task<IActionResult> DeleteReply(int id)
        {
            var response = await _commentService.DeleteReply(id);
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
