using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPImageApplication.Models
{
    public class Image { public int ImageId { get; set; }
    public string Title { get; set; }
    public string Owner { get; set; }
    public Category Category { get; set; }
    public int CategoryId { get; set; } }
}
