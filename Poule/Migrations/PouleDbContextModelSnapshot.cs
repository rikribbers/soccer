﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Poule.Entities;
using System;

namespace Poule.Migrations
{
    [DbContext(typeof(PouleDbContext))]
    partial class PouleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011");

            modelBuilder.Entity("Poule.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AwayTeam")
                        .IsRequired();

                    b.Property<DateTime>("Date");

                    b.Property<string>("FulltimeScore");

                    b.Property<string>("HalftimeScore");

                    b.Property<string>("HomeTeam")
                        .IsRequired();

                    b.Property<int>("Order");

                    b.Property<int>("Round");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Poule.Entities.Prediction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FulltimeScore");

                    b.Property<int>("GameId");

                    b.Property<string>("HalftimeScore");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Predictions");
                });

            modelBuilder.Entity("Poule.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailAddress")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Order");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Poule.Entities.Prediction", b =>
                {
                    b.HasOne("Poule.Entities.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Poule.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
