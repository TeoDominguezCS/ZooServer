using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace zooModel;

public partial class ProjectSourceContext : IdentityDbContext<ZooUser>
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

    //removing hardcoded db connection
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
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
