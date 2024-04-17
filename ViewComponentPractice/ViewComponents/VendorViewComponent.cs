using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using ViewComponentPractice.Models;

namespace ViewComponentPractice.ViewComponents
{
    public class VendorViewComponent: Microsoft.AspNetCore.Mvc.ViewComponent
    {
        private readonly APContext context;
        public VendorViewComponent(APContext _context)
        {

            this.context = _context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var context = new APContext();
            var model = context.Vendors.OrderBy(m=>m.VendorState).Select(m => m.VendorState).Distinct().ToList();
            return View(model);
        }

    }
}
