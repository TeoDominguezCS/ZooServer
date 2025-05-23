﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using zooModel;

#nullable disable

namespace zooModel.Migrations
{
    [DbContext(typeof(ProjectSourceContext))]
    [Migration("20250328015253_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("zooModel.Animal", b =>
                {
                    b.Property<int>("AnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("animal_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnimalId"));

                    b.Property<int>("AdultNum")
                        .HasColumnType("int")
                        .HasColumnName("adult_num");

                    b.Property<int>("ChildNum")
                        .HasColumnType("int")
                        .HasColumnName("child_num");

                    b.Property<bool>("Endangered")
                        .HasColumnType("bit")
                        .HasColumnName("endangered");

                    b.Property<int>("FemaleNum")
                        .HasColumnType("int")
                        .HasColumnName("female_num");

                    b.Property<int>("HabitatId")
                        .HasColumnType("int")
                        .HasColumnName("habitat_id");

                    b.Property<int>("MaleNum")
                        .HasColumnType("int")
                        .HasColumnName("male_num");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<int>("Population")
                        .HasColumnType("int")
                        .HasColumnName("population");

                    b.HasKey("AnimalId");

                    b.HasIndex("HabitatId");

                    b.ToTable("animals");
                });

            modelBuilder.Entity("zooModel.Habitat", b =>
                {
                    b.Property<int>("HabitatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("habitat_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HabitatId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("Population")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("population");

                    b.Property<int>("ZooId")
                        .HasColumnType("int")
                        .HasColumnName("zoo_id");

                    b.HasKey("HabitatId");

                    b.HasIndex("ZooId");

                    b.ToTable("habitats");
                });

            modelBuilder.Entity("zooModel.Zoo", b =>
                {
                    b.Property<int>("ZooId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("zoo_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ZooId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("address");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)")
                        .HasColumnName("state");

                    b.HasKey("ZooId");

                    b.ToTable("zoos");
                });

            modelBuilder.Entity("zooModel.Animal", b =>
                {
                    b.HasOne("zooModel.Habitat", "Habitat")
                        .WithMany("Animals")
                        .HasForeignKey("HabitatId")
                        .IsRequired()
                        .HasConstraintName("FK_animals_habitats");

                    b.Navigation("Habitat");
                });

            modelBuilder.Entity("zooModel.Habitat", b =>
                {
                    b.HasOne("zooModel.Zoo", "Zoo")
                        .WithMany("Habitats")
                        .HasForeignKey("ZooId")
                        .IsRequired()
                        .HasConstraintName("FK_habitats_zoos");

                    b.Navigation("Zoo");
                });

            modelBuilder.Entity("zooModel.Habitat", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("zooModel.Zoo", b =>
                {
                    b.Navigation("Habitats");
                });
#pragma warning restore 612, 618
        }
    }
}
