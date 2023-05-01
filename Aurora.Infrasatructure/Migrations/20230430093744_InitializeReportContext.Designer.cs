﻿// <auto-generated />
using System;
using Aurora.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Aurora.Infrastructure.Migrations
{
    [DbContext(typeof(ReportDbContext))]
    [Migration("20230430093744_InitializeReportContext")]
    partial class InitializeReportContext
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Aurora.Interfaces.Models.AuroraUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AspNetUsers", null, t =>
                        {
                            t.ExcludeFromMigrations();
                        });
                });

            modelBuilder.Entity("Aurora.Interfaces.Models.Reporting.Agency", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Agency");
                });

            modelBuilder.Entity("Aurora.Interfaces.Models.Reporting.IncidentType", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AgencyId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("CollectLocation")
                        .HasColumnType("bit");

                    b.Property<bool>("CollectPerson")
                        .HasColumnType("bit");

                    b.Property<bool>("CollectTime")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("RequiresTime")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AgencyId");

                    b.ToTable("IncidentType");
                });

            modelBuilder.Entity("Aurora.Interfaces.Models.Reporting.Report", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AgencyId")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("AuroraUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("IncidentTypeId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<double?>("Miles")
                        .HasColumnType("float");

                    b.Property<string>("Narrative")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan?>("Time")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("AgencyId");

                    b.HasIndex("AuroraUserId");

                    b.HasIndex("IncidentTypeId");

                    b.ToTable("Reports");
                });

            modelBuilder.Entity("Aurora.Interfaces.Models.Reporting.ReportPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportPerson");
                });

            modelBuilder.Entity("Aurora.Interfaces.Models.Reporting.IncidentType", b =>
                {
                    b.HasOne("Aurora.Interfaces.Models.Reporting.Agency", null)
                        .WithMany("IncidentTypes")
                        .HasForeignKey("AgencyId");
                });

            modelBuilder.Entity("Aurora.Interfaces.Models.Reporting.Report", b =>
                {
                    b.HasOne("Aurora.Interfaces.Models.Reporting.Agency", "Agency")
                        .WithMany()
                        .HasForeignKey("AgencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aurora.Interfaces.Models.AuroraUser", "User")
                        .WithMany()
                        .HasForeignKey("AuroraUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Aurora.Interfaces.Models.Reporting.IncidentType", "IncidentType")
                        .WithMany()
                        .HasForeignKey("IncidentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Aurora.Interfaces.Models.Reporting.Location", "Location", b1 =>
                        {
                            b1.Property<string>("ReportId")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<string>("Address")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("City")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<int>("LocationType")
                                .HasColumnType("int");

                            b1.Property<string>("State")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("Zip")
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.HasKey("ReportId");

                            b1.ToTable("Reports");

                            b1.WithOwner()
                                .HasForeignKey("ReportId");
                        });

                    b.Navigation("Agency");

                    b.Navigation("IncidentType");

                    b.Navigation("Location")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Aurora.Interfaces.Models.Reporting.ReportPerson", b =>
                {
                    b.HasOne("Aurora.Interfaces.Models.Reporting.Report", null)
                        .WithMany("People")
                        .HasForeignKey("ReportId");

                    b.OwnsOne("Aurora.Interfaces.Models.Reporting.PhoneNumber", "PhoneNumber", b1 =>
                        {
                            b1.Property<int>("ReportPersonId")
                                .HasColumnType("int");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("nvarchar(20)");

                            b1.Property<int>("Type")
                                .HasColumnType("int");

                            b1.HasKey("ReportPersonId");

                            b1.ToTable("ReportPerson");

                            b1.WithOwner()
                                .HasForeignKey("ReportPersonId");
                        });

                    b.OwnsOne("Aurora.Interfaces.Models.Reporting.Location", "Location", b1 =>
                        {
                            b1.Property<int>("ReportPersonId")
                                .HasColumnType("int");

                            b1.Property<string>("Address")
                                .HasMaxLength(100)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("City")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<int>("LocationType")
                                .HasColumnType("int");

                            b1.Property<string>("State")
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("Zip")
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.HasKey("ReportPersonId");

                            b1.ToTable("ReportPerson");

                            b1.WithOwner()
                                .HasForeignKey("ReportPersonId");
                        });

                    b.Navigation("Location")
                        .IsRequired();

                    b.Navigation("PhoneNumber");
                });

            modelBuilder.Entity("Aurora.Interfaces.Models.Reporting.Agency", b =>
                {
                    b.Navigation("IncidentTypes");
                });

            modelBuilder.Entity("Aurora.Interfaces.Models.Reporting.Report", b =>
                {
                    b.Navigation("People");
                });
#pragma warning restore 612, 618
        }
    }
}
