using Blog.Infrastructure.Data.Services;
using Blog.Infrastructure.Data.Services.Rate.SubComments;
using Blog.Infrastructure.DTOs.SubComments;
using Blog.Web.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
	public class SubCommentsController : MyController
	{
		private readonly SubCommentsService _subCommentsService;
		private readonly SubCommentsRateService _subCommentsRateService;

		public SubCommentsController(SubCommentsService subCommentsService, SubCommentsRateService subCommentsRateService)
		{
			_subCommentsService = subCommentsService;
			_subCommentsRateService = subCommentsRateService;
		}

		[HttpPost]
		public Task<string> SubComment(SubCommentPostRequest subComment)
		{
			return _subCommentsService.SubCommentAsync(subComment.Content, User.GetId(), subComment.CommentId);
		}

		[HttpPost("like")]
		public Task<int> Like(string subCommentId)
		{
			var userId = User.GetId();

			return _subCommentsRateService.LikeSubCommentAsync(userId, subCommentId);
		}

		[HttpPost("dislike")]
		public Task<int> Dislike(string subCommentId)
		{
			var userId = User.GetId();

			return _subCommentsRateService.DislikeSubCommentAsync(userId, subCommentId);
		}

		[HttpPut("{subCommentId}")]
		public Task<string> Edit(EditSubCommentRequest model)
		{
			var userId = User.GetId();

			return _subCommentsService.EditSubCommentAsync(model.SubCommentId, userId, model.NewContent);
		}

		[HttpDelete("{subCommentId}")]
		public Task<string> Delete(string subCommentId)
		{
			var userId = User.GetId();

			return _subCommentsService.DeleteSubCommentAsync(subCommentId, userId);
		}
	}
}
