using Blog.Core.Entities;
using Blog.Infrastructure.DTOs.Comments;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Services
{
	public class CommentsService
	{
		private readonly ApplicationDbContext dbContext;

		public CommentsService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task<string> CreateCommentAsync(string content, string userId, string articleId)
		{
			if (string.IsNullOrWhiteSpace(content))
			{
				throw new ArgumentException("Comment have to be at least 1 character long.");
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
				.Where(c => c.Id == commentId && c.UserId == userId && !c.IsDeleted)
				.FirstOrDefaultAsync();

			if (comment == null)
			{
				throw new ArgumentNullException("Such comment does not exists.");
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

			if (!string.IsNullOrWhiteSpace(newContent))
			{

				if (comment.UserId != userId)
				{
					throw new ArgumentException("User can only edit his own comments.");
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

		public async Task<IEnumerable<CommentsListingDTO>> GetAllAsync(string articleId)
		{
			var result = new List<CommentsListingDTO>();

			var comments = dbContext
				.Comments
				.Where(c => c.ArticleId == articleId && !c.IsDeleted)
				.OrderByDescending(c => c.Likes)
				.ThenByDescending(c => c.CreatedOn)
				.AsQueryable();

			if (comments.Count() == 0 || comments == null)
			{
				return result;
			}
			else
			{
				result = await comments.Select(c => new CommentsListingDTO
				{
					Id = c.Id,
					Content = c.Content,
					Like = c.Likes,
					Dislike = c.Dislikes,
					User = new UserCommentModel
					{
						Username = c.User.UserName,
						Email = c.User.Email,
						ProfilePic = c.User.ProfilePic,
						UserId = c.User.Id
					}
				})
				.ToListAsync();

				return result;
			}
		}
	}
}
