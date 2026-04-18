using Microsoft.EntityFrameworkCore;

namespace final_proj.Models;

public partial class Db26TeamoneContext
{
    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<OrderWorkflowState> OrderWorkflowStates { get; set; }

    public virtual DbSet<InventoryRecord> InventoryRecords { get; set; }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.ToTable("app_user", "warehouse");

            entity.HasKey(e => e.Id).HasName("app_user_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(300)
                .HasColumnName("password_hash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(300)
                .HasColumnName("password_salt");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");

            entity.HasIndex(e => e.Username)
                .IsUnique()
                .HasDatabaseName("app_user_username_uk");
        });

        modelBuilder.Entity<OrderWorkflowState>(entity =>
        {
            entity.ToTable("app_order_workflow_state", "warehouse");

            entity.HasKey(e => e.Id).HasName("app_order_workflow_state_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasColumnName("status");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasIndex(e => e.OrderId)
                .IsUnique()
                .HasDatabaseName("app_order_workflow_state_order_id_uk");

            entity.HasOne(e => e.Order)
                .WithMany()
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("app_order_workflow_state_order_id_fkey");
        });

        modelBuilder.Entity<InventoryRecord>(entity =>
        {
            entity.ToTable("app_inventory", "warehouse");

            entity.HasKey(e => e.Id).HasName("app_inventory_pkey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BinId).HasColumnName("bin_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasIndex(e => e.BinId)
                .IsUnique()
                .HasDatabaseName("app_inventory_bin_id_uk");

            entity.HasOne(e => e.Item)
                .WithMany()
                .HasForeignKey(e => e.ItemId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("app_inventory_item_id_fkey");

            entity.HasOne(e => e.Bin)
                .WithMany()
                .HasForeignKey(e => e.BinId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("app_inventory_bin_id_fkey");
        });
    }
}
