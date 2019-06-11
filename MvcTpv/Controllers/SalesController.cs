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
    using  MvcTpv.Models.TpvViewModel;
    using System.Globalization;

    public class SalesController : Controller
    {
        private readonly MvcTpvContext _context;

        public SalesController(MvcTpvContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index(
            int? id,
            int? saleDetailID, 
            string sortOrder,
            string searchString
        )
        {
            //var mvcTpvContext = _context.Sales.Include(s => s.Customer).Include(s => s.SaleMan);
            var viewModel = new VentasIndexData();
            //var mvcTpvContext = 
              viewModel.Sales =  await _context.Sales                      
                            .Include(i  => i.Customer)
                            .Include(i => i.SaleMan)
                           .Include(i => i.SaleDetails)
                               .ThenInclude(i => i.Product)
                            .AsNoTracking()                   
                            .OrderBy(i=>i.ID) // .OrderBy(i => i.Fecha_hora)
                            .ToListAsync();
            //Inputs Sort Order begin
          ViewData["CurrentSort"] = sortOrder;
          ViewData["TipoCompSortParm"] = String.IsNullOrEmpty(sortOrder) ? "comp_desc" : "";
          ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
          ViewData["CustomerSortParm"] = sortOrder =="Customer"? "customer_desc" : "Customer";
          ViewData["SalemanSortParm"] = sortOrder =="Saleman"? "saleman_desc" : "Saleman";
          ViewData["CurrentFilter"] = searchString;



            switch (sortOrder)
            {
                case "comp_desc":
                    viewModel.Sales = viewModel.Sales.OrderByDescending(s => s.Tipo_Comprobante);
                    break;
                case "Date":
                    viewModel.Sales = viewModel.Sales.OrderBy(s => s.Fecha_Hora);
                    break;
                case "date_desc":
                    viewModel.Sales = viewModel.Sales.OrderByDescending(s => s.Fecha_Hora);
                    break;
                case "Customer":
                    viewModel.Sales = viewModel.Sales.OrderBy(s => s.Customer.FirstMidName);
                    break;
                case "customer_desc":
                    viewModel.Sales = viewModel.Sales.OrderByDescending(s => s.Customer.FirstMidName);
                    break;    
                case "Saleman":
                    viewModel.Sales = viewModel.Sales.OrderBy(s => s.SaleMan.FirstMidName);
                    break;
                case "saleman_desc":
                    viewModel.Sales = viewModel.Sales.OrderByDescending(s => s.SaleMan.FirstMidName);
                    break; 
                default:
                    viewModel.Sales = viewModel.Sales.OrderBy(s => s.Tipo_Comprobante);
                    break;
            } 
        //Sale detail selected
            if (id != null)
            {
            ViewData["SaleID"] = id.Value;
                


                Sale sale = viewModel.Sales
                                .Where(i => i.ID == id.Value)
                                .Single();

              viewModel.SaleDetails = sale
                                       .SaleDetails.ToList();

                //product selected
                if (saleDetailID != null)
                {
                    ViewData["InputDetailID"] = saleDetailID.Value;


                    viewModel.Product = viewModel.SaleDetails
                                        .Where(x => x.SaleDetailID == saleDetailID)
                                        .Single().Product;
                }

            }
            
            if (!String.IsNullOrEmpty(searchString))
            {
                                            

                var salesToSort = from s in _context.Sales.Include(i => i.Customer)
                                                            .Include(i => i.SaleMan).Include(s => s.SaleDetails) //para visualizar Total Ingreso sino se cargan los detalles 
                                                                                          //no se visualizará nada en total ingreso.
                                   //.Include(i => i.InputDetails).ThenInclude(i => i.Product) //el producto no es requerido
                                   select s;

                salesToSort = salesToSort.Where(s => s.Num_comprobante.Contains(searchString) // s.Tipo_Comprobante.Contains(searchString)
                                                || s.Serie_comprobante.Contains(searchString)
                                                || s.Customer.FirstMidName.Contains(searchString)
                                                || s.SaleMan.FirstMidName.Contains(searchString)
                                              //||  s.Provider.FirstMidName.ToUpper().Contains(searchString.ToUpper())
                                                );

                viewModel.Sales = salesToSort.ToList();

            }
            return View("Index",viewModel);
            //return View(await mvcTpvContext.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(int? id , bool printing)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                 .Include(i => i.SaleDetails)
                .Include(s => s.Customer)
                .Include(s => s.SaleMan)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (printing) { }
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            PopulatePersonsDropDownList();
            populateProductDropDownList();
            //ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FirstMidName");
            //ViewData["SaleManID"] = new SelectList(_context.SaleMans, "SaleManID", "FirstMidName");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo_Comprobante,Serie_comprobante,Num_comprobante,Impuesto,Estado,CustomerID,SaleManID")] Sale sale ,
             string[] selectedProducts, string[] selectedCants, string[] selectedDTOs, string[] selectedPVPs)
        {

            if (selectedProducts == null)
            {
                return NotFound();
            }
            if (selectedCants == null) for (int i = 0; i < selectedProducts.Length; i++) selectedCants[i] = "10";
            if (selectedDTOs == null) for (int i = 0; i < selectedProducts.Length; i++) selectedDTOs[i] = "5";
            if (selectedPVPs == null) for (int i = 0; i < selectedProducts.Length; i++) selectedPVPs[i] = "200";

            Product prod = new Product();

            sale.SaleDetails = new List<SaleDetail>();
            int j = 0;
            var SubTotal = 0.00m;
            foreach (var product in selectedProducts)
            {

                prod = _context.Products.Where(p => p.ProductID == int.Parse(product)).Single();
                prod.Stock++;
                var productToAdd = new SaleDetail { SaleID = sale.ID, ProductID = int.Parse(product), Cantidad = int.Parse(selectedCants[j]), Descuento = decimal.Parse(selectedDTOs[j],CultureInfo.InvariantCulture), PVP = decimal.Parse(selectedPVPs[j], CultureInfo.InvariantCulture) };


                sale.SaleDetails.Add(productToAdd);
                SubTotal += productToAdd.TotalParcial;


                j++;
            }

            // pon la
            foreach (var detalle in sale.SaleDetails)
            {
                // total += detalle.TotalParcial;
                sale.TotalSale += detalle.Cantidad * (decimal) (detalle.PVP-detalle.Descuento);  //pvp y aplicado dto¿?
            }

            sale.Fecha_Hora = DateTime.Now;
           


            try
            {
                if (ModelState.IsValid)
                {
                   // _context.Add(prod);
                    _context.Add(sale);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {


                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }

           
            //ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FirstMidName", sale.CustomerID);
            //ViewData["SaleManID"] = new SelectList(_context.SaleMans, "SaleManID", "FirstMidName", sale.SaleManID);

            PopulatePersonsDropDownList(sale.CustomerID, sale.SaleManID);
            populateProductDropDownList();
            return View(sale);
        }

        // GET: Sales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
            .AsNoTracking()
            .SingleOrDefaultAsync(m => m.ID == id);
            if (sale == null)
            {
                return NotFound();
            }
           //ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FirstMidName", sale.CustomerID);
           //ViewData["SaleManID"] = new SelectList(_context.SaleMans, "SaleManID", "FirstMidName", sale.SaleManID);
            PopulatePersonsDropDownList(sale.CustomerID,sale.SaleManID);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
           // , [Bind("Fecha_Hora,Tipo_Comprobante,Serie_comprobante,Num_comprobante,TotalSale,Impuesto,Estado,CustomerID,SaleManID")] Sale sale)
        {
            if (id == null)
            {
                return NotFound();
            }

            var saleToUpdate = await _context.Sales.SingleOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Sale>(
                saleToUpdate,
                "",
                s => s.Fecha_Hora, 
               // s => s.Tipo_Comprobante, 
               // s => s.Serie_comprobante, s => s.Num_comprobante, 
                s => s.Impuesto, 
                //s => s.Estado,
                s => s.TotalSale,// poner que no se pueda editar
                s => s.CustomerID, s => s.SaleManID))
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

            //ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FirstMidName", sale.CustomerID);
            //ViewData["SaleManID"] = new SelectList(_context.SaleMans, "SaleManID", "FirstMidName", sale.SaleManID);

            PopulatePersonsDropDownList(saleToUpdate.CustomerID,saleToUpdate.SaleManID);
            return View(saleToUpdate);
        }

          private void PopulatePersonsDropDownList(object selectedCustomer = null,object selectedSaleMan = null )
            {
                var customersQuery = from c in _context.Customers
                                     orderby c.FirstMidName
                                     select c;

               var saleMansQuery =  from s in _context.SaleMans
                                    orderby s.FirstMidName
                                    select s;                   
                //var categoriesQuery = _context.Categories
                //                      .OrderBy(c =>c.CategoryName);                                  


                ViewBag.CustomerID = new SelectList(customersQuery.AsNoTracking(), "CustomerID", "FirstMidName", selectedCustomer);
                ViewBag.SaleManID = new SelectList(saleMansQuery.AsNoTracking(), "SaleManID", "FirstMidName", selectedSaleMan);

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

        // GET: Sales/Delete/5
          public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.SaleMan)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sale = await _context.Sales.SingleOrDefaultAsync(m => m.ID == id);
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.ID == id);
        }
    }
}
