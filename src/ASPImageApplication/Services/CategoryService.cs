using Microsoft.AspNet.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPImageApplication.Services
{
    public interface CategoryService
    {
        IEnumerable<SelectListItem> GetCategories(string id);
    }
}
