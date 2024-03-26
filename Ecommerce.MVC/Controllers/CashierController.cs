using Ecommerce.Application.Services;
using Ecommerce.Dtos.Cashier;
using Ecommerce.Dtos.Product;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.MVC.Controllers
{
    public class CashierController : Controller
    {
        private readonly ICashierService _cashierService;

        public CashierController(ICashierService cashierService)
        {
            _cashierService = cashierService;
        }
        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var Prds = await _cashierService.GetAllPagination(5, 0);
            return View(Prds);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrUpdateCashierDto Cashier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var Res = await _cashierService.Create(Cashier);
                    if (Res.IsSuccess)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        ViewBag.Error = Res.Message;
                        return View(Cashier);
                    }
                }
                else
                {
                    return View(Cashier);

                }
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
       
        // POST: ProductController/Edit/5
        [HttpPost]
        public ActionResult Edit(int id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
