using Core.Abstracts.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concretes.Entities
{
    public class Cart : BaseEntity
    {
        public string MemeberId { get; set; } = null!;
        public virtual Member Member { get; set; } = null!;

        public virtual ICollection<CartItem> Items { get; set; } = [];
    }
}
