using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OrderManagementDemo.Model;

public partial class OrderMgntDbContext : DbContext
{
    public OrderMgntDbContext()
    {
    }

    public OrderMgntDbContext(DbContextOptions<OrderMgntDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderTable> OrderTables { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source =DESKTOP-8PGNBNF\\SQLEXPRESS; Initial Catalog = OrderMgntDB; Integrated Security = True;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__8CB382B1D25268EC");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Customer_name");
            entity.Property(e => e.CustomerNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Item__3FB403ACE66C8F31");

            entity.ToTable("Item");

            entity.Property(e => e.ItemId).HasColumnName("Item_id");
            entity.Property(e => e.ItemName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Item_Name");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__Order_It__2F31262A3A555762");

            entity.ToTable("Order_Item");

            entity.Property(e => e.OrderItemId).HasColumnName("OrderItem_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.UnitePrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Item).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__Order_Ite__item___4F7CD00D");
        });

        modelBuilder.Entity<OrderTable>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__OrderTab__F1E4607B09579BBC");

            entity.ToTable("OrderTable");

            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("Order_date");
            entity.Property(e => e.OrderItemId).HasColumnName("OrderItem_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.OrderTables)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__OrderTabl__Custo__5441852A");

            entity.HasOne(d => d.OrderItem).WithMany(p => p.OrderTables)
                .HasForeignKey(d => d.OrderItemId)
                .HasConstraintName("FK__OrderTabl__Order__5535A963");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
