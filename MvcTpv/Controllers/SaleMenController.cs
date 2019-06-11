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

    public class SaleMenController : Controller
    {
        private readonly MvcTpvContext _context;

        public SaleMenController(MvcTpvContext context)
        {
            _context = context;
        }

        // GET: SaleMen
        public async Task<IActionResult> Index(int? id, int? saleID, string sortOrder, string searchString)
        {
           var viewModel = new SaleManIndexData();
           viewModel.SaleMans=  await _context.SaleMans                      
                            .Include(c  => c.Sales)
                                .ThenInclude(i => i.SaleDetails)                                
                            .AsNoTracking()                   
                            .OrderBy(i => i.SaleManID)
                            .ToListAsync();

           ViewData["CurrentSort"] = sortOrder;
           ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
           ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
          
          switch (sortOrder)
            {
                case "name_desc":
                    viewModel.SaleMans = viewModel.SaleMans.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    viewModel.SaleMans = viewModel.SaleMans.OrderBy(s => s.HighDate);
                    break;
                case "date_desc":
                    viewModel.SaleMans = viewModel.SaleMans.OrderByDescending(s => s.HighDate);
                    break;
                default:
                    viewModel.SaleMans = viewModel.SaleMans.OrderBy(s => s.LastName);
                    break;
            }
            ViewData["CurrentFilter"] = searchString;
           
           
            if (id != null)
            {
                ViewData["SaleManID"] = id.Value;

                //viewModel.SaleMans = await _context.SaleMans
                //           .Include(c => c.Sales)
                //               .ThenInclude(i => i.SaleDetails)
                //           //.AsNoTracking()
                //           .OrderBy(i => i.SaleManID)
                //           .ToListAsync();

                SaleMan saleman = viewModel.SaleMans
                                 .Where( p => p.SaleManID == id.Value)
                                 .Single();

                viewModel.Sales = saleman.Sales.ToList();
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                var saleman = from s in _context.SaleMans
                              select s;
                saleman = saleman.Where(s => s.FirstMidName.ToUpper().Contains(searchString.ToUpper())
                                            || s.LastName.ToUpper().Contains(searchString.ToUpper())
                                            || s.Direccion.ToUpper().Contains(searchString.ToUpper())
                                            || s.Email.Contains(searchString)
                                            || s.TipoDocumento.Contains(searchString)
                                            || s.NumDocumento.Contains(searchString)
                                            || s.Telefono.Contains(searchString)

                                                           );
                viewModel.SaleMans = saleman.ToList();

            }
            return View(viewModel);
            //return View(await customer.ToListAsync());
            //return View(await _context.SaleMans.ToListAsync());
        }

        // GET: SaleMen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleMan = await _context.SaleMans
                .SingleOrDefaultAsync(m => m.SaleManID == id);
            if (saleMan == null)
            {
                return NotFound();
            }

            return View(saleMan);
        }

        // GET: SaleMen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SaleMen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstMidName,LastName,HighDate,Email,TipoDocumento,NumDocumento,Direccion,Telefono")] SaleMan saleMan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleMan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(saleMan);
        }

        // GET: SaleMen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleMan = await _context.SaleMans
                        .AsNoTracking()
                        .SingleOrDefaultAsync(m => m.SaleManID == id);
            if (saleMan == null)
            {
                return NotFound();
            }
            return View(saleMan);
        }

        // POST: SaleMen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        //, [Bind("FirstMidName,LastName,HighDate,Email,TipoDocumento,NumDocumento,Direccion,Telefono")] SaleMan saleMan)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salementoToUpdate = await _context.SaleMans.SingleOrDefaultAsync(s => s.SaleManID == id);
            if (await TryUpdateModelAsync<SaleMan>(
                salementoToUpdate,
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
            return View(salementoToUpdate);
        }

        // GET: SaleMen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleMan = await _context.SaleMans
                .SingleOrDefaultAsync(m => m.SaleManID == id);
            if (saleMan == null)
            {
                return NotFound();
            }

            return View(saleMan);
        }

        // POST: SaleMen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleMan = await _context.SaleMans.SingleOrDefaultAsync(m => m.SaleManID == id);
            _context.SaleMans.Remove(saleMan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleManExists(int id)
        {
            return _context.SaleMans.Any(e => e.SaleManID == id);
        }
    }
}
