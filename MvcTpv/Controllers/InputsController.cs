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
    using System.Globalization;

    public class InputsController : Controller
    {
        private readonly MvcTpvContext _context;

        public InputsController(MvcTpvContext context)
        {
            _context = context;
        }



        //MEtodo Get
        public async Task<IActionResult> Report(
        //public IActionResult Report(
            DateTime? selectedDate
            //, DateTime? fechaMax
            //, string selectedProduct
            , string selectedProvider
            )
        {

            //ViewData["currentProviderFilter"] = selectedProvider;
            //ViewData["currentDateInicioFilter"] = selectedDate;
            var inputsToSort = await _context.Inputs
                                     .Include(i => i.Provider)
                                     .Include(i => i.InputDetails)                                                                                             
                                         .ThenInclude(i => i.Product)
                                     .AsNoTracking()
                                     .OrderBy(i => i.InputID) // .OrderBy(i => i.Fecha_hora)
                                      .ToListAsync();

           

            //DateTime fechainicioTest = DateTime.Parse("2017/15/10");

       
            //como searchtring en index
            //tranformar 
            if (selectedDate != null)
            {
                string fechaSelected = selectedDate.ToString();
                DateTime fechainicioSelected = DateTime.Parse(fechaSelected);


                inputsToSort = inputsToSort.Where(i => i.Fecha_hora >= fechainicioSelected)//selectedDate)
                                           .ToList();
                if (selectedProvider != null)
                {
                    //.Where(i=>i.Fecha_hora >= selectedDate && i.Provider.FirstMidName==selectedProvider)

                }
            }
            //DateTime fechainicioTest2 = new DateTime(2017, 10, 25);
            //inputsToSort = inputsToSort.Where(i => i.Fecha_hora >= fechainicioTest2)
            //                                           .ToList();

            //IEnumerable<Input> inputs = from i in _context.Inputs



            //formulario 
            //if (id == null)
            //{
            //    return NotFound();
            //}
            //var input = await _context.Inputs
            //    .Include(i => i.Provider)
            //    .SingleOrDefaultAsync(m => m.InputID == id);
            //if (input == null)
            //{
            //    return NotFound();
            //}
            //return View(input);


            //Inputs Search


            PopulateProvidersDropDownList();  //para reportes
            populateProductDropDownList();  //más adelante 
            return View(inputsToSort);
        }

        // GET: Inputs // public async Task<IActionResult> Index()
        public async Task<IActionResult> Index(
            int? id,
            int? inputDetailID, 
            string sortOrder,
            string currentFilter,
            string searchString,
            DateTime? selectedFirstDate
             ,int? page)                
        {


           
            var viewModel = new IngresosIndexData();
         
            //var mvcTpvContext = 
            viewModel.Inputs = await  _context.Inputs
                         .Include(i => i.Provider)
                         .Include(i => i.InputDetails)
                             .ThenInclude(i => i.Product)
                          .AsNoTracking()
                          .OrderBy(i => i.InputID)// .OrderBy(i => i.Fecha_hora)
                          .ToListAsync();
            
         

           //Inputs Sort Order begin
          ViewData["CurrentSort"] = sortOrder;
          ViewData["TipoCompSortParm"] = String.IsNullOrEmpty(sortOrder) ? "comp_desc" : "";
          ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";        
          ViewData["currentFirstDate"] = selectedFirstDate;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

           



            switch (sortOrder)
            {
                case "comp_desc":
                    viewModel.Inputs = viewModel.Inputs.OrderByDescending(s => s.Tipo_Comprobante);//.Take(5).Skip(0); 
                    break;
                case "Date":
                    viewModel.Inputs = viewModel.Inputs.OrderBy(s => s.Fecha_hora);//.Take(5).Skip(0); 
                    break;
                case "date_desc":
                    viewModel.Inputs = viewModel.Inputs.OrderByDescending(s => s.Fecha_hora);//.Take(5).Skip(0); 
                    break;
                default:
                    viewModel.Inputs = viewModel.Inputs.OrderBy(s => s.Tipo_Comprobante);//.Take(5).Skip(0);
                    break;
            }
           
            //Inputs Sort Order end


            //Buscamos primero




            //Input detail selected
            if (id != null)
            {
            ViewData["InputID"] = id.Value;
                


                Input input = viewModel.Inputs
                                
                                .Where(i => i.InputID == id.Value)
                                .Single();

              viewModel.InputDetails = input
                                       .InputDetails.ToList();

                //product selected
                if (inputDetailID != null)
                {
                    ViewData["InputDetailID"] = inputDetailID.Value;


                    viewModel.Product = viewModel.InputDetails
                                        .Where(x => x.InputDetailID == inputDetailID)
                                        .Single().Product;
                }

            }

             
            

            if (!String.IsNullOrEmpty(searchString))
            {
                viewModel.Inputs = GetInputsSorted(searchString).ToList();
            }

            if(selectedFirstDate != null)
            {
                  viewModel.Inputs = GetInputsSorted(selectedFirstDate).ToList();
            }

            PopulateProvidersDropDownList();  //para reportes
            populateProductDropDownList();  //más adelante 
                                            // return View(await mvcTpvContext.ToListAsync());

            int pageSize = 10;
            viewModel.Inputs = paging(viewModel.Inputs, page ?? 1, pageSize);

            return View("Index",viewModel);
          
           //return View(await PaginatedList<IngresosIndexData>.CreateAsync(viewModel, page ?? 1, pageSize));
        }


        public IQueryable<Input> GetInputsSorted(string searchString )
        {
                 var inputsToSort = from s in _context.Inputs.Include(s=>s.Provider).Include(s => s.InputDetails) //para visualizar Total Ingreso sino se cargan los detalles 
                                                                                          //no se visualizará nada en total ingreso.
                                   //.Include(i => i.InputDetails).ThenInclude(i => i.Product) //el producto no es requerido
                                   select s;

                inputsToSort = inputsToSort.Where(s => s.Tipo_Comprobante.Contains(searchString)
                                                || s.num_comprobante.Contains(searchString)
                                                || s.serie_comprobante.Contains(searchString)
                                                || s.Provider.FirstMidName.Contains(searchString)
                                               //||  s.Provider.FirstMidName.ToUpper().Contains(searchString.ToUpper())
                                                );

                return inputsToSort;
        }

        public IQueryable<Input> GetInputsSorted(DateTime? selectedFirstDate)
        {
            var inputsToSort = from s in _context.Inputs.Include(s => s.Provider).Include(s => s.InputDetails) //para visualizar Total Ingreso sino se cargan los detalles 
                                                                                                               //no se visualizará nada en total ingreso.
                                                                                                               //.Include(i => i.InputDetails).ThenInclude(i => i.Product) //el producto no es requerido
                               select s;

            inputsToSort = inputsToSort.Where(s => s.Fecha_hora.Date >= selectedFirstDate.Value.Date
                                       );

            return inputsToSort;
        }

        public IEnumerable<Input> paging(IEnumerable <Input> inputs, int pageIndex, int pageSize) //, IQueryable<T> source)
        {
            //int pageSize = 5;
            //int inicio = 0;
            //if (page != null) { inicio = (int) page * pageSize; }
            //var querypage = inputs.Take(pageSize).Skip(inicio);

            
            var count = inputs.Count();   // el número de detalles

            var  TotalPages = (int)Math.Ceiling(count / (double)pageSize);  //El numero total de paginas

            var pageEnd = count % pageSize;

            if (pageIndex < TotalPages)
            {
                var items = inputs.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();// items por pagina 
                return items;
            }
            else
            {
                
                var items = inputs.Skip((pageIndex - 1)*pageSize).Take(pageEnd).ToList();
                return items;
            }  

            

                

        }


        // GET: Inputs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var input = await _context.Inputs
                .Include(i=>i.InputDetails)
                .Include(i => i.Provider)
                .SingleOrDefaultAsync(m => m.InputID == id);
            if (input == null)
            {
                return NotFound();
            }

            return View(input);
        }

        // GET: Inputs/Create
        public IActionResult Create()
        {
            
            //ViewData["ProviderID"] = new SelectList(_context.Providers, "ProviderID", "FirstMidName");
            PopulateProvidersDropDownList();
            populateProductDropDownList();
            return View();
        }

       


           

        // POST: Inputs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Tipo_Comprobante,serie_comprobante,num_comprobante,Impuesto,ProviderID")] Input input,
       
        string[] selectedProducts, string[] selectedCants, string[] selectedPNETOs, string[] selectedPVPs )
        {
            
            if (selectedProducts == null)
            {
                return NotFound();
            }
            if (selectedCants == null) for (int i = 0; i < selectedProducts.Length; i++) selectedCants[i] = "10";
            if (selectedPNETOs == null) for (int i = 0; i < selectedProducts.Length; i++) selectedPNETOs[i] = "100";
            if (selectedPVPs == null) for (int i = 0; i < selectedProducts.Length; i++) selectedPVPs[i] = "200";
            

            input.InputDetails = new List<InputDetail>();
            int j = 0;
           
            CultureInfo provider = new CultureInfo("es-ES");
            //NumberStyles style = NumberStyles.AllowDecimalPoint; //| NumberStyles.AllowCurrencySymbol; //| NumberStyles.AllowThousands;

            foreach (var product in selectedProducts)
            {
                var productToAdd = new InputDetail
                {
                    InputID = input.InputID,
                    ProductID = int.Parse(product),
                    Cantidad = int.Parse(selectedCants[j]),//,style, CultureInfo.CurrentCulture),
                    PNETO = decimal.Parse(selectedPNETOs[j], CultureInfo.InvariantCulture),
                    //PNETO= System.Convert.ToDecimal(selectedPNETOs[j], provider), //Fallo aquí en conversión
                    PVP = decimal.Parse(selectedPVPs[j], CultureInfo.InvariantCulture)//,style, provider),// CultureInfo.CurrentCulture) 
                };
                input.InputDetails.Add(productToAdd);
                foreach (var detalle in input.InputDetails)
                {
                    // total += detalle.TotalParcial;
                    input.TotalInput += detalle.Cantidad * (decimal)detalle.PNETO;

                }
                input.Fecha_hora = DateTime.Now;
                //var productToUpdate = _context.Products.Where(p => p.ProductID == int.Parse(product));
                var   productToUpdate = _context.Products.Where(p => p.ProductID == int.Parse(product)).Single();
                productToUpdate.Stock+= int.Parse(selectedCants[j]);

                j++;

                if (await TryUpdateModelAsync<Product>(productToUpdate,"",
                     s => s.Stock))
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            _context.Add(input);
                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                        //await _context.SaveChangesAsync();
                        //return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                   
                }
               

            }
            
            //input.Impuesto = decimal.Parse( igic);
            //input.TotalInput = SubTotal * decimal.Parse(igic);
            //input.TotalInput =  SubTotal * input.Impuesto;//¿? +subtotal
            
            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //       
            //        _context.Add(input);
            //        await _context.SaveChangesAsync();
            //        return RedirectToAction(nameof(Index));
            //    }
            //}
            //catch (DbUpdateException /* ex */)
            //{
            //    //Log the error (uncomment ex variable name and write a log.)
            //    ModelState.AddModelError("", "Unable to save changes. " +
            //        "Try again, and if the problem persists, " +
            //        "see your system administrator.");
            //}


            //ViewData["ProviderID"] = new SelectList(_context.Providers, "ProviderID", "FirstMidName", input.ProviderID);
            PopulateProvidersDropDownList(input.ProviderID);
            populateProductDropDownList();
            return View(input);
        }


        // GET: Inputs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var input = await _context.Inputs
                                .Include(i => i.InputDetails)
                                 
                                .AsNoTracking()
                                .SingleOrDefaultAsync(m => m.InputID == id);
            if (input == null)
            {
                return NotFound();
            }
            //ViewData["ProviderID"] = new SelectList(_context.Providers, "ProviderID", "FirstMidName", input.ProviderID);
            PopulateProvidersDropDownList(input.ProviderID);
            populateProductDropDownList();
            return View(input);
        }

        // POST: Inputs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        //, [Bind("Fecha_hora,Tipo_Comprobante,serie_comprobante,num_comprobante,Impuesto,TotalInput,ProviderID")] Input input)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inputToUpdate = await _context.Inputs
                                        .Include(i => i.InputDetails)
                                                          
                                    .SingleOrDefaultAsync(s => s.InputID == id);
            if (await TryUpdateModelAsync<Input>(
                inputToUpdate,
                "",
                s => s.Fecha_hora, s => s.Tipo_Comprobante, s => s.serie_comprobante, s => s.num_comprobante, s => s.Impuesto,
                s => s.TotalInput,// poner que no se pueda editar
                s => s.ProviderID))  
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
            //  PopulateDepartmentsDropDownList(inputToUpdate.ProviderID); //por definir
            
            //  ViewData["ProviderID"] = new SelectList(_context.Providers, "ProviderID", "FirstMidName", input.ProviderID);
            PopulateProvidersDropDownList(inputToUpdate.ProviderID);
            populateProductDropDownList();
            return View(inputToUpdate);
        }

        void PopulateProvidersDropDownList(object selectedProvider = null)
        {
             var providersQuery = from d in _context.Providers
                           orderby d.FirstMidName
                           select d;
              ViewBag.ProviderID = new SelectList(providersQuery.AsNoTracking(), "ProviderID", "FirstMidName", selectedProvider);


        }
          
          private void  populateProductDropDownList(object selectedProduct = null)
            {
            //var productQuery =  from p in _context.Products
            //                    orderby p.ProductName
            //                    select p;
            //ViewBag.ProductID = new SelectList(productQuery.AsNoTracking(), "ProductID", "ProductName", selectedProduct);

            var productQuery = _context.Products.Select(x => new
            {
                Id = x.ProductID,

                Name = x.ProductID.ToString()+ " " + x.ProductName

            }
                                                         ).OrderBy(x => x.Id);

            ViewBag.ProductID = new SelectList(productQuery.AsNoTracking(), "Id", "Name", selectedProduct);
        }

        // GET: Inputs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var input = await _context.Inputs
                .Include(i => i.Provider)
                .SingleOrDefaultAsync(m => m.InputID == id);
            if (input == null)
            {
                return NotFound();
            }

            return View(input);
        }

        // POST: Inputs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var input = await _context.Inputs
                                .Include(i=>i.InputDetails)
                                .SingleOrDefaultAsync(m => m.InputID == id);
            _context.Inputs.Remove(input);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InputExists(int id)
        {
            return _context.Inputs.Any(e => e.InputID == id);
        }
    }
}
