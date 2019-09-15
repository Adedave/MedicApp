﻿// <auto-generated />
using System;
using MedicApp.Api.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MedicApp.Api.Migrations
{
    [DbContext(typeof(MedicAppDbContext))]
    [Migration("20190823090917_new")]
    partial class @new
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MedicApp.Api.Domain.Models.Disease", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DiseaseName");

                    b.HasKey("Id");

                    b.ToTable("Diseases");
                });

            modelBuilder.Entity("MedicApp.Api.Domain.Models.Enrollee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<int>("Age");

                    b.Property<string>("Gender");

                    b.Property<double>("Height");

                    b.Property<int>("HospitalId");

                    b.Property<string>("Lga");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.Property<double>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("HospitalId");

                    b.ToTable("Enrollees");
                });

            modelBuilder.Entity("MedicApp.Api.Domain.Models.EnrolleeDisease", b =>
                {
                    b.Property<int>("EnrolleeId");

                    b.Property<int>("DiseaseId");

                    b.Property<DateTime>("DateDiseaseDiagnosed");

                    b.HasKey("EnrolleeId", "DiseaseId");

                    b.HasIndex("DiseaseId");

                    b.ToTable("EnrolleeDiseases");
                });

            modelBuilder.Entity("MedicApp.Api.Domain.Models.Hospital", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<string>("Lga");

                    b.Property<int>("LocationId");

                    b.Property<string>("Name");

                    b.Property<string>("OfficePhoneNumber");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Hospitals");
                });

            modelBuilder.Entity("MedicApp.Api.Domain.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("MedicApp.Api.Domain.Models.Enrollee", b =>
                {
                    b.HasOne("MedicApp.Api.Domain.Models.Hospital", "Hospital")
                        .WithMany("Enrollees")
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MedicApp.Api.Domain.Models.EnrolleeDisease", b =>
                {
                    b.HasOne("MedicApp.Api.Domain.Models.Disease", "Disease")
                        .WithMany("Enrollees")
                        .HasForeignKey("DiseaseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MedicApp.Api.Domain.Models.Enrollee", "Enrollee")
                        .WithMany("Diseases")
                        .HasForeignKey("EnrolleeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MedicApp.Api.Domain.Models.Hospital", b =>
                {
                    b.HasOne("MedicApp.Api.Domain.Models.Location", "Location")
                        .WithMany("Hospitals")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
