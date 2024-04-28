using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GetResearch.Db.Entity;

public partial class PostgresContext : DbContext
{
    public PostgresContext(DbContextOptions<PostgresContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public virtual DbSet<application> applications { get; set; }

    public virtual DbSet<research> researches { get; set; }

    public virtual DbSet<user> users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<application>(entity =>
        {
            entity.HasKey(e => e.id).HasName("application_pkey");

            entity.ToTable("application");

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.time).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<research>(entity =>
        {
            entity.HasKey(e => e.id).HasName("research_pkey");

            entity.ToTable("research");

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.time).HasDefaultValueSql("now()");
        });

        modelBuilder.Entity<user>(entity =>
        {
            entity.HasKey(e => e.id).HasName("users_pkey");

            entity.Property(e => e.id).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.time).HasDefaultValueSql("now()");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
