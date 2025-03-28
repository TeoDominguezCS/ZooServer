using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace zooModel;

public partial class ProjectSourceContext : DbContext
{
    public ProjectSourceContext()
    {
    }

    public ProjectSourceContext(DbContextOptions<ProjectSourceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Animal> Animals { get; set; }

    public virtual DbSet<Habitat> Habitats { get; set; }

    public virtual DbSet<Zoo> Zoos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=Project");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Animal>(entity =>
        {
            entity.HasOne(d => d.Habitat).WithMany(p => p.Animals)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_animals_habitats");
        });

        modelBuilder.Entity<Habitat>(entity =>
        {
            entity.HasOne(d => d.Zoo).WithMany(p => p.Habitats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_habitats_zoos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
