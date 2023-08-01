﻿// <auto-generated />
using System;
using MedicineScheduler.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MedicineScheduler.DataAccessLayer.Migrations
{
    [DbContext(typeof(EFContext))]
    [Migration("20230801184933_AddEntityConfigurations")]
    partial class AddEntityConfigurations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.9");

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.ActivePatientLocation", b =>
                {
                    b.Property<int>("ActivePatientLocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("LocationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PatientId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SetTime")
                        .HasColumnType("date");

                    b.HasKey("ActivePatientLocationId");

                    b.HasIndex("LocationId");

                    b.HasIndex("PatientId");

                    b.ToTable("ActivePatientLocations");
                });

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.DosageForm", b =>
                {
                    b.Property<int>("DosageFormId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<double>("Value")
                        .HasColumnType("REAL");

                    b.HasKey("DosageFormId");

                    b.ToTable("DosageForms");
                });

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LocationId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.Medicine", b =>
                {
                    b.Property<int>("MedicineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("MedicineId");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DosageFormId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("date");

                    b.Property<int>("LocationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MedicineId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("date");

                    b.Property<double>("PricePerPackage")
                        .HasPrecision(9, 2)
                        .HasColumnType("REAL");

                    b.Property<double>("PriceTotal")
                        .HasPrecision(9, 2)
                        .HasColumnType("REAL");

                    b.Property<DateTime>("ProduceDate")
                        .HasColumnType("date");

                    b.Property<int>("QuantityPackages")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuantityPerPackage")
                        .HasColumnType("INTEGER");

                    b.HasKey("OrderId");

                    b.HasIndex("DosageFormId");

                    b.HasIndex("LocationId");

                    b.HasIndex("MedicineId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.OrderRemains", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastCheck")
                        .HasColumnType("date");

                    b.Property<double>("Quantity")
                        .HasColumnType("REAL");

                    b.HasKey("OrderId");

                    b.ToTable("OrdersRemains");
                });

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.Patient", b =>
                {
                    b.Property<int>("PatientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PatientId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.Prescription", b =>
                {
                    b.Property<int>("PrescriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("AmountMedication")
                        .HasColumnType("REAL");

                    b.Property<int>("DosageFormId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<int>("MedicationsPerDay")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MedicineId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<int>("PatientId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PrescriptionDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("PrescriptionId");

                    b.HasIndex("DosageFormId");

                    b.HasIndex("MedicineId");

                    b.HasIndex("PatientId");

                    b.ToTable("Prescriptions");
                });

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.Tag", b =>
                {
                    b.Property<string>("TagId")
                        .HasMaxLength(25)
                        .HasColumnType("TEXT");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("MedicineTag", b =>
                {
                    b.Property<int>("MedicinesMedicineId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TagsTagId")
                        .HasColumnType("TEXT");

                    b.HasKey("MedicinesMedicineId", "TagsTagId");

                    b.HasIndex("TagsTagId");

                    b.ToTable("MedicineTag");
                });

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.ActivePatientLocation", b =>
                {
                    b.HasOne("MedicineScheduler.DataAccessLayer.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicineScheduler.DataAccessLayer.Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.Order", b =>
                {
                    b.HasOne("MedicineScheduler.DataAccessLayer.Entities.DosageForm", "DosageForm")
                        .WithMany()
                        .HasForeignKey("DosageFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicineScheduler.DataAccessLayer.Entities.Location", "Location")
                        .WithMany("Orders")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicineScheduler.DataAccessLayer.Entities.Medicine", "Medicine")
                        .WithMany("Orders")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DosageForm");

                    b.Navigation("Location");

                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.OrderRemains", b =>
                {
                    b.HasOne("MedicineScheduler.DataAccessLayer.Entities.Order", "Order")
                        .WithOne("Remains")
                        .HasForeignKey("MedicineScheduler.DataAccessLayer.Entities.OrderRemains", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.Prescription", b =>
                {
                    b.HasOne("MedicineScheduler.DataAccessLayer.Entities.DosageForm", "DosageForm")
                        .WithMany()
                        .HasForeignKey("DosageFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicineScheduler.DataAccessLayer.Entities.Medicine", "Medicine")
                        .WithMany("Prescriptions")
                        .HasForeignKey("MedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicineScheduler.DataAccessLayer.Entities.Patient", "Patient")
                        .WithMany("Prescriptions")
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DosageForm");

                    b.Navigation("Medicine");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("MedicineTag", b =>
                {
                    b.HasOne("MedicineScheduler.DataAccessLayer.Entities.Medicine", null)
                        .WithMany()
                        .HasForeignKey("MedicinesMedicineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MedicineScheduler.DataAccessLayer.Entities.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.Location", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.Medicine", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Prescriptions");
                });

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.Order", b =>
                {
                    b.Navigation("Remains")
                        .IsRequired();
                });

            modelBuilder.Entity("MedicineScheduler.DataAccessLayer.Entities.Patient", b =>
                {
                    b.Navigation("Prescriptions");
                });
#pragma warning restore 612, 618
        }
    }
}
