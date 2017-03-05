using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Sample01;

namespace Sample01.Migrations
{
    [DbContext(typeof(KptBoardContext))]
    [Migration("20170305061252_step01")]
    partial class step01
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

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
        }
    }
}
