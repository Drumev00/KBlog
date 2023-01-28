using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Blog.Infrastructure.Data.Services
{
	public class CommentsService
	{
		private readonly ApplicationDbContext dbContext;
		private readonly ILogger<CommentsService> _logger;

		public CommentsService(ApplicationDbContext dbContext, ILogger<CommentsService> logger)
		{
			this.dbContext = dbContext;
			this._logger = logger;
		}

		public async Task<string> CreateCommentAsync(string content, string userId, string articleId)
		{
			var errorMessage = string.Empty;
			if (string.IsNullOrWhiteSpace(content))
			{
				errorMessage = "Comment have to be at least 1 character long.";
				_logger.LogError(errorMessage);
				return errorMessage;
			}

			var comment = new Comment
			{
				ArticleId = articleId,
				Content = content,
				UserId = userId
			};

			await dbContext.Comments.AddAsync(comment);
			await dbContext.SaveChangesAsync();

			return comment.Content;
		}

		public async Task<string> DeleteCommentAsync(string commentId, string userId)
		{
			var comment = await dbContext
				.Comments
				.FirstOrDefaultAsync(c => c.Id == commentId && c.UserId == userId && !c.IsDeleted);
			var errorMessage = string.Empty;

			if (comment == null)
			{
				errorMessage = "Such comment does not exists.";
				_logger.LogError(errorMessage);
				return errorMessage;
			}

			comment.IsDeleted = true;
			comment.DeletedOn = DateTime.UtcNow;

			await dbContext.SaveChangesAsync();

			return comment.ArticleId;
		}

		public async Task<string> EditCommentAsync(string commentId, string userId, string newContent)
		{
			var result = string.Empty;

			var comment = await dbContext
				.Comments
				.FirstOrDefaultAsync(c => c.Id == commentId && !c.IsDeleted);
			var errorMessage = string.Empty;


			if (!string.IsNullOrWhiteSpace(newContent))
			{

				if (comment.UserId != userId)
				{
					errorMessage = "User can only edit his own comments.";
					_logger.LogError(errorMessage);
					return errorMessage;
				}

				comment.Content = newContent;
				comment.ModifiedOn = DateTime.UtcNow;

				await dbContext.SaveChangesAsync();

				result = newContent;
			}
			else
			{
				result = comment.Content;
			}

			return result;
		}
	}
}
