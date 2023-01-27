using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Services.Rate.Comments
{
	public class CommentsRateService
	{
		private readonly ApplicationDbContext dbContext;

		public CommentsRateService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<int> LikeCommentAsync(string commentId, string userId)
		{
			var comment = await dbContext
				.Comments
				.Include(c => c.CommentsLikes)
				.Include(c => c.CommentsDislikes)
				.FirstOrDefaultAsync(c => c.Id == commentId && !c.IsDeleted);

			var commentLike = new CommentsLikes
			{
				CommentId = commentId,
				UserId = userId
			};

			if (!comment.CommentsLikes.Any(cl => cl.CommentId == commentId && cl.UserId == userId))
			{
				comment.Likes++;
				await dbContext.CommentsLikes.AddAsync(commentLike);
			}
			else if (comment.CommentsDislikes.Any(cd => cd.CommentId == commentId && cd.UserId == userId))
			{
				var dislikeToRemove = comment.CommentsDislikes.FirstOrDefault(cd => cd.UserId == userId && cd.CommentId == commentId);
				dbContext.CommentsDislikes.Remove(dislikeToRemove);
				comment.Dislikes--;

				await dbContext.CommentsLikes.AddAsync(commentLike);
				comment.Likes++;
			}

			await dbContext.SaveChangesAsync();

			return comment.Likes;
		}

		public async Task<int> DislikeCommentAsync(string commentId, string userId)
		{
			var comment = await dbContext
				.Comments
				.Include(c => c.CommentsDislikes)
				.Include(c => c.CommentsLikes)
				.FirstOrDefaultAsync(c => c.Id == commentId && !c.IsDeleted);

			var commentDislike = new CommentsDislikes
			{
				CommentId = commentId,
				UserId = userId
			};

			if (!comment.CommentsDislikes.Any(cd => cd.CommentId == commentId && cd.UserId == userId))
			{
				await dbContext.CommentsDislikes.AddAsync(commentDislike);
				comment.Dislikes++;
			}
			else if (comment.CommentsLikes.Any(cl => cl.CommentId == commentId && cl.UserId == userId))
			{
				var likeToRemove = comment.CommentsLikes.FirstOrDefault(cl => cl.UserId == userId && cl.CommentId == commentId);
				dbContext.CommentsLikes.Remove(likeToRemove);
				comment.Likes--;

				await dbContext.CommentsDislikes.AddAsync(commentDislike);
				comment.Dislikes++;
			}

			await dbContext.SaveChangesAsync();

			return comment.Dislikes;
		}
	}
}
