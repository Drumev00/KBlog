using System.ComponentModel.DataAnnotations;

namespace Blog.Core.Abstractions
{
	public abstract class BaseModel<T> : IAuditInfo
    {
        [Key]
        public T Id { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
