﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OnlineShop.Persistence;

#nullable disable

namespace OnlineShop.Persistence.Migrations
{
    [DbContext(typeof(OnlineStoreDbContext))]
    [Migration("20250209221310_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OnlineShop.Domain.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("description");

                    b.Property<int>("IdProductCategory")
                        .HasColumnType("integer")
                        .HasColumnName("product_category_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("numeric(10,2)")
                        .HasColumnName("price");

                    b.HasKey("Id")
                        .HasName("pk_products");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("ix_products_id");

                    b.HasIndex("IdProductCategory")
                        .HasDatabaseName("ix_products_product_category_id");

                    b.ToTable("products", (string)null);
                });

            modelBuilder.Entity("OnlineShop.Domain.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("character varying(1024)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pk_product_categories");

                    b.HasIndex("Id")
                        .IsUnique()
                        .HasDatabaseName("ix_product_categories_id");

                    b.ToTable("product_categories", (string)null);
                });

            modelBuilder.Entity("OnlineShop.Domain.Product", b =>
                {
                    b.HasOne("OnlineShop.Domain.ProductCategory", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("IdProductCategory")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_products_product_categories_product_category_id");

                    b.Navigation("ProductCategory");
                });
#pragma warning restore 612, 618
        }
    }
}
