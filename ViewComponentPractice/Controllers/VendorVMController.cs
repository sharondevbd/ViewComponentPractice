using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ViewComponentPractice.Models;
using ViewComponentPractice.ViewModels;

namespace ViewComponentPractice.Controllers
{
	public class VendorVMController : Controller
	{
		private readonly APContext _context;
		public VendorVMController(APContext context)
		{
			_context = context;
		}
		public IActionResult Vendors(string name = "")
		{
			var data = _context.Vendors.ToList();
			if (!string.IsNullOrEmpty(name))
			{
				data = data.Where(v => v.VendorState.ToLower().Equals(name.ToLower())).ToList();
			}
			return View(data);
		}
		public IActionResult VendorsInvoice(string name = "")
		{
			var joinData = (from i in _context.Invoices
							join v in _context.Vendors
							on i.VendorId equals v.VendorId
							group i by new {v.VendorId, v.VendorName, v.VendorAddress1, v.VendorAddress2, v.VendorCity,v.VendorState, v.VendorZipCode} into c

		select new VendorVM
		{
			VendorState = c.Key.VendorState,
			Address = $" {c.Key.VendorAddress1 ?? c.Key.VendorAddress2} {c.Key.VendorCity},{c.Key.VendorState},{c.Key.VendorZipCode}",
			Name = c.Key.VendorName,
			TotalInvoice = c.Count(),
			Total = (double) c.Sum(p=>p.InvoiceTotal),
			Paid = (double) c.Sum(p => p.PaymentTotal),
			Due = (c.Sum(p=> p.InvoiceTotal)) - (c.Sum (p=> p.PaymentTotal)) - (c.Sum (p=>p.CreditTotal))
		}).Where(p=>p.VendorState.Equals(name));

			return View(joinData);
		}
		public IActionResult VendorsInvoiceSQL(string name = "")
		{
			var context = new APContext();

			var VendorInvoice = context.Vendors
							  .FromSqlRaw("Select * from Students where Name = 'Bill'")
							  .ToList();

			return View(VendorInvoice);
		}
	}
}





