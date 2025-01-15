using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Shoesify.Entities.Models;

public partial class ShoesifyContext : DbContext
{
    public ShoesifyContext()
    {
    }

    public ShoesifyContext(DbContextOptions<ShoesifyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Export> Exports { get; set; }

    public virtual DbSet<ExportDetail> ExportDetails { get; set; }

    public virtual DbSet<Import> Imports { get; set; }

    public virtual DbSet<ImportDetail> ImportDetails { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Shoe> Shoes { get; set; }

    public virtual DbSet<ShoesDetail> ShoesDetails { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =ADMIN-PC; database = Shoesify;uid=sa;pwd=12345;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2B12B192DB");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(20);
        });

        modelBuilder.Entity<Export>(entity =>
        {
            entity.HasKey(e => e.ExportId).HasName("PK__Export__E5C997A45545D759");

            entity.ToTable("Export");

            entity.Property(e => e.ExportId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ExportID");
            entity.Property(e => e.InventoryId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("InventoryID");

            entity.HasOne(d => d.Inventory).WithMany(p => p.Exports)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK__Export__Inventor__68487DD7");
        });

        modelBuilder.Entity<ExportDetail>(entity =>
        {
            entity.HasKey(e => new { e.ExportId, e.ShoeDetailId }).HasName("PK__ExportDe__93CAAE7FF33A1670");

            entity.ToTable("ExportDetail");

            entity.Property(e => e.ExportId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ExportID");
            entity.Property(e => e.ShoeDetailId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ShoeDetailID");

            entity.HasOne(d => d.Export).WithMany(p => p.ExportDetails)
                .HasForeignKey(d => d.ExportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExportDet__Expor__6B24EA82");

            entity.HasOne(d => d.ShoeDetail).WithMany(p => p.ExportDetails)
                .HasForeignKey(d => d.ShoeDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ExportDet__ShoeD__6C190EBB");
        });

        modelBuilder.Entity<Import>(entity =>
        {
            entity.HasKey(e => e.ImportId).HasName("PK__Import__8697678AC8B6FDE7");

            entity.ToTable("Import");

            entity.Property(e => e.ImportId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ImportID");
            entity.Property(e => e.InventoryId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("InventoryID");
            entity.Property(e => e.SupplierId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SupplierID");

            entity.HasOne(d => d.Inventory).WithMany(p => p.Imports)
                .HasForeignKey(d => d.InventoryId)
                .HasConstraintName("FK__Import__Inventor__60A75C0F");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Imports)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__Import__Supplier__619B8048");
        });

        modelBuilder.Entity<ImportDetail>(entity =>
        {
            entity.HasKey(e => new { e.ImportId, e.ShoeDetailId }).HasName("PK__ImportDe__F0945E51BD08F1BD");

            entity.ToTable("ImportDetail");

            entity.Property(e => e.ImportId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ImportID");
            entity.Property(e => e.ShoeDetailId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ShoeDetailID");

            entity.HasOne(d => d.Import).WithMany(p => p.ImportDetails)
                .HasForeignKey(d => d.ImportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ImportDet__Impor__6477ECF3");

            entity.HasOne(d => d.ShoeDetail).WithMany(p => p.ImportDetails)
                .HasForeignKey(d => d.ShoeDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ImportDet__ShoeD__656C112C");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PK__Inventor__F5FDE6D3D3CA1B29");

            entity.ToTable("Inventory");

            entity.HasIndex(e => e.UserId, "UQ__Inventor__1788CCAD012C628C").IsUnique();

            entity.Property(e => e.InventoryId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("InventoryID");
            entity.Property(e => e.Location).HasMaxLength(30);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Status).HasDefaultValue(true);
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("UserID");

            entity.HasOne(d => d.User).WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(d => d.UserId)
                .HasConstraintName("FK__Inventory__UserI__571DF1D5");
        });

        modelBuilder.Entity<Shoe>(entity =>
        {
            entity.HasKey(e => e.ShoeId).HasName("PK__Shoes__5A8354151709F69E");

            entity.Property(e => e.ShoeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ShoeID");
            entity.Property(e => e.Brand).HasMaxLength(20);
            entity.Property(e => e.CategoryId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CategoryID");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.Category).WithMany(p => p.Shoes)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Shoes__CategoryI__4E88ABD4");
        });

        modelBuilder.Entity<ShoesDetail>(entity =>
        {
            entity.HasKey(e => e.ShoeDetailId).HasName("PK__ShoesDet__60339DB61C4FE1CB");

            entity.ToTable("ShoesDetail");

            entity.Property(e => e.ShoeDetailId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ShoeDetailID");
            entity.Property(e => e.Color).HasMaxLength(10);
            entity.Property(e => e.ShoeId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ShoeID");
            entity.Property(e => e.Status).HasDefaultValue(true);

            entity.HasOne(d => d.Shoe).WithMany(p => p.ShoesDetails)
                .HasForeignKey(d => d.ShoeId)
                .HasConstraintName("FK__ShoesDeta__ShoeI__52593CB8");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => new { e.ShoeDetailId, e.InventoryId }).HasName("PK__Stock__4F6C43DB9256D1C8");

            entity.ToTable("Stock");

            entity.Property(e => e.ShoeDetailId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ShoeDetailID");
            entity.Property(e => e.InventoryId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("InventoryID");

            entity.HasOne(d => d.Inventory).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.InventoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stock__Inventory__5BE2A6F2");

            entity.HasOne(d => d.ShoeDetail).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ShoeDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Stock__ShoeDetai__5AEE82B9");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE666947047A760");

            entity.ToTable("Supplier");

            entity.Property(e => e.SupplierId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SupplierID");
            entity.Property(e => e.SupplierAddress).HasMaxLength(50);
            entity.Property(e => e.SupplierEmail)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SupplierName).HasMaxLength(20);
            entity.Property(e => e.SupplierPhone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CCAC99CEA8F2");

            entity.ToTable("User");

            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(20);
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValue(true);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
