using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CustomerService.Models;

public partial class SalesOrderManagementContext : DbContext
{
    public SalesOrderManagementContext()
    {
    }

    public SalesOrderManagementContext(DbContextOptions<SalesOrderManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ComCustomer> ComCustomers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=sales_order_management;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ComCustomer>(entity =>
        {
            entity.HasKey(e => e.ComCustomerId).HasName("PK__COM_CUST__898A90BC28F11709");

            entity.ToTable("COM_CUSTOMER");

            entity.Property(e => e.ComCustomerId).HasColumnName("COM_CUSTOMER_ID");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("CUSTOMER_NAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
