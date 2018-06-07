﻿// <auto-generated />
using DiabetWeb.Data;
using DiabetWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DiabetWeb.Migrations
{
    [DbContext(typeof(DiabetWebDbContext))]
    partial class DiabetWebDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DiabetWeb.Models.FoodItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Attribute");

                    b.Property<double>("Carbohydrates");

                    b.Property<int>("Category");

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<double?>("EnergyKC");

                    b.Property<double?>("EnergyKJ");

                    b.Property<double>("Fat");

                    b.Property<int?>("Favorites");

                    b.Property<int>("GlycemicIndex");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<double>("Protein");

                    b.HasKey("Id");

                    b.ToTable("FoodItems");
                });

            modelBuilder.Entity("DiabetWeb.Models.MealItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FoodItemId");

                    b.Property<int?>("MemberItemId");

                    b.Property<int>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("FoodItemId");

                    b.HasIndex("MemberItemId");

                    b.ToTable("MealItems");
                });

            modelBuilder.Entity("DiabetWeb.Models.MemberItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Dose");

                    b.Property<double?>("EnergyKC");

                    b.Property<double?>("EnergyKJ");

                    b.Property<double>("F1");

                    b.Property<double>("F2");

                    b.Property<double>("F3");

                    b.Property<double>("K1");

                    b.Property<double>("K2");

                    b.Property<double>("K3");

                    b.Property<string>("MemberLogin")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Name");

                    b.Property<double?>("TotalCarbohydrates");

                    b.Property<double?>("TotalFat");

                    b.Property<double?>("TotalProtein");

                    b.HasKey("Id");

                    b.ToTable("MemberItems");
                });

            modelBuilder.Entity("DiabetWeb.Models.MealItem", b =>
                {
                    b.HasOne("DiabetWeb.Models.FoodItem", "FoodItem")
                        .WithMany()
                        .HasForeignKey("FoodItemId");

                    b.HasOne("DiabetWeb.Models.MemberItem", "MemberItem")
                        .WithMany()
                        .HasForeignKey("MemberItemId");
                });
#pragma warning restore 612, 618
        }
    }
}