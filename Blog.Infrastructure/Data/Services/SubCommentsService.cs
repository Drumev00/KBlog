using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Services
{
	public class SubCommentsService
	{
		private readonly ApplicationDbContext _dbContext;

		public SubCommentsService(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<string> DeleteSubCommentAsync(string subCommentId, string userId)
		{
			var subComment = await _dbContext
				.SubComments
				.FirstOrDefaultAsync(sc => sc.Id == subCommentId && sc.UserId == userId && !sc.IsDeleted);

			if (subComment == null)
			{
				throw new ArgumentNullException("Such subcomment does not exists.");
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

			if (!string.IsNullOrWhiteSpace(newContent))
			{
				if (subComment == null)
				{
					throw new ArgumentNullException("Subcomment cannot be found or it is deleted.");
				}

				if (subComment.UserId != userId)
				{
					throw new ArgumentException("User can only edit his own subcomments.");
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
			if (string.IsNullOrWhiteSpace(content))
			{
				throw new ArgumentException("Comments with no content cannot exist.");
			}

			var comment = await _dbContext
				.Comments
				.FirstOrDefaultAsync(c => c.Id == rootCommentId && !c.IsDeleted);

			if (comment == null)
			{
				throw new ArgumentNullException("Comment cannot be found.");
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
