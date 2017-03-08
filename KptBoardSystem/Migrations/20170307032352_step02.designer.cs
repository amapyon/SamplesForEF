using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using KptBoardSystem;

namespace KptBoardSystem.Migrations
{
    [DbContext(typeof(KptBoardModel))]
    [Migration("20170307032352_step02")]
    partial class step02
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("KptBoardModel.KptBoard", b =>
                {
                    b.Property<int>("KptBoardId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Keep");

                    b.Property<string>("Problem");

                    b.Property<DateTime>("Time");

                    b.Property<string>("Try");

                    b.Property<int>("UserId");

                    b.HasKey("KptBoardId");

                    b.HasIndex("UserId");

                    b.ToTable("KptBards");
                });

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

            modelBuilder.Entity("KptBoardModel.KptBoard", b =>
                {
                    b.HasOne("KptBoardModel.User")
                        .WithMany("KptBoards")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
