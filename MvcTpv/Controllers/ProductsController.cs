using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcTpv.Data;
using MvcTpv.Models;

namespace MvcTpv.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MvcTpvContext _context;

        public ProductsController(MvcTpvContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string sortOrder,string currentFilter, string searchString)
        {

            var productosToSort = from s in _context.Products                                  
                                  select s; 
            productosToSort = productosToSort
                               //.AsNoTracking()
                               .Include(s => s.Category);

            //var mvcTpvContext = _context.Products.Include(p => p.Category);
             ViewData["CurrentSort"] = sortOrder;
            //Para mostrar en la vista 
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["CategorySortParm"] = sortOrder == "Category" ? "category_desc" : "Category";
            ViewData["CurrentFilter"] = searchString;
            
            switch (sortOrder)
            {
                case "name_desc":
                    productosToSort = productosToSort.OrderByDescending(s => s.ProductName);
                    break;
                case "Category":
                    productosToSort = productosToSort.OrderBy(s => s.Category.CategoryName);
                    break;
                case "category_desc":
                    productosToSort = productosToSort.OrderByDescending(s => s.Category.CategoryName);
                    break;
                default:
                    productosToSort = productosToSort.OrderBy(s => s.ProductID);
                    break;
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                //poner searstring en mayusculas 
                productosToSort = productosToSort.Where(s => s.ProductName.Contains(searchString)
                                                        || s.Description.Contains(searchString)
                                                        || s.Category.CategoryName.Contains(searchString));
            }
        
          

            


            return View(await productosToSort.AsNoTracking().ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                 .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            //ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            
             PopulateCategoriesDropDownList();

            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductName,Description,ImagePath,Codigo,Stock,CategoryID")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateCategoriesDropDownList();
           // ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }
             PopulateCategoriesDropDownList();
           // ViewData["CategoryID"] = new SelectList(_context.Categories, "CategoryID", "CategoryName", product.CategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
      [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)      
        {
            if (id ==null)
            {
                return NotFound();            }


            

        var productToUpdate = await _context.Products
        .SingleOrDefaultAsync(s => s.ProductID == id);

        if (await TryUpdateModelAsync<Product>(
            productToUpdate,
            "",
            s => s.ProductName, 
            s => s.Description, 
            s => s.ImagePath , 
            s => s.Codigo,
            s => s.Stock 
            //,s => s.CategoryID
            ))
        {
            try
            {
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }
             return RedirectToAction(nameof(Index));
        }
        PopulateCategoriesDropDownList();
        return View(productToUpdate);

        }

        private void PopulateCategoriesDropDownList(object selectedCategory = null)
        {
            var categoriesQuery = from c in _context.Categories
                                  orderby c.CategoryName
                                  select c;
            //var categoriesQuery = _context.Categories
            //                      .OrderBy(c =>c.CategoryName);                                  


            ViewBag.CategoryID = new SelectList(categoriesQuery.AsNoTracking(), "CategoryID", "CategoryName", selectedCategory);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .SingleOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.SingleOrDefaultAsync(m => m.ProductID == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
