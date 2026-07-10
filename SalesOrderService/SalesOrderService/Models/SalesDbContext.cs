using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SalesOrderService.Models;

public partial class SalesDbContext : DbContext
{
    public SalesDbContext()
    {
    }

    public SalesDbContext(DbContextOptions<SalesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<SalesSo> SalesSos { get; set; }

    public virtual DbSet<SalesSoLitem> SalesSoLitems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=sales_order_management;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SalesSo>(entity =>
        {
            entity.HasKey(e => e.SalesSoId).HasName("PK__SALES_SO__D25C0E7527F9B5A4");

            entity.ToTable("SALES_SO");

            entity.Property(e => e.SalesSoId).HasColumnName("SALES_SO_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.ComCustomerId).HasColumnName("COM_CUSTOMER_ID");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("ORDER_DATE");
            entity.Property(e => e.SoNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("SO_NO");
        });

        modelBuilder.Entity<SalesSoLitem>(entity =>
        {
            entity.HasKey(e => e.SalesSoLitemId).HasName("PK__SALES_SO__B36578C8B978ECDE");

            entity.ToTable("SALES_SO_LITEM");

            entity.Property(e => e.SalesSoLitemId).HasColumnName("SALES_SO_LITEM_ID");
            entity.Property(e => e.ItemName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ITEM_NAME");
            entity.Property(e => e.Price).HasColumnName("PRICE");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
            entity.Property(e => e.SalesSoId).HasColumnName("SALES_SO_ID");

            entity.HasOne(d => d.SalesSo).WithMany(p => p.SalesSoLitems)
                .HasForeignKey(d => d.SalesSoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LITEM_SO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
