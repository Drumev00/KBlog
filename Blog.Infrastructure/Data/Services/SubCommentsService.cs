using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Blog.Infrastructure.Data.Services
{
	public class SubCommentsService
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly ILogger<SubCommentsService> _logger;

		public SubCommentsService(ApplicationDbContext dbContext, ILogger<SubCommentsService> logger)
		{
			_dbContext = dbContext;
			_logger = logger;
		}

		public async Task<string> DeleteSubCommentAsync(string subCommentId, string userId)
		{
			var subComment = await _dbContext
				.SubComments
				.FirstOrDefaultAsync(sc => sc.Id == subCommentId && sc.UserId == userId && !sc.IsDeleted);

			var errorMessage = string.Empty;

			if (subComment == null)
			{
				errorMessage = "Such subcomment does not exists.";
				_logger.LogError(errorMessage);
				return errorMessage;
			}

			subComment.IsDeleted = true;
			subComment.DeletedOn = DateTime.UtcNow;

			await _dbContext.SaveChangesAsync();

			return subComment.Id;
		}

		public async Task<string> EditSubCommentAsync(string subCommentId, string userId, string newContent)
		{
			var result = string.Empty;

			var subComment = await _dbContext
				.SubComments
				.FirstOrDefaultAsync(sc => sc.Id == subCommentId && !sc.IsDeleted);
			var errorMessage = string.Empty;

			if (!string.IsNullOrWhiteSpace(newContent) || newContent.Length <= 4000)
			{
				if (subComment == null)
				{
					errorMessage = "Subcomment cannot be found or it is deleted.";
					_logger.LogError(errorMessage);
					return errorMessage;
				}

				if (subComment.UserId != userId)
				{
					errorMessage = "User can only edit his own subcomments.";
					_logger.LogError(errorMessage);
					return errorMessage;
				}

				subComment.Content = newContent;
				subComment.ModifiedOn = DateTime.UtcNow;

				await _dbContext.SaveChangesAsync();

				result = newContent;
			}
			else
			{
				result = subComment.Content;
			}

			return result;

		}

		public async Task<string> SubCommentAsync(string content, string userId, string rootCommentId)
		{
			var errorMessage = string.Empty;

			if (string.IsNullOrWhiteSpace(content))
			{
				errorMessage = "Comments with no content cannot exist.";
				_logger.LogError(errorMessage);
				return errorMessage;
			}

			var subComment = new SubComment
			{
				Content = content,
				UserId = userId,
				RootCommentId = rootCommentId
			};

			await _dbContext.SubComments.AddAsync(subComment);
			await _dbContext.SaveChangesAsync();

			return subComment.Id;
		}
	}
}
