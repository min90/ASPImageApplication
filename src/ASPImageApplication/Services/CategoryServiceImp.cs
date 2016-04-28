using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;
using ASPImageApplication.Models;

namespace ASPImageApplication.Services
{
    public class CategoryServiceImp : CategoryService
    {
        ApplicationDbContext context;
        public CategoryServiceImp(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<SelectListItem> GetCategories(string id)
        {
            var res = context.Category.Where(i => i.Owner == id);

            var rese = res.Select(x =>
            new SelectListItem()
            {
                Text = x.Name,
                Value = x.CategoryId.ToString()
            });
            return rese;
        }
    }
}
