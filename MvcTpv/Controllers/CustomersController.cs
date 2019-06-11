using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace MvcTpv.Controllers
{
    using MvcTpv.Data;
    using MvcTpv.Models;
    using MvcTpv.Models.TpvViewModel;

    public class CustomersController : Controller
    {
        private readonly MvcTpvContext _context;

        public CustomersController(MvcTpvContext context)
        {

            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index(int? id, int? saleID,string sortOrder, string searchString)
        {

           var viewModel = new CustomerIndexData();

           viewModel.Customers=  await _context.Customers                      
                            .Include(c  => c.Sales)
                                .ThenInclude(i => i.SaleDetails)                                
                            .AsNoTracking()                   
                            .OrderBy(i => i.CustomerID)
                            .ToListAsync();

           ViewData["CurrentSort"] = sortOrder;
           ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
           ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
          
           switch (sortOrder)
            {
                case "name_desc":
                    viewModel.Customers = viewModel.Customers.OrderByDescending(c => c.LastName);
                    break;
                case "Date":
                    viewModel.Customers = viewModel.Customers.OrderBy(c => c.HighDate);
                    break;
                case "date_desc":
                    viewModel.Customers = viewModel.Customers.OrderByDescending(c => c.HighDate);
                    break;
                default:
                    viewModel.Customers = viewModel.Customers.OrderBy(c => c.LastName);
                    break;
            }
            ViewData["CurrentFilter"] = searchString;
           
           
            if (id != null)
            {
                ViewData["CustomerID"] = id.Value;

                //viewModel.Customers = await _context.Customers
                //           .Include(c => c.Sales)
                //               .ThenInclude(i => i.SaleDetails)
                //           //.AsNoTracking()
                //           .OrderBy(i => i.CustomerID)
                //           .ToListAsync();

                Customer customer = viewModel.Customers
                                   .Where( p => p.CustomerID == id.Value)
                                   .Single();

                viewModel.Sales= customer.Sales.ToList();   
                                                
                
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                var customer = from c in _context.Customers
                               select c;
                customer = customer.Where(c => c.FirstMidName.ToUpper().Contains(searchString.ToUpper())
                                            || c.LastName.ToUpper().Contains(searchString.ToUpper())
                                            || c.Direccion.ToUpper().Contains(searchString.ToUpper())
                                            || c.Email.Contains(searchString)
                                            || c.TipoDocumento.Contains(searchString)
                                            || c.NumDocumento.Contains(searchString)
                                            || c.Telefono.Contains(searchString)

                                                           );

                viewModel.Customers = customer.ToList();
            }

            return View(viewModel);
            //return View(await customer.ToListAsync());
            //return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //Aquí también lista las ventas de cada cliente

            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .SingleOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstMidName,LastName,HighDate,Email,TipoDocumento,NumDocumento,Direccion,Telefono")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                                .AsNoTracking()
                                .SingleOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
            //, [Bind("FirstMidName,LastName,HighDate,Email,TipoDocumento,NumDocumento,Direccion,Telefono")] Customer customer)
        {
            if (id == null)
            {
                return NotFound();
            }


            var customerToUpdate = await _context.Customers.SingleOrDefaultAsync(s => s.CustomerID == id);
            if (await TryUpdateModelAsync<Customer>(
                customerToUpdate,
                "",
                s => s.FirstMidName, s => s.LastName, s => s.HighDate, s => s.Email, s => s.TipoDocumento, s => s.NumDocumento, s => s.Direccion, s => s.Telefono))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(customerToUpdate);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .SingleOrDefaultAsync(m => m.CustomerID == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.SingleOrDefaultAsync(m => m.CustomerID == id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }
    }
}
