using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
// using MvcTpv.Data;
// using MvcTpv.Models;

namespace MvcTpv.Controllers
{   
    using MvcTpv.Data;
    using MvcTpv.Models;
    using MvcTpv.Models.TpvViewModel;
    public class ProvidersController : Controller
    {
        private readonly MvcTpvContext _context;

        public ProvidersController(MvcTpvContext context)
        {
            _context = context;
        }

        // GET: Providers
        public async Task<IActionResult> Index(int? id, string sortOrder,string searchString)
        {
            //var mvcTpvContext = _context.Providers.Include(p => p.Category);
           var viewModel = new ProviderIndexData();
           viewModel.Providers=  await _context.Providers                      
                                        .Include(i  => i.Inputs)
                                          .ThenInclude(i => i.InputDetails)                                
                                        .AsNoTracking()                   
                                        .OrderBy(i => i.ProviderID)
                                        .ToListAsync();


            
           //sorting by name and date
           ViewData["CurrentSort"] = sortOrder;
           ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
           ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
          
           switch (sortOrder)
            {
                case "name_desc":
                    viewModel.Providers = viewModel.Providers.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    viewModel.Providers = viewModel.Providers.OrderBy(s => s.HighDate);
                    break;
                case "date_desc":
                    viewModel.Providers = viewModel.Providers.OrderByDescending(s => s.HighDate);
                    break;
                default:
                    viewModel.Providers = viewModel.Providers.OrderBy(s => s.LastName);
                    break;
            }

           ViewData["CurrentFilter"] = searchString;
          //    var provider = from s in _context.Providers
          //                        select s;
          
            if (id != null)
            {
                ViewData["ProviderID"] = id.Value;

                //viewModel.Providers = await _context.Providers
                //                       .Include(i => i.Inputs)
                //                         .ThenInclude(i => i.InputDetails)
                //                       // .AsNoTracking()
                //                       .OrderBy(i => i.ProviderID)
                //                       .ToListAsync();


                Provider provider = viewModel.Providers
                                .Where( p => p.ProviderID == id.Value)
                                .Single();

               viewModel.Inputs= provider.Inputs.ToList();   
                                                 
                
            }

            if (!String.IsNullOrEmpty(searchString))
            {

                var provider = from p in _context.Providers
                               select p;
                provider = provider.Where(p => p.FirstMidName.ToUpper().Contains(searchString.ToUpper())
                                              || p.LastName.ToUpper().Contains(searchString.ToUpper())
                                              || p.Direccion.ToUpper().Contains(searchString.ToUpper())
                                              || p.Email.Contains(searchString)
                                              || p.TipoDocumento.Contains(searchString)
                                              || p.NumDocumento.Contains(searchString)
                                              || p.Telefono.Contains(searchString)

                                                             );

                viewModel.Providers = provider.ToList();
            }
          
            return View(viewModel);
           //return View(await provider.ToListAsync());                         
            //return View(await mvcTpvContext.ToListAsync());
        }

        // GET: Providers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //aquí también lista las entradas de cada provider
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers
                .Include(p => p.Category)
                 .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ProviderID == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // GET: Providers/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Providers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstMidName,LastName,HighDate,Email,TipoDocumento,NumDocumento,Direccion,Telefono,CategoryID")] Provider provider)
        {
            if (ModelState.IsValid)
            {
                _context.Add(provider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName", provider.CategoryID);
            return View(provider);
        }

        // GET: Providers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers
            .AsNoTracking()
            .SingleOrDefaultAsync(m => m.ProviderID == id);
            if (provider == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName", provider.CategoryID);
            return View(provider);
        }

        // POST: Providers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
            //[Bind("FirstMidName,LastName,HighDate,Email,TipoDocumento,NumDocumento,Direccion,Telefono,CategoryID")] Provider provider)
        {
            if (id==null)
            {
                return NotFound();
            }

            var providerToUpdate = await _context.Providers.SingleOrDefaultAsync(s => s.ProviderID == id);
            if (await TryUpdateModelAsync<Provider>(
                providerToUpdate,
                "",
                s => s.FirstMidName, s => s.LastName, s => s.HighDate, s => s.Email, s => s.TipoDocumento, s => s.NumDocumento, s => s.Direccion, s => s.Telefono, s => s.CategoryID))
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
            return View(providerToUpdate);
        }

        // GET: Providers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers
                .Include(p => p.Category)
                .SingleOrDefaultAsync(m => m.ProviderID == id);
            if (provider == null)
            {
                return NotFound();
            }

            return View(provider);
        }

        // POST: Providers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var provider = await _context.Providers.SingleOrDefaultAsync(m => m.ProviderID == id);
            _context.Providers.Remove(provider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProviderExists(int id)
        {
            return _context.Providers.Any(e => e.ProviderID == id);
        }
    }
}
