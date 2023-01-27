using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Services.Rate.SubComments
{
	public class SubCommentsRateService
	{
		private readonly ApplicationDbContext dbContext;

		public SubCommentsRateService(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}
		public async Task<int> LikeSubCommentAsync(string subCommentId, string userId)
		{
			var subComment = await dbContext
				.SubComments
				.Include(sc => sc.SubCommentsLikes)
				.Include(sc => sc.SubCommentsDislikes)
				.FirstOrDefaultAsync(c => c.Id == subCommentId && !c.IsDeleted);

			var subCommentLike = new SubCommentsLikes
			{
				SubCommentId = subCommentId,
				UserId = userId
			};

			if (!subComment.SubCommentsLikes.Any(scl => scl.SubCommentId == subCommentId && scl.UserId == userId))
			{
				subComment.Likes++;
				await dbContext.SubCommentsLikes.AddAsync(subCommentLike);
			}
			else if (subComment.SubCommentsDislikes.Any(scd => scd.SubCommentId == subCommentId && scd.UserId == userId))
			{
				var dislikeToRemove = subComment.SubCommentsDislikes.FirstOrDefault(scd => scd.UserId == userId && scd.SubCommentId == subCommentId);
				dbContext.SubCommentsDislikes.Remove(dislikeToRemove);
				subComment.Dislikes--;

				await dbContext.SubCommentsLikes.AddAsync(subCommentLike);
				subComment.Likes++;
			}

			await dbContext.SaveChangesAsync();

			return subComment.Likes;
		}

		public async Task<int> DislikeSubCommentAsync(string subCommentId, string userId)
		{
			var subComment = await dbContext
				.SubComments
				.Include(sc => sc.SubCommentsDislikes)
				.Include(sc => sc.SubCommentsLikes)
				.FirstOrDefaultAsync(c => c.Id == subCommentId && !c.IsDeleted);

			var subCommentDislike = new SubCommentsDislikes
			{
				SubCommentId = subCommentId,
				UserId = userId
			};

			if (!subComment.SubCommentsDislikes.Any(scd => scd.SubCommentId == subCommentId && scd.UserId == userId))
			{
				await dbContext.SubCommentsDislikes.AddAsync(subCommentDislike);
				subComment.Dislikes++;
			}
			else if (subComment.SubCommentsLikes.Any(scl => scl.SubCommentId == subCommentId && scl.UserId == userId))
			{
				var likeToRemove = subComment.SubCommentsLikes.FirstOrDefault(scl => scl.UserId == userId && scl.SubCommentId == subCommentId);
				dbContext.SubCommentsLikes.Remove(likeToRemove);
				subComment.Likes--;

				await dbContext.SubCommentsDislikes.AddAsync(subCommentDislike);
				subComment.Dislikes++;
			}

			await dbContext.SaveChangesAsync();

			return subComment.Dislikes;
		}

	}
}
