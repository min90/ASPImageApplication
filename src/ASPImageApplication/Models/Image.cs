using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPImageApplication.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Owner { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public byte[] Data { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
