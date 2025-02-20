using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Book
{
    public class CreateOrUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PageCount { get; set; }
        public string Excerpt { get; set; } = string.Empty;
        public DateTime PublishDate { get; set; }
    }
}
