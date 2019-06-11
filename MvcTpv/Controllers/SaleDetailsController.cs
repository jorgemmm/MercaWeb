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
    public class SaleDetailsController : Controller
    {
        private readonly MvcTpvContext _context;

        public SaleDetailsController(MvcTpvContext context)
        {
            _context = context;
        }

        // GET: SaleDetails
        public async Task<IActionResult> Index()
        {
            var mvcTpvContext = _context.SaleDetails
                                  .Include(s => s.Sale)
                                  .Include(s => s.Product);
                                 
            return View(await mvcTpvContext.ToListAsync());
        }

        // GET: SaleDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetails
                                    .Include(s => s.Product)
                                    .Include(s => s.Sale)
                                    .SingleOrDefaultAsync(m => m.SaleDetailID == id);
            if (saleDetail == null)
            {
                return NotFound();
            }
            //return RedirectToAction("Index", "Sales");
            return View(saleDetail);
        }

        // GET: SaleDetails/Create
        public async Task <IActionResult> Create(int? id)
        {
            
                var saleDetail = await  _context.SaleDetails
                                .Include(i => i.Sale)
                                .Include(i => i.Product)
                                .AsNoTracking()
                                .SingleOrDefaultAsync(m => m.SaleDetailID == id);
           

            populateProductDropDownList(saleDetail.ProductID);
            populateSalesDropDownList(saleDetail.SaleID);

           // ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Description");
            //ViewData["SaleID"] = new SelectList(_context.Sales, "ID", "ID");
            return View(saleDetail);
        }


        // POST: SaleDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cantidad,PVP,Descuento,SaleID,ProductID")] SaleDetail saleDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saleDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Sales");
                //return RedirectToAction(nameof(Index));
            }
            //ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Description", saleDetail.ProductID);
            //ViewData["SaleID"] = new SelectList(_context.Sales, "ID", "ID", saleDetail.SaleID);
            populateProductDropDownList();
            populateSalesDropDownList();
            return RedirectToAction("Create", "Sales");
            //return View(saleDetail);
        }

        // GET: SaleDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetails
                                    .Include(s => s.Sale)
                                    .AsNoTracking()
                                    .SingleOrDefaultAsync(m => m.SaleDetailID == id);
            if (saleDetail == null)
            {
                return NotFound();
            }
            //ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Description", saleDetail.ProductID);
            //ViewData["SaleID"] = new SelectList(_context.Sales, "ID", "ID", saleDetail.SaleID);

            populateProductDropDownList(saleDetail.ProductID);
            return View(saleDetail);
        }

        // POST: SaleDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        //[HttpPost] [ValidateAntiForgeryToken]public async Task<IActionResult> Edit(int id, [Bind("Cantidad,PVP,Descuento,SaleID,ProductID")] SaleDetail saleDetail)

        {
            if (id == null)
            {
                return NotFound();
            }

            var saledetailToUpdate = await _context.SaleDetails
                                            .Include(s => s.Sale)
                                            .SingleOrDefaultAsync(s => s.SaleDetailID == id);
            if (await TryUpdateModelAsync<SaleDetail>(
                saledetailToUpdate,
                "",
                s => s.Cantidad, s => s.PVP, s => s.Descuento, s => s.ProductID, s => s.SaleID))

            {
                try
                {
                    _context.Update(saledetailToUpdate);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaleDetailExists(saledetailToUpdate.SaleDetailID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //  return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Sales");
            }
            //ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Description", saleDetail.ProductID);
            //ViewData["SaleID"] = new SelectList(_context.Sales, "ID", "ID", saleDetail.SaleID);
            populateProductDropDownList(saledetailToUpdate.ProductID);
            return View(saledetailToUpdate);
        }




        private void populateProductDropDownList(object selectedProduct = null)
        {
            //var productQuery =  from p in _context.Products
            //                    orderby p.ProductName
            //                    select p;
            //ViewBag.ProductID = new SelectList(productQuery.AsNoTracking(), "ProductID", "ProductName", selectedProduct);

            var productQuery = _context.Products.Select(x => new
            {
                Id = x.ProductID,

                Name = x.ProductID.ToString() + " " + x.ProductName

            }
                                                         ).OrderBy(x => x.Id);

            ViewBag.ProductID = new SelectList(productQuery.AsNoTracking(), "Id", "Name", selectedProduct);
        }


        private void populateSalesDropDownList(object selectedSale = null)
        {
            //ViewData["InputID"] = new SelectList(_context.Inputs, "InputID", "InputID");
            //var inputQuery = from p in _context.Inputs
            //                   orderby p.InputID
            //                   select p;
            //ViewBag.InputID = new SelectList(inputQuery.AsNoTracking(), "InputID", "InputID", selectedInput);

            var inputQuery = _context.Sales.Select(x => new
            {
                Id = x.ID,

                Name = x.ID.ToString() + " " + x.Serie_comprobante + x.Num_comprobante

            }
                                                         ).OrderBy(x => x.Id);

            ViewBag.SaleID = new SelectList(inputQuery.AsNoTracking(), "Id", "Name", selectedSale);
        }




        // GET: SaleDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleDetail = await _context.SaleDetails
                .Include(s => s.Product)
                .Include(s => s.Sale)
                .SingleOrDefaultAsync(m => m.SaleDetailID == id);
            if (saleDetail == null)
            {
                return NotFound();
            }

            return View(saleDetail);
        }

        // POST: SaleDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var saleDetail = await _context.SaleDetails.SingleOrDefaultAsync(m => m.SaleDetailID == id);
            _context.SaleDetails.Remove(saleDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleDetailExists(int id)
        {
            return _context.SaleDetails.Any(e => e.SaleDetailID == id);
        }
    }
}
