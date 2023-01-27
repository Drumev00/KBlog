using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Abstractions
{
	public abstract class BaseDeletableModel<T> : BaseModel<T>, IDeletableEntity
    {
        [Required]
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
