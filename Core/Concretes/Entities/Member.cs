using Core.Concretes.Enums;
using Microsoft.AspNetCore.Identity;

namespace Core.Concretes.Entities
{
    public class Member : IdentityUser
    {
        public MemberType MemberType { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool Active { get; set; } = true;

        public virtual Admin? AdminProfile { get; set; }
        public virtual Customer? CustomerProfile { get; set; }
        public virtual Store? StoreProfile { get; set; }
    }
}
