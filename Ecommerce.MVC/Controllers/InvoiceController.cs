using Ecommerce.Application.Services;
using Ecommerce.Dtos.Product;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.MVC.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }
        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var Prds = await _invoiceService.GetAllPagination(5, 0);
            return View(Prds);
        }

      
    }
}
