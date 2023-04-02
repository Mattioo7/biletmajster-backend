﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using biletmajster_backend.Database;

#nullable disable

namespace biletmajster_backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230402122041_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("biletmajster_backend.Database.Entities.Category", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("ModelEventId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ModelEventId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("biletmajster_backend.Database.Entities.ModelEvent", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("EndTime")
                        .HasColumnType("bigint");

                    b.Property<long>("FreePlace")
                        .HasColumnType("bigint");

                    b.Property<string>("Latitude")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Longitude")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("MaxPlace")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("OrganizerId")
                        .HasColumnType("bigint");

                    b.Property<string>("PlaceSchema")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("StartTime")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerId");

                    b.ToTable("ModelEvents");
                });

            modelBuilder.Entity("biletmajster_backend.Database.Entities.Organizer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Organizers");
                });

            modelBuilder.Entity("biletmajster_backend.Database.Entities.Place", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<bool>("Free")
                        .HasColumnType("boolean");

                    b.Property<long?>("ModelEventId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ModelEventId");

                    b.ToTable("Place");
                });

            modelBuilder.Entity("biletmajster_backend.Database.Entities.Reservation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("EventId")
                        .HasColumnType("bigint");

                    b.Property<long>("PlaceId")
                        .HasColumnType("bigint");

                    b.Property<string>("ReservationToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("biletmajster_backend.Database.Entities.Category", b =>
                {
                    b.HasOne("biletmajster_backend.Database.Entities.ModelEvent", null)
                        .WithMany("Categories")
                        .HasForeignKey("ModelEventId");
                });

            modelBuilder.Entity("biletmajster_backend.Database.Entities.ModelEvent", b =>
                {
                    b.HasOne("biletmajster_backend.Database.Entities.Organizer", null)
                        .WithMany("Events")
                        .HasForeignKey("OrganizerId");
                });

            modelBuilder.Entity("biletmajster_backend.Database.Entities.Place", b =>
                {
                    b.HasOne("biletmajster_backend.Database.Entities.ModelEvent", null)
                        .WithMany("Places")
                        .HasForeignKey("ModelEventId");
                });

            modelBuilder.Entity("biletmajster_backend.Database.Entities.ModelEvent", b =>
                {
                    b.Navigation("Categories");

                    b.Navigation("Places");
                });

            modelBuilder.Entity("biletmajster_backend.Database.Entities.Organizer", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}