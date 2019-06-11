﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MvcTpv.Data;
using MvcTpv.Models;
using System;

namespace MvcTpv.Migrations
{
    [DbContext(typeof(MvcTpvContext))]
    [Migration("20171112174538_inputChangesv2")]
    partial class inputChangesv2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MvcTpv.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Description");

                    b.HasKey("CategoryID");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("MvcTpv.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Direccion");

                    b.Property<string>("Email");

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasColumnName("First Name")
                        .HasMaxLength(50);

                    b.Property<DateTime>("HighDate");

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<string>("NumDocumento");

                    b.Property<string>("Telefono");

                    b.Property<string>("TipoDocumento");

                    b.HasKey("CustomerID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("MvcTpv.Models.Input", b =>
                {
                    b.Property<int>("InputID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Fecha_hora");

                    b.Property<decimal>("Impuesto");

                    b.Property<int>("ProviderID");

                    b.Property<string>("Tipo_Comprobante")
                        .HasColumnName("Tipo Comprobante");

                    b.Property<decimal>("TotalInput")
                        .HasColumnType("money");

                    b.Property<string>("num_comprobante")
                        .HasColumnName("Numero Comprobante");

                    b.Property<string>("serie_comprobante")
                        .HasColumnName("Serie Comprobante");

                    b.HasKey("InputID");

                    b.HasIndex("ProviderID");

                    b.ToTable("Input");
                });

            modelBuilder.Entity("MvcTpv.Models.InputDetail", b =>
                {
                    b.Property<int>("InputDetailID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cantidad");

                    b.Property<int>("InputID");

                    b.Property<decimal?>("PNETO")
                        .HasColumnType("money");

                    b.Property<decimal?>("PVP")
                        .HasColumnType("money");

                    b.Property<int>("ProductID");

                    b.HasKey("InputDetailID");

                    b.HasIndex("InputID");

                    b.HasIndex("ProductID");

                    b.ToTable("InputDetail");
                });

            modelBuilder.Entity("MvcTpv.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryID");

                    b.Property<string>("Codigo");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("ProductDescription")
                        .HasMaxLength(10000);

                    b.Property<string>("ImagePath");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Stock");

                    b.HasKey("ProductID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("MvcTpv.Models.Provider", b =>
                {
                    b.Property<int>("ProviderID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CategoryID");

                    b.Property<string>("Direccion");

                    b.Property<string>("Email");

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasColumnName("First Name")
                        .HasMaxLength(50);

                    b.Property<DateTime>("HighDate");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("NumDocumento");

                    b.Property<string>("Telefono");

                    b.Property<string>("TipoDocumento");

                    b.HasKey("ProviderID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Provider");
                });

            modelBuilder.Entity("MvcTpv.Models.Sale", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerID");

                    b.Property<int?>("Estado");

                    b.Property<DateTime>("Fecha_Hora");

                    b.Property<decimal?>("Impuesto");

                    b.Property<string>("Num_comprobante");

                    b.Property<int>("SaleManID");

                    b.Property<string>("Serie_comprobante");

                    b.Property<int?>("Tipo_Comprobante");

                    b.Property<decimal?>("TotalSale")
                        .HasColumnType("money");

                    b.HasKey("ID");

                    b.HasIndex("CustomerID");

                    b.HasIndex("SaleManID");

                    b.ToTable("Sale");
                });

            modelBuilder.Entity("MvcTpv.Models.SaleDetail", b =>
                {
                    b.Property<int>("SaleDetailID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Cantidad");

                    b.Property<decimal>("Descuento")
                        .HasColumnType("money");

                    b.Property<decimal>("PVP")
                        .HasColumnType("money");

                    b.Property<int>("ProductID");

                    b.Property<int>("SaleID");

                    b.HasKey("SaleDetailID");

                    b.HasIndex("ProductID");

                    b.HasIndex("SaleID");

                    b.ToTable("SaleDetail");
                });

            modelBuilder.Entity("MvcTpv.Models.SaleMan", b =>
                {
                    b.Property<int>("SaleManID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Direccion");

                    b.Property<string>("Email");

                    b.Property<string>("FirstMidName")
                        .IsRequired()
                        .HasColumnName("First Name")
                        .HasMaxLength(50);

                    b.Property<DateTime>("HighDate");

                    b.Property<string>("LastName")
                        .HasMaxLength(50);

                    b.Property<string>("NumDocumento");

                    b.Property<string>("Telefono");

                    b.Property<string>("TipoDocumento");

                    b.HasKey("SaleManID");

                    b.ToTable("SaleMan");
                });

            modelBuilder.Entity("MvcTpv.Models.Input", b =>
                {
                    b.HasOne("MvcTpv.Models.Provider", "Provider")
                        .WithMany("Inputs")
                        .HasForeignKey("ProviderID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MvcTpv.Models.InputDetail", b =>
                {
                    b.HasOne("MvcTpv.Models.Input", "Input")
                        .WithMany("InputDetails")
                        .HasForeignKey("InputID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MvcTpv.Models.Product", "Product")
                        .WithMany("InputDetails")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MvcTpv.Models.Product", b =>
                {
                    b.HasOne("MvcTpv.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryID");
                });

            modelBuilder.Entity("MvcTpv.Models.Provider", b =>
                {
                    b.HasOne("MvcTpv.Models.Category", "Category")
                        .WithMany("Provider")
                        .HasForeignKey("CategoryID");
                });

            modelBuilder.Entity("MvcTpv.Models.Sale", b =>
                {
                    b.HasOne("MvcTpv.Models.Customer", "Customer")
                        .WithMany("Sales")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MvcTpv.Models.SaleMan", "SaleMan")
                        .WithMany("Sales")
                        .HasForeignKey("SaleManID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MvcTpv.Models.SaleDetail", b =>
                {
                    b.HasOne("MvcTpv.Models.Product", "Product")
                        .WithMany("SaleDetails")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MvcTpv.Models.Sale", "Sale")
                        .WithMany("SaleDetails")
                        .HasForeignKey("SaleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}