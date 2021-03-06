﻿// <auto-generated />
using BusTicketSystem.Data;
using BusTicketSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BusTicketSystem.Data.Migrations
{
    [DbContext(typeof(BusTicketContext))]
    [Migration("20171130161414_InitialandSeed")]
    partial class InitialandSeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusTicketSystem.Models.BankAccount", b =>
                {
                    b.Property<int>("BankAccountId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccountNumber")
                        .IsRequired();

                    b.Property<decimal>("Balance")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0.0m);

                    b.HasKey("BankAccountId");

                    b.HasIndex("AccountNumber")
                        .IsUnique();

                    b.ToTable("BankAccounts");
                });

            modelBuilder.Entity("BusTicketSystem.Models.BusCompany", b =>
                {
                    b.Property<int>("BusCompanyId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Nationality");

                    b.Property<decimal?>("Rating");

                    b.HasKey("BusCompanyId");

                    b.ToTable("BusCompanies");
                });

            modelBuilder.Entity("BusTicketSystem.Models.BusStation", b =>
                {
                    b.Property<int>("BusStationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("TownId");

                    b.HasKey("BusStationId");

                    b.HasIndex("TownId");

                    b.ToTable("BusStations");
                });

            modelBuilder.Entity("BusTicketSystem.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("Birthday");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<int>("Gender")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(2);

                    b.Property<int>("HomeTownId");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.HasKey("CustomerId");

                    b.HasIndex("HomeTownId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BusTicketSystem.Models.CustomerBankAcc", b =>
                {
                    b.Property<int>("CustomerId");

                    b.Property<int>("BankAccountId");

                    b.HasKey("CustomerId", "BankAccountId");

                    b.HasIndex("BankAccountId")
                        .IsUnique();

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("CustomersBankAccounts");
                });

            modelBuilder.Entity("BusTicketSystem.Models.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BusCompanyId");

                    b.Property<string>("Content");

                    b.Property<int>("CustomerId");

                    b.Property<decimal>("Grade");

                    b.Property<DateTime>("PublishedOn")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETDATE()");

                    b.HasKey("ReviewId");

                    b.HasIndex("BusCompanyId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("BusTicketSystem.Models.Ticket", b =>
                {
                    b.Property<int>("TicketId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerId");

                    b.Property<decimal>("Price");

                    b.Property<int>("Seat");

                    b.Property<int>("TripId");

                    b.HasKey("TicketId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("TripId");

                    b.HasIndex("Seat", "TripId")
                        .IsUnique();

                    b.ToTable("Tickets");
                });

            modelBuilder.Entity("BusTicketSystem.Models.Town", b =>
                {
                    b.Property<int>("TownId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("TownId");

                    b.ToTable("Towns");
                });

            modelBuilder.Entity("BusTicketSystem.Models.Trip", b =>
                {
                    b.Property<int>("TripId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ArrivalTime");

                    b.Property<int>("BusCompanyId");

                    b.Property<DateTime>("DepartureTime");

                    b.Property<int>("DestinationBusStationId");

                    b.Property<int>("OriginBusStationId");

                    b.Property<int>("Status");

                    b.HasKey("TripId");

                    b.HasIndex("BusCompanyId");

                    b.HasIndex("DestinationBusStationId");

                    b.HasIndex("OriginBusStationId");

                    b.ToTable("Trips");
                });

            modelBuilder.Entity("BusTicketSystem.Models.BusStation", b =>
                {
                    b.HasOne("BusTicketSystem.Models.Town", "Town")
                        .WithMany("BusStations")
                        .HasForeignKey("TownId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusTicketSystem.Models.Customer", b =>
                {
                    b.HasOne("BusTicketSystem.Models.Town", "HomeTown")
                        .WithMany("Customers")
                        .HasForeignKey("HomeTownId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusTicketSystem.Models.CustomerBankAcc", b =>
                {
                    b.HasOne("BusTicketSystem.Models.BankAccount", "BankAccount")
                        .WithOne("BankAccCustomer")
                        .HasForeignKey("BusTicketSystem.Models.CustomerBankAcc", "BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BusTicketSystem.Models.Customer", "Customer")
                        .WithOne("CustomerBankAccount")
                        .HasForeignKey("BusTicketSystem.Models.CustomerBankAcc", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusTicketSystem.Models.Review", b =>
                {
                    b.HasOne("BusTicketSystem.Models.BusCompany", "BusCompany")
                        .WithMany("Reviews")
                        .HasForeignKey("BusCompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BusTicketSystem.Models.Customer", "Customer")
                        .WithMany("Reviews")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusTicketSystem.Models.Ticket", b =>
                {
                    b.HasOne("BusTicketSystem.Models.Customer", "Customer")
                        .WithMany("Tickets")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BusTicketSystem.Models.Trip", "Trip")
                        .WithMany("Tickets")
                        .HasForeignKey("TripId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusTicketSystem.Models.Trip", b =>
                {
                    b.HasOne("BusTicketSystem.Models.BusCompany", "BusCompany")
                        .WithMany("Trips")
                        .HasForeignKey("BusCompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BusTicketSystem.Models.BusStation", "DestinationBusStation")
                        .WithMany("TripsArrivedAt")
                        .HasForeignKey("DestinationBusStationId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BusTicketSystem.Models.BusStation", "OriginBusStation")
                        .WithMany("TripsGoFrom")
                        .HasForeignKey("OriginBusStationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
