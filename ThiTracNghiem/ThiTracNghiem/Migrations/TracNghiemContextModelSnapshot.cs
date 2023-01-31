﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThiTracNghiem.Data;

#nullable disable

namespace ThiTracNghiem.Migrations
{
    [DbContext(typeof(TracNghiemContext))]
    partial class TracNghiemContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ThiTracNghiem.Models.Admin", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserName");

                    b.ToTable("Admin", (string)null);
                });

            modelBuilder.Entity("ThiTracNghiem.Models.CauHoi", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("A")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("B")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("C")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("D")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DapAnDung")
                        .HasColumnType("int");

                    b.Property<int>("DeThiID")
                        .HasColumnType("int");

                    b.Property<string>("NoiDung")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("DeThiID");

                    b.ToTable("CauHoi", (string)null);
                });

            modelBuilder.Entity("ThiTracNghiem.Models.DeThi", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int>("MonThiID")
                        .HasColumnType("int");

                    b.Property<int?>("SoLuongCauHoi")
                        .HasColumnType("int");

                    b.Property<string>("TenDeThi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ThoiGian")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MonThiID");

                    b.ToTable("DeThi", (string)null);
                });

            modelBuilder.Entity("ThiTracNghiem.Models.MaThi", b =>
                {
                    b.Property<string>("Ma")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SLSD")
                        .HasColumnType("int");

                    b.HasKey("Ma");

                    b.ToTable("MaThi", (string)null);
                });

            modelBuilder.Entity("ThiTracNghiem.Models.MonThi", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<int?>("SoLuongDe")
                        .HasColumnType("int");

                    b.Property<string>("TenMonThi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("MonThi", (string)null);
                });

            modelBuilder.Entity("ThiTracNghiem.Models.CauHoi", b =>
                {
                    b.HasOne("ThiTracNghiem.Models.DeThi", "DeThi")
                        .WithMany("CauHois")
                        .HasForeignKey("DeThiID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeThi");
                });

            modelBuilder.Entity("ThiTracNghiem.Models.DeThi", b =>
                {
                    b.HasOne("ThiTracNghiem.Models.MonThi", "MonThi")
                        .WithMany("DeThis")
                        .HasForeignKey("MonThiID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MonThi");
                });

            modelBuilder.Entity("ThiTracNghiem.Models.DeThi", b =>
                {
                    b.Navigation("CauHois");
                });

            modelBuilder.Entity("ThiTracNghiem.Models.MonThi", b =>
                {
                    b.Navigation("DeThis");
                });
#pragma warning restore 612, 618
        }
    }
}
