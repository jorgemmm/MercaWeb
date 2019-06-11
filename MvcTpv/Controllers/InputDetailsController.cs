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
    public class InputDetailsController : Controller
    {
        private readonly MvcTpvContext _context;

        public InputDetailsController(MvcTpvContext context)
        {
            _context = context;
        }

        // GET: InputDetails
        public async Task<IActionResult> Index()
        {
            var mvcTpvContext = _context.InputDetails
                            .Include(i => i.Input)
                            .Include(i => i.Product);
            return View(await mvcTpvContext.ToListAsync());
        }

        // GET: InputDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inputDetail = await _context.InputDetails
                .Include(i => i.Input)
                .Include(i => i.Product)
                .SingleOrDefaultAsync(m => m.InputDetailID == id);
            if (inputDetail == null)
            {
                return NotFound();
            }

            return View(inputDetail);
        }

        // GET: InputDetails/Create   //Metodo para añadir detalles a la entrada seleecionada
        public async Task<IActionResult> Create(int? id)
        {

            
              
        var inputDetail = await _context.InputDetails
                                .Include(i => i.Input)
                                .Include(i => i.Product)
                               .AsNoTracking()
                               .SingleOrDefaultAsync(m => m.InputDetailID == id);


                //ViewData["InputID"] = new SelectList(_context.Inputs, "InputID", "InputID");
                // ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Description");
                populateProductDropDownList(inputDetail.ProductID);
                populateInputsDropDownList(inputDetail.InputID);
                return View(inputDetail);
          
            
        }

        // POST: InputDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cantidad,PNETO,PVP,ProductID,InputID")] InputDetail inputDetail, int? ID, string tipo, string serie, string num)
        {

            //if (ID != null)         
            //{

            //    var inputID = await _context.Inputs
            //                         .Where(i => i.Tipo_Comprobante == tipo && i.serie_comprobante == serie && i.num_comprobante == num)
            //                         .SingleAsync();

                                     
            //}


            if (ModelState.IsValid)
            {
                _context.Add(inputDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Inputs");
                //return RedirectToAction(nameof(Index));
            }
           // ViewData["InputID"] = new SelectList(_context.Inputs, "InputID", "InputID", inputDetail.InputID);
           //  ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Description", inputDetail.ProductID);
            populateProductDropDownList();
            populateInputsDropDownList();
            return RedirectToAction("Create", "Inputs");
            //return View(inputDetail);
        }

        




        // GET: InputDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inputDetail = await _context.InputDetails
                            .Include(i=>i.Input)
                            .AsNoTracking()
                            .SingleOrDefaultAsync(m => m.InputDetailID == id);


            if (inputDetail == null)
            {
                return NotFound();
            }
            // ViewData["InputID"] = new SelectList(_context.Inputs, "InputID", "InputID", inputDetail.InputID);
            //ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "Description", inputDetail.ProductID);
            populateProductDropDownList(inputDetail.ProductID);
            return View(inputDetail);
        }

        // POST: InputDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        //, [Bind("Cantidad,PNETO,PVP,ProductID,InputID")] InputDetail inputDetail)
        {
            if (id == null)
            {
                return NotFound();
            }

              var inputdetailToUpdate = await _context.InputDetails
                                                 .Include(i=>i.Input)
                                                 .SingleOrDefaultAsync(s => s.InputDetailID == id);
             if (await TryUpdateModelAsync<InputDetail>(
                inputdetailToUpdate,
                "",
                s => s.Cantidad, s => s.PNETO, s => s.PVP, s => s.ProductID, s => s.InputID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    // return RedirectToAction(nameof(Index));
                    return RedirectToAction("Index", "Inputs");
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            populateProductDropDownList(inputdetailToUpdate.ProductID);
            return View(inputdetailToUpdate);
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


        private void populateInputsDropDownList(object selectedInput = null)
        {
            //ViewData["InputID"] = new SelectList(_context.Inputs, "InputID", "InputID");
            //var inputQuery = from p in _context.Inputs
            //                   orderby p.InputID
            //                   select p;
            //ViewBag.InputID = new SelectList(inputQuery.AsNoTracking(), "InputID", "InputID", selectedInput);

            var inputQuery = _context.Inputs.Select(x => new
            {
                Id = x.InputID,

                Name = x.InputID.ToString() + " " + x.serie_comprobante + x.num_comprobante

            }
                                                         ).OrderBy(x => x.Id);

            ViewBag.InputID = new SelectList(inputQuery.AsNoTracking(), "Id", "Name", selectedInput);
        }


        // GET: InputDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

          

            var inputDetail = await _context.InputDetails
                .Include(i => i.Input)
                .Include(i => i.Product)
                .SingleOrDefaultAsync(m => m.InputDetailID == id);

           
            //var productToUpdate = _context.Products.Where(p => p.ProductID == inputDetail.ProductID).Single();

            if (inputDetail == null)
            {
                return NotFound();
            }

            return View(inputDetail);
        }

        // POST: InputDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {


            var inputDetail = await _context.InputDetails
                .Include(i=>i.Product)
                .SingleOrDefaultAsync(m => m.InputDetailID == id);

            inputDetail.Product.Stock -= inputDetail.Cantidad;

            _context.InputDetails.Remove(inputDetail);


            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Inputs");
            //return RedirectToAction(nameof(Index));
        }

        private bool InputDetailExists(int id)
        {
            return _context.InputDetails.Any(e => e.InputDetailID == id);
        }
    }
}
