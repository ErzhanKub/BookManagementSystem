using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Basket
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public List<Guid>? BookId { get; set; }
    }
}
