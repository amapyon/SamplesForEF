using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using KptBoardSystem;

namespace KptBoardSystem.Migrations
{
    [DbContext(typeof(KptBoardModel))]
    [Migration("20170307010943_step01")]
    partial class step01
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("KptBoardModel.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Age");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });
        }
    }
}
