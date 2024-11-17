﻿// <auto-generated />
using System;
using FiStockBackend.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FiStockBackend.Migrations
{
    [DbContext(typeof(StockTrackingDbContext))]
    [Migration("20241117130946_FiMig")]
    partial class FiMig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FiStockBackend.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("FiStockBackend.Models.Customer", b =>
                {
                    b.Property<int>("CustomerCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CustomerCode"));

                    b.Property<string>("ContactInformation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("CustomerName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CustomerCode");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("FiStockBackend.Models.Product", b =>
                {
                    b.Property<int>("ProductCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ProductCode"));

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<int>("MinimumStockLevel")
                        .HasColumnType("integer");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("numeric");

                    b.Property<string>("SKU")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("SalePrice")
                        .HasColumnType("numeric");

                    b.Property<int>("SupplierCode")
                        .HasColumnType("integer");

                    b.Property<int>("SupplierId")
                        .HasColumnType("integer");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ProductCode");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SupplierCode");

                    b.HasIndex("SupplierId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("FiStockBackend.Models.StockMovement", b =>
                {
                    b.Property<int>("StockMovementId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StockMovementId"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("ProductCode")
                        .HasColumnType("integer");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric");

                    b.Property<int?>("SourceDestinationId")
                        .HasColumnType("integer");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric");

                    b.HasKey("StockMovementId");

                    b.HasIndex("ProductCode");

                    b.HasIndex("SourceDestinationId");

                    b.ToTable("StockMovements");
                });

            modelBuilder.Entity("FiStockBackend.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SupplierCode"));

                    b.Property<string>("ContactInformation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SupplierName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("SupplierCode");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("FiStockBackend.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PermissionLevel")
                        .HasColumnType("integer");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FiStockBackend.Models.Warehouse", b =>
                {
                    b.Property<int>("WarehouseCode")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("WarehouseCode"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ResponsiblePerson")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WarehouseName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("WarehouseCode");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("FiStockBackend.Models.Product", b =>
                {
                    b.HasOne("FiStockBackend.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FiStockBackend.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FiStockBackend.Models.Supplier", null)
                        .WithMany()
                        .HasForeignKey("SupplierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("FiStockBackend.Models.StockMovement", b =>
                {
                    b.HasOne("FiStockBackend.Models.Product", "Product")
                        .WithMany("StockMovements")
                        .HasForeignKey("ProductCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FiStockBackend.Models.Warehouse", null)
                        .WithMany()
                        .HasForeignKey("SourceDestinationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FiStockBackend.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("FiStockBackend.Models.Product", b =>
                {
                    b.Navigation("StockMovements");
                });
#pragma warning restore 612, 618
        }
    }
}
