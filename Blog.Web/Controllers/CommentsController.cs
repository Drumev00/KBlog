using Blog.Infrastructure.Data.Services;
using Blog.Infrastructure.Data.Services.Rate.Comments;
using Blog.Infrastructure.DTOs.Comments;
using Blog.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
	public class CommentsController : MyController
	{
		private readonly CommentsService _commentsService;
		private readonly CommentsRateService _commentsRateService;

		public CommentsController(CommentsService commentsService, CommentsRateService commentsRateService)
		{
			_commentsService = commentsService;
			_commentsRateService = commentsRateService;
		}

		[HttpPost]
		public Task<string> Comment(CommentRequest request)
		{
			return _commentsService.CreateCommentAsync(request.Content, User.GetId(), request.ArticleId);
		}

		[HttpPost("like")]
		public Task<int> Like(string commentId)
		{
			var userId = User.GetId();

			return _commentsRateService.LikeCommentAsync(userId, commentId);
		}

		[HttpPost("dislike")]
		public Task<int> Dislike(string commentId)
		{
			var userId = User.GetId();

			return _commentsRateService.DislikeCommentAsync(userId, commentId);
		}

		[HttpDelete("{commentId}")]
		public Task<string> Delete(string commentId)
		{
			var userId = User.GetId();

			return _commentsService.DeleteCommentAsync(commentId, userId);
		}

		[HttpPut("{commentId}")]
		public Task<string> Edit(EditCommentRequest model)
		{
			var userId = User.GetId();

			return _commentsService.EditCommentAsync(model.CommentId, userId, model.NewContent);
		}
	}
}
