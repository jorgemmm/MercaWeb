using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcTpv.Data
{
    using MvcTpv.Models;

    public static class ProductDatabaseInitializer
    {
        public static void Seed(MvcTpvContext context)
        {
           /* if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }*/

            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return;
            }



            var categorias = new Category[]
            {
                new Category
                {

                    CategoryName = "Cars"
                },
                new Category
                {

                    CategoryName = "Planes"
                },
                new Category
                {

                    CategoryName = "Trucks"
                },
                new Category
                {

                    CategoryName = "Boats"
                },
                new Category
                {

                    CategoryName = "Rockets"
                },
            };

            foreach (Category c in categorias)
            {
                context.Categories.Add(c);
            }
            context.SaveChanges();

            var productos = new Product[] 
            {
            new Product
            {
                
                ProductName = "Convertible Car",
                Description = "This convertible car is fast! The engine is powered by a neutrino based battery (not included)." +
                                  "Power it up and let it go!",
                ImagePath = "carconvert.png",
                //UnitPrice = 22.50,
                Stock=3,
                CategoryID = 1
            },
                new Product
                {
                  
                    ProductName = "Old-time Car",
                    Description = "There's nothing old about this toy car, except it's looks. Compatible with other old toy cars.",
                    ImagePath = "carearly.png",                    
                    Stock = 15,
                    CategoryID = 1
                },
                new Product
                {
                   
                    ProductName = "Fast Car",
                    Description = "Yes this car is fast, but it also floats in water.",
                    ImagePath = "carfast.png",
                   
                    Stock=3,
                    CategoryID = 1
                },
                new Product
                {
                  
                    ProductName = "Super Fast Car",
                    Description = "Use this super fast car to entertain guests. Lights and doors work!",
                    ImagePath = "carfaster.png",                   
                    Stock=3,
                    CategoryID = 1
                },
                new Product
                {
                   
                    ProductName = "Old Style Racer",
                    Description = "This old style racer can fly (with user assistance). Gravity controls flight duration." +
                                  "No batteries required.",
                    ImagePath = "carracer.png",
                   
                        Stock=3,
                    CategoryID = 1
                },
                new Product
                {
                  
                    ProductName = "Ace Plane",
                    Description = "Authentic airplane toy. Features realistic color and details.",
                    ImagePath = "planeace.png",
                    
                        Stock=3,
                    CategoryID = 2
                },
                new Product
                {
                  
                    ProductName = "Glider",
                    Description = "This fun glider is made from real balsa wood. Some assembly required.",
                    ImagePath = "planeglider.png",
                    //UnitPrice = 4.95,
                        Stock=3,
                    CategoryID = 2
                },
                new Product
                {
                   
                    ProductName = "Paper Plane",
                    Description = "This paper plane is like no other paper plane. Some folding required.",
                    ImagePath = "planepaper.png",
                    //UnitPrice = 2.95,
                        Stock=3,
                    CategoryID = 2
                },
                new Product
                {
                   
                    ProductName = "Propeller Plane",
                    Description = "Rubber band powered plane features two wheels.",
                    ImagePath = "planeprop.png",
                    //UnitPrice = 32.95,
                        Stock=3,
                    CategoryID = 2
                },
                new Product
                {
                   
                    ProductName = "Early Truck",
                    Description = "This toy truck has a real gas powered engine. Requires regular tune ups.",
                    ImagePath = "truckearly.png",
                    //UnitPrice = 15.00,
                        Stock=3,
                    CategoryID = 3
                },
                new Product
                {
                   
                    ProductName = "Fire Truck",
                    Description = "You will have endless fun with this one quarter sized fire truck.",
                    ImagePath = "truckfire.png",
                    //UnitPrice = 26.00,
                        Stock=3,
                    CategoryID = 3
                },
                new Product
                {
                    
                    ProductName = "Big Truck",
                    Description = "This fun toy truck can be used to tow other trucks that are not as big.",
                    ImagePath = "truckbig.png",
                    //UnitPrice = 29.00,
                        Stock=3,
                    CategoryID = 3
                },
                new Product
                {
                   
                    ProductName = "Big Ship",
                    Description = "Is it a boat or a ship. Let this floating vehicle decide by using its " +
                                  "artifically intelligent computer brain!",
                    ImagePath = "boatbig.png",
                    //UnitPrice = 95.00,
                        Stock=3,
                    CategoryID = 4
                },
                new Product
                {
                   

                    ProductName = "Paper Boat",
                    Description = "Floating fun for all! This toy boat can be assembled in seconds. Floats for minutes!" +
                                  "Some folding required.",
                    ImagePath = "boatpaper.png",
                    //UnitPrice = 4.95,
                        Stock=3,
                    CategoryID = 4
                },
                new Product
                {
                   

                    ProductName = "Sail Boat",
                    Description = "Put this fun toy sail boat in the water and let it go!",
                    ImagePath = "boatsail.png",
                    //UnitPrice = 42.95,
                        Stock=3,
                    CategoryID = 4
                },
                new Product
                {
                   

                    ProductName = "Rocket",
                    Description = "This fun rocket will travel up to a height of 200 feet.",
                    ImagePath = "rocket.png",
                    //UnitPrice = 122.95,
                        Stock=3,
                    CategoryID = 5
                }
            };

            foreach (Product p in productos)
            {
                context.Products.Add(p);
            }
            context.SaveChanges();


            var providers = new Provider[]
                    {
                new Provider {CategoryID=1, FirstMidName = "Laura",    LastName = "Cars Provider",  HighDate = DateTime.Parse("2013-09-01") },
                new Provider { CategoryID=2, FirstMidName = "Nino",     LastName = "Plane Provider", HighDate = DateTime.Parse("2005-09-01") },
                new Provider {CategoryID=3,  FirstMidName = "Norman",    LastName = "Truck Provider",  HighDate = DateTime.Parse("2013-09-01") },
                new Provider {CategoryID=4, FirstMidName = "Olivetto",     LastName = "Ship Provider", HighDate = DateTime.Parse("2005-09-01") },
                new Provider {CategoryID=5, FirstMidName = "Lolo",    LastName = "Rocket Provider",  HighDate = DateTime.Parse("2013-09-01") },
             



                    };

            foreach (Provider p in providers)
            {
                context.Providers.Add(p);
            }
            context.SaveChanges();




           var customers   = new Customer[]
                      {
                new Customer { FirstMidName = "Carson",   LastName = "Alexander",
                    HighDate = DateTime.Parse("2010-09-01") },
                new Customer { FirstMidName = "Meredith", LastName = "Alonso",
                    HighDate = DateTime.Parse("2012-09-01") },
                new Customer { FirstMidName = "Arturo",   LastName = "Anand",
                    HighDate = DateTime.Parse("2013-09-01") },
                new Customer { FirstMidName = "Gytis",    LastName = "Barzdukas",
                    HighDate = DateTime.Parse("2012-09-01") },
                 new Customer { FirstMidName = "Laura",    LastName = "Norman",
                     HighDate = DateTime.Parse("2013-09-01") },
                new Customer { FirstMidName = "Nino",     LastName = "Olivetto",
                    HighDate = DateTime.Parse("2005-09-01") }

                      };

            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();

            var salemans = new SaleMan[]
                     {
                new SaleMan { FirstMidName = "Yan",      LastName = "Li",
                    HighDate = DateTime.Parse("2012-09-01") },
                new SaleMan { FirstMidName = "Peggy",    LastName = "Justice",
                    HighDate = DateTime.Parse("2011-09-01") },
                     };

            foreach (SaleMan s in salemans)
            {
                context.SaleMans.Add(s);
            }
            context.SaveChanges();

           


            var sales = new Sale[]
                    {
                new Sale {CustomerID=1, SaleManID=1,  Tipo_Comprobante = Comprobante.Recibo,     Serie_comprobante="01R", Num_comprobante="0001" ,Fecha_Hora = DateTime.Parse("2016-09-01"),  Impuesto=0.14m,  Estado=Estado.Emitido},
                new Sale {CustomerID=2, SaleManID=1,  Tipo_Comprobante = Comprobante.Recibo,     Serie_comprobante="01R", Num_comprobante="0002" ,Fecha_Hora = DateTime.Parse("2012-10-01"),  Impuesto=0.14m, Estado=Estado.Anulada},

                new Sale {CustomerID=3, SaleManID=2,  Tipo_Comprobante = Comprobante.Factura,    Serie_comprobante="01F", Num_comprobante="0001" ,Fecha_Hora = DateTime.Parse("2014-03-01"),  Impuesto=0.14m,  Estado=Estado.Emitido },
                new Sale {CustomerID=4, SaleManID=2 , Tipo_Comprobante = Comprobante.Factura,    Serie_comprobante="01F", Num_comprobante="0002" ,Fecha_Hora = DateTime.Parse("2015-05-01") , Impuesto=0.14m,  Estado= Estado.Facturado},


                    };

            foreach (Sale s in sales)
            {
                context.Sales.Add(s);
            }
            context.SaveChanges();


            var saledetails = new SaleDetail[]
                   {
                new SaleDetail {ProductID=1, SaleID=1,   Cantidad=2, PVP= 22.50m, Descuento=2.5m},
                new SaleDetail {ProductID=2, SaleID=1,   Cantidad=2, PVP= 15.95m, Descuento=2.5m},
                new SaleDetail {ProductID=3, SaleID=1,   Cantidad=2, PVP= 32.99m, Descuento=2.5m},
                new SaleDetail {ProductID=4, SaleID=1 ,  Cantidad=2, PVP= 8.95m, Descuento=2.5m},


                new SaleDetail {ProductID=5, SaleID=2,   Cantidad=2, PVP= 32.99m, Descuento=2.5m},
                new SaleDetail {ProductID=6, SaleID=2 ,  Cantidad=2, PVP= 8.95m, Descuento=2.5m},
                   };

            foreach (SaleDetail sd in saledetails)
            {
                context.SaleDetails.Add(sd);
            }
            context.SaveChanges();

            var inputs = new Input[]
                   {
                new Input {ProviderID=1,  Tipo_Comprobante = "Albaran",     serie_comprobante="P1A", num_comprobante="0001" , Fecha_hora = DateTime.Parse("2017-09-01"), Impuesto=0.14m },
                new Input {ProviderID=2,  Tipo_Comprobante = "Albaran",     serie_comprobante="P1A", num_comprobante="0002" ,Fecha_hora = DateTime.Parse("2014-10-01"),  Impuesto=0.14m },
                new Input {ProviderID=3,  Tipo_Comprobante = "Factura",    serie_comprobante= "P1F", num_comprobante="0001" ,Fecha_hora = DateTime.Parse("2015-03-01"),  Impuesto=0.14m },

               new Input {ProviderID=4,  Tipo_Comprobante =  "Factura",    serie_comprobante= "P1F", num_comprobante="0002" ,Fecha_hora = DateTime.Parse("2016-05-01") , Impuesto=0.14m},
                   };

            foreach (Input i in inputs)
            {
                context.Inputs.Add(i);
            }
            context.SaveChanges();


            var inputdetails = new InputDetail[]
                   {
                new InputDetail { InputID=1, ProductID=1,   Cantidad=2, PNETO =11.25m, PVP= 22.50m, },
                new InputDetail {InputID=1, ProductID=2,    Cantidad=2, PNETO=7.95m, PVP= 15.95m},
                new InputDetail {InputID=1, ProductID=3,    Cantidad=2, PNETO=17.51m,PVP= 32.99m},
                new InputDetail {InputID=1 ,ProductID=4,    Cantidad=2, PNETO=7.99m, PVP= 8.95m },

                new InputDetail {InputID=2, ProductID=6,   Cantidad=2, PNETO=49.95m, PVP= 95.00m},
                new InputDetail {InputID=2 ,ProductID=7,  Cantidad=4, PNETO=5.99m, PVP= 4.95m},


                new InputDetail {InputID=3, ProductID=10,    Cantidad=2, PNETO=7.50m, PVP= 15.00m},
                new InputDetail {InputID=3 , ProductID=11,   Cantidad=2, PNETO=13.00m, PVP= 26.00m},

                new InputDetail {InputID=4, ProductID=13,    Cantidad=2, PNETO=49.95m, PVP= 4.95m},


                   };

            foreach (InputDetail id in inputdetails)
            {
                context.InputDetails.Add(id);
            }
            context.SaveChanges();

        }
    }
}
