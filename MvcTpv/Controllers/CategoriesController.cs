using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using MvcTpv.Data;
//using MvcTpv.Models;

namespace MvcTpv.Controllers
{
    using MvcTpv.Data;
    using MvcTpv.Models;
    using MvcTpv.Models.TpvViewModel;
    public class CategoriesController : Controller
    {
        private readonly MvcTpvContext _context;

        public CategoriesController(MvcTpvContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index(int? id, string sortOrder, string searchString)
        {
              
           var viewModel = new CategoryIndexData();
           viewModel.Categories = await _context.Categories
                                        .Include(i => i.Products)
                                        
            // viewModel.Categories = await _context.Categories
            //                  .Include(i => i.Products)
            //                      .ThenInclude(i => i.InputDetails)
            //                    .Include(i => i.Products)
            //                      .ThenInclude(i => i.SaleDetails)
            //                     .Include(i => i.Provider)
            //                       .ThenInclude(i => i.Inputs)
                            .AsNoTracking()
                            .OrderBy(i => i.CategoryID)
                            .ToListAsync();
            //Categories Sort Order begin
           ViewData["CurrentSort"] = sortOrder;
           ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
           ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
           ViewData["CurrentFilter"] = searchString;
            
            switch (sortOrder)
            {
                case "name_desc":
                    viewModel.Categories = viewModel.Categories.OrderByDescending(s => s.CategoryName);
                    break;
                case "Date":
                    viewModel.Categories = viewModel.Categories.OrderBy(s => s.CategoryID);
                    break;
                case "date_desc":
                    viewModel.Categories = viewModel.Categories.OrderByDescending(s => s.CategoryID);
                    break;
                default:
                    viewModel.Categories = viewModel.Categories.OrderBy(s => s.CategoryName);
                    break;
            }
            //Categories Sort Order End

            //Categories Search

           


            //Categories detail selected
            if (id != null)
            {


                ViewData["CategoryID"] = id.Value;
                viewModel.Categories = await _context.Categories
                                         .Include(i => i.Products)
                                         .OrderBy(i => i.CategoryID)
                                         .ToListAsync();


                Category category =  viewModel.Categories
                                    .Where( c => c.CategoryID == id.Value)
                                    .Single();

                viewModel.Products = category.Products.ToList();                                     
                
            }

            if (!String.IsNullOrEmpty(searchString))
             {
                        
                                        
                var category = from s in _context.Categories
                                select s;
                category = category.Where(s => s.Description.Contains(searchString)
                                        || s.CategoryName.Contains(searchString)
                                        //|| s.CategoryName.ToUpper().Contains(searchString.ToUpper())
                                        );

                viewModel.Categories = category.ToList();

                        
            }
            return View( viewModel);
            // return View(await category.ToListAsync()); 
            //return View(await _context.Categories.ToListAsync());
           

        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
             //Aquí también lista los productos de cada categoría
             //Y ordénalos de más a menos vendidos.
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories

                .Include( c => c.Products)
                .Include( c => c.Provider)
                .AsNoTracking() 
                .SingleOrDefaultAsync(m => m.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryName,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
            //, [Bind("CategoryName,Description")] Category category)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryToUpdate = await _context.Categories.SingleOrDefaultAsync(s => s.CategoryID == id);
            if (await TryUpdateModelAsync<Category>(
                categoryToUpdate,
                "",
                s => s.CategoryName, s => s.Description
                
                ))
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
            return View(categoryToUpdate);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .SingleOrDefaultAsync(m => m.CategoryID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(m => m.CategoryID == id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryID == id);
        }
    }
}
