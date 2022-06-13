﻿// <auto-generated />
using System;
using EF_FluentAPI.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EF_FluentAPI.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20220613172408_ProdCart")]
    partial class ProdCart
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("EF_FluentAPI.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomerId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Cart", (string)null);
                });

            modelBuilder.Entity("EF_FluentAPI.Models.Credential", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Passhash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("EF_FluentAPI.Models.Customer", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("EF_FluentAPI.Models.Intermediate_Entities.ProductCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CartId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProsuctCart", (string)null);
                });

            modelBuilder.Entity("EF_FluentAPI.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("EF_FluentAPI.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("ProductOrder", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("ProductOrder", (string)null);
                });

            modelBuilder.Entity("EF_FluentAPI.Models.Cart", b =>
                {
                    b.HasOne("EF_FluentAPI.Models.Customer", "Customer")
                        .WithOne("Cart")
                        .HasForeignKey("EF_FluentAPI.Models.Cart", "CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("EF_FluentAPI.Models.Credential", b =>
                {
                    b.HasOne("EF_FluentAPI.Models.Customer", "Customer")
                        .WithOne("Credential")
                        .HasForeignKey("EF_FluentAPI.Models.Credential", "CustomerId");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("EF_FluentAPI.Models.Intermediate_Entities.ProductCart", b =>
                {
                    b.HasOne("EF_FluentAPI.Models.Cart", "Cart")
                        .WithMany("ProductCarts")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EF_FluentAPI.Models.Product", "Product")
                        .WithMany("ProductCarts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EF_FluentAPI.Models.Order", b =>
                {
                    b.HasOne("EF_FluentAPI.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ProductOrder", b =>
                {
                    b.HasOne("EF_FluentAPI.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EF_FluentAPI.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EF_FluentAPI.Models.Cart", b =>
                {
                    b.Navigation("ProductCarts");
                });

            modelBuilder.Entity("EF_FluentAPI.Models.Customer", b =>
                {
                    b.Navigation("Cart");

                    b.Navigation("Credential");

                    b.Navigation("Orders");
                });

            modelBuilder.Entity("EF_FluentAPI.Models.Product", b =>
                {
                    b.Navigation("ProductCarts");
                });
#pragma warning restore 612, 618
        }
    }
}
