using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Data.Configurations
{
	internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
	{
		public void Configure(EntityTypeBuilder<RefreshToken> entity)
		{
			entity.HasKey(x => x.Id);

			entity.Property(x => x.Token)
				.HasMaxLength(1024)
				.IsRequired();

			entity.Property(x => x.JwtId)
				.HasMaxLength(1024)
				.IsRequired();

			entity.Property(x => x.IsRevoked)
				.IsRequired();

			entity.Property(x => x.AddedOn)
				.IsRequired();

			entity.Property(x => x.EndsOn)
				.IsRequired();

			entity.HasOne(rt => rt.User)
				.WithMany(u => u.RefreshTokens)
				.HasForeignKey(rt => rt.UserId)
				.IsRequired();
		}
	}
}
