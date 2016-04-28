using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPImageApplication.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<Image> Images { get; set; }
        public string Owner { get; set; }
    }
}
