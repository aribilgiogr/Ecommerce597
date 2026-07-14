using System.ComponentModel.DataAnnotations;

namespace Core.Abstracts.Bases
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;
        public bool Deleted { get; set; } = false;
        public DateTime? UpdatedAt { get; set; }
    }
}
