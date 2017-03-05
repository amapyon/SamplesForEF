using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Sample01;

namespace Sample01.Migrations
{
    [DbContext(typeof(KptBoardContext))]
    [Migration("20170305061409_step02")]
    partial class step02
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("Sample01.Board", b =>
                {
                    b.Property<int>("BoardID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Keep");

                    b.Property<int>("PersonID");

                    b.HasKey("BoardID");

                    b.HasIndex("PersonID");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("Sample01.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Age");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("PersonId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Sample01.Board", b =>
                {
                    b.HasOne("Sample01.Person")
                        .WithMany("Boards")
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
