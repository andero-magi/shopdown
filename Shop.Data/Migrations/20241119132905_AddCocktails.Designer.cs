﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.Data;

#nullable disable

namespace Shop.Data.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20241119132905_AddCocktails")]
    partial class AddCocktails
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Shop.Core.Domain.FileToApi", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ExistingFilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SpaceshipId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Shop.Core.Domain.FileToDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("ImageTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("RealEstateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("DbFiles");
                });

            modelBuilder.Entity("Shop.Core.Domain.RealEstate", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BuildingType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<double>("Size")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("RealEstate");
                });

            modelBuilder.Entity("Shop.Core.Domain.Spaceship", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BuildDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Crew")
                        .HasColumnType("int");

                    b.Property<int>("EnginePower")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpaceshipModel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Typename")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Spaceships");
                });

            modelBuilder.Entity("Shop.Core.Dto.Cocktails.Drink", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasAnnotation("Relational:JsonPropertyName", "idDrink");

                    b.Property<string>("dateModified")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "dateModified");

                    b.Property<string>("strAlcoholic")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strAlcoholic");

                    b.Property<string>("strCategory")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strCategory");

                    b.Property<string>("strCreativeCommonsConfirmed")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strCreativeCommonsConfirmed");

                    b.Property<string>("strDrink")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strDrink");

                    b.Property<string>("strDrinkAlternate")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strDrinkAlternate");

                    b.Property<string>("strDrinkThumb")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strDrinkThumb");

                    b.Property<string>("strGlass")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strGlass");

                    b.Property<string>("strIBA")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strIBA");

                    b.Property<string>("strImageAttribution")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strImageAttribution");

                    b.Property<string>("strImageSource")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strImageSource");

                    b.Property<string>("strIngredient1")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strIngredient1");

                    b.Property<string>("strIngredient10")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strIngredient10");

                    b.Property<string>("strIngredient11")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strIngredient11");

                    b.Property<string>("strIngredient12")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strIngredient12");

                    b.Property<string>("strIngredient13")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strIngredient13");

                    b.Property<string>("strIngredient14")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strIngredient14");

                    b.Property<string>("strIngredient15")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strIngredient15");

                    b.Property<string>("strIngredient2")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strIngredient2");

                    b.Property<string>("strIngredient3")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strIngredient3");

                    b.Property<string>("strIngredient4")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strIngredient4");

                    b.Property<string>("strIngredient5")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strIngredient5");

                    b.Property<string>("strIngredient6")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strIngredient6");

                    b.Property<string>("strIngredient7")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strIngredient7");

                    b.Property<string>("strIngredient8")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strIngredient8");

                    b.Property<string>("strIngredient9")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strIngredient9");

                    b.Property<string>("strInstructions")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strInstructions");

                    b.Property<string>("strInstructionsDE")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strInstructionsDE");

                    b.Property<string>("strInstructionsES")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strInstructionsES");

                    b.Property<string>("strInstructionsFR")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strInstructionsFR");

                    b.Property<string>("strInstructionsIT")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strInstructionsIT");

                    b.Property<string>("strInstructionsZHHANS")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strInstructionsZH-HANS");

                    b.Property<string>("strInstructionsZHHANT")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strInstructionsZH-HANT");

                    b.Property<string>("strMeasure1")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strMeasure1");

                    b.Property<string>("strMeasure10")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strMeasure10");

                    b.Property<string>("strMeasure11")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strMeasure11");

                    b.Property<string>("strMeasure12")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strMeasure12");

                    b.Property<string>("strMeasure13")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strMeasure13");

                    b.Property<string>("strMeasure14")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strMeasure14");

                    b.Property<string>("strMeasure15")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strMeasure15");

                    b.Property<string>("strMeasure2")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strMeasure2");

                    b.Property<string>("strMeasure3")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strMeasure3");

                    b.Property<string>("strMeasure4")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strMeasure4");

                    b.Property<string>("strMeasure5")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strMeasure5");

                    b.Property<string>("strMeasure6")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strMeasure6");

                    b.Property<string>("strMeasure7")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strMeasure7");

                    b.Property<string>("strMeasure8")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strMeasure8");

                    b.Property<string>("strMeasure9")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strMeasure9");

                    b.Property<string>("strTags")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strTags");

                    b.Property<string>("strVideo")
                        .HasColumnType("nvarchar(max)")
                        .HasAnnotation("Relational:JsonPropertyName", "strVideo");

                    b.HasKey("Id");

                    b.ToTable("Drinks");
                });
#pragma warning restore 612, 618
        }
    }
}
