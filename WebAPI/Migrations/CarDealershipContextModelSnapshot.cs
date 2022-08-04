﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;
using Repository;

#nullable disable

namespace WebAPI.Migrations
{
    [DbContext(typeof(CarDealershipContext))]
    partial class CarDealershipContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebAPI.Models.Car", b =>
                {
                    b.Property<Guid>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("DealerShopId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfOwners")
                        .HasColumnType("int");

                    b.Property<int>("ProductionYear")
                        .HasColumnType("int");

                    b.HasKey("CarId");

                    b.HasIndex("DealerShopId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("WebAPI.Models.DealerShop", b =>
                {
                    b.Property<Guid>("DealerShopId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Point>("Location")
                        .IsRequired()
                        .HasColumnType("geography");

                    b.Property<int>("OrdinalNumber")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("DealerShopId");

                    b.ToTable("DealerShops");
                });

            modelBuilder.Entity("WebAPI.Models.PhotoForCar", b =>
                {
                    b.Property<Guid>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Bytes")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("CarId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Size")
                        .HasPrecision(30, 10)
                        .HasColumnType("decimal(30,10)");

                    b.HasKey("PhotoId");

                    b.HasIndex("CarId");

                    b.ToTable("PhotosForCar");
                });

            modelBuilder.Entity("WebAPI.Models.PhotoForDealerShop", b =>
                {
                    b.Property<Guid>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("Bytes")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("DealerShopId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Size")
                        .HasPrecision(30, 10)
                        .HasColumnType("decimal(30,10)");

                    b.HasKey("PhotoId");

                    b.HasIndex("DealerShopId");

                    b.ToTable("PhotosForDealershop");
                });

            modelBuilder.Entity("WebAPI.Models.Car", b =>
                {
                    b.HasOne("WebAPI.Models.DealerShop", "DealerShop")
                        .WithMany("Cars")
                        .HasForeignKey("DealerShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DealerShop");
                });

            modelBuilder.Entity("WebAPI.Models.PhotoForCar", b =>
                {
                    b.HasOne("WebAPI.Models.Car", "Car")
                        .WithMany("Photos")
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");
                });

            modelBuilder.Entity("WebAPI.Models.PhotoForDealerShop", b =>
                {
                    b.HasOne("WebAPI.Models.DealerShop", "DealerShop")
                        .WithMany("Photos")
                        .HasForeignKey("DealerShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DealerShop");
                });

            modelBuilder.Entity("WebAPI.Models.Car", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("WebAPI.Models.DealerShop", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Photos");
                });
#pragma warning restore 612, 618
        }
    }
}
