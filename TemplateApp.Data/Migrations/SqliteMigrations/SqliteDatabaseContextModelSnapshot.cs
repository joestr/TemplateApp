﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TemplateApp.Data.Contexts;

#nullable disable

namespace TemplateApp.Data.Migrations.SqliteMigrations
{
    [DbContext(typeof(SqliteDatabaseContext))]
    partial class SqliteDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("TemplateApp.Presentation.Web.Data.Entities.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Prefix")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Salutation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Suffix")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5f4546a4-701b-4b2c-ae1e-c4a67b1e6ed9"),
                            FirstName = "Karl",
                            LastName = "Marx",
                            Prefix = "",
                            Salutation = "",
                            Suffix = ""
                        });
                });

            modelBuilder.Entity("TemplateApp.Presentation.Web.Data.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Subtitle")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = new Guid("21396dd4-d045-418a-8aea-bc5f8bae30a5"),
                            AuthorId = new Guid("5f4546a4-701b-4b2c-ae1e-c4a67b1e6ed9"),
                            Description = "",
                            Subtitle = "",
                            Title = "Manifest der Kommunistischen Partei"
                        },
                        new
                        {
                            Id = new Guid("e4219615-344c-4858-a148-f016d2c220eb"),
                            AuthorId = new Guid("5f4546a4-701b-4b2c-ae1e-c4a67b1e6ed9"),
                            Description = "",
                            Subtitle = "Kritik der politischen Ökonomie",
                            Title = "Das Kapital"
                        });
                });

            modelBuilder.Entity("TemplateApp.Presentation.Web.Data.Entities.Book", b =>
                {
                    b.HasOne("TemplateApp.Presentation.Web.Data.Entities.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("TemplateApp.Presentation.Web.Data.Entities.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
