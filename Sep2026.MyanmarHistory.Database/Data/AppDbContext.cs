using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Sep2026.MyanmarHistory.Database.AppDbContextModels;

namespace Sep2026.MyanmarHistory.Database.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HistoricalEra> HistoricalEras { get; set; }

    public virtual DbSet<King> Kings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HistoricalEra>(entity =>
        {
            entity.HasKey(e => e.EraId).HasName("PK__Historic__0FEF67E0BC7F87C1");

            entity.Property(e => e.EraName).HasMaxLength(100);
        });

        modelBuilder.Entity<King>(entity =>
        {
            entity.HasKey(e => e.KingId).HasName("PK__Kings__78B6071CC3AF3643");

            entity.Property(e => e.KingName).HasMaxLength(150);

            entity.HasOne(d => d.Era).WithMany(p => p.Kings)
                .HasForeignKey(d => d.EraId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kings_HistoricalEras");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
