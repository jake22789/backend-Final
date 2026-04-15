using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace final_proj.Models;

public partial class Db26TeamoneContext : DbContext
{
    public Db26TeamoneContext()
    {
    }

    public Db26TeamoneContext(DbContextOptions<Db26TeamoneContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aisle> Aisles { get; set; }

    public virtual DbSet<Aisle1> Aisles1 { get; set; }

    public virtual DbSet<Bay> Bays { get; set; }

    public virtual DbSet<Bay1> Bays1 { get; set; }

    public virtual DbSet<Bin> Bins { get; set; }

    public virtual DbSet<Bin1> Bins1 { get; set; }

    public virtual DbSet<BinLocation> BinLocations { get; set; }

    public virtual DbSet<BinLocation1> BinLocations1 { get; set; }

    public virtual DbSet<BinType> BinTypes { get; set; }

    public virtual DbSet<BinType1> BinTypes1 { get; set; }

    public virtual DbSet<Carrier> Carriers { get; set; }

    public virtual DbSet<Carrier1> Carriers1 { get; set; }

    public virtual DbSet<ContainerType> ContainerTypes { get; set; }

    public virtual DbSet<ContainerType1> ContainerTypes1 { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Customer1> Customers1 { get; set; }

    public virtual DbSet<CustomerOrder> CustomerOrders { get; set; }

    public virtual DbSet<CustomerOrder1> CustomerOrders1 { get; set; }

    public virtual DbSet<CustomerOrderToContainer> CustomerOrderToContainers { get; set; }

    public virtual DbSet<CustomerOrderToContainer1> CustomerOrderToContainers1 { get; set; }

    public virtual DbSet<CustomerOrderToItem> CustomerOrderToItems { get; set; }

    public virtual DbSet<CustomerOrderToItem1> CustomerOrderToItems1 { get; set; }

    public virtual DbSet<Hebera> Heberas { get; set; }

    public virtual DbSet<Heberabeforetest> Heberabeforetests { get; set; }

    public virtual DbSet<Heberb> Heberbs { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Item1> Items1 { get; set; }

    public virtual DbSet<ItemShipment> ItemShipments { get; set; }

    public virtual DbSet<ItemShipment1> ItemShipments1 { get; set; }

    public virtual DbSet<OrderVolume> OrderVolumes { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<PurchaseOrder1> PurchaseOrders1 { get; set; }

    public virtual DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }

    public virtual DbSet<PurchaseOrderItem1> PurchaseOrderItems1 { get; set; }

    public virtual DbSet<Shelf> Shelves { get; set; }

    public virtual DbSet<Shelf1> Shelves1 { get; set; }

    public virtual DbSet<Shipment> Shipments { get; set; }

    public virtual DbSet<Shipment1> Shipments1 { get; set; }

    public virtual DbSet<ShipmentAction> ShipmentActions { get; set; }

    public virtual DbSet<ShipmentAction1> ShipmentActions1 { get; set; }

    public virtual DbSet<ShippingContainer> ShippingContainers { get; set; }

    public virtual DbSet<ShippingContainer1> ShippingContainers1 { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    public virtual DbSet<Vendor1> Vendors1 { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=database-1.cisqkskacvfb.us-west-2.rds.amazonaws.com;Port=5432;Database=db26_teamone;Username=teamone;Password=secret987654secret3210");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("test_schema", "alphabet_enum", new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" })
            .HasPostgresEnum("warehouse", "alphabet_enum", new[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" });

        modelBuilder.Entity<Aisle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("aisle_pkey");

            entity.ToTable("aisle", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Aisle1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("aisle_pkey");

            entity.ToTable("aisle", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Bay>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bay_pkey");

            entity.ToTable("bay", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AisleId).HasColumnName("aisle_id");

            entity.HasOne(d => d.Aisle).WithMany(p => p.Bays)
                .HasForeignKey(d => d.AisleId)
                .HasConstraintName("bay_aisle_fkey");
        });

        modelBuilder.Entity<Bay1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bay_pkey");

            entity.ToTable("bay", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AisleId).HasColumnName("aisle_id");

            entity.HasOne(d => d.Aisle).WithMany(p => p.Bay1s)
                .HasForeignKey(d => d.AisleId)
                .HasConstraintName("bay_aisle_id_fkey");
        });

        modelBuilder.Entity<Bin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bin_pkey");

            entity.ToTable("bin", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Type).HasColumnName("type");

            entity.HasOne(d => d.TypeNavigation).WithMany(p => p.Bins)
                .HasForeignKey(d => d.Type)
                .HasConstraintName("bin_bin_type_fkey");
        });

        modelBuilder.Entity<Bin1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bin_pkey");

            entity.ToTable("bin", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BinType).HasColumnName("bin_type");

            entity.HasOne(d => d.BinTypeNavigation).WithMany(p => p.Bin1s)
                .HasForeignKey(d => d.BinType)
                .HasConstraintName("bin_bintype_fkey");
        });

        modelBuilder.Entity<BinLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bin_location_pkey");

            entity.ToTable("bin_location", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BinId).HasColumnName("bin_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ShelfId).HasColumnName("shelf_id");

            entity.HasOne(d => d.Bin).WithMany(p => p.BinLocations)
                .HasForeignKey(d => d.BinId)
                .HasConstraintName("bin_location_bin_id_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.BinLocations)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("bin_location_item_id_fkey");

            entity.HasOne(d => d.Shelf).WithMany(p => p.BinLocations)
                .HasForeignKey(d => d.ShelfId)
                .HasConstraintName("bin_location_shelf_id_fkey");
        });

        modelBuilder.Entity<BinLocation1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bay_location_pkey");

            entity.ToTable("bin_location", "warehouse");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('warehouse.bay_location_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.BinId).HasColumnName("bin_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ShelfId).HasColumnName("shelf_id");

            entity.HasOne(d => d.Bin).WithMany(p => p.BinLocation1s)
                .HasForeignKey(d => d.BinId)
                .HasConstraintName("bay_location_bin_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.BinLocation1s)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("bin_location_item_id_fkey");

            entity.HasOne(d => d.Shelf).WithMany(p => p.BinLocation1s)
                .HasForeignKey(d => d.ShelfId)
                .HasConstraintName("bay_location_shelf_fkey");
        });

        modelBuilder.Entity<BinType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bin_type_pkey");

            entity.ToTable("bin_type", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Width).HasColumnName("width");
        });

        modelBuilder.Entity<BinType1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bin_type_pkey");

            entity.ToTable("bin_type", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Length).HasColumnName("length");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Width).HasColumnName("width");
        });

        modelBuilder.Entity<Carrier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("carrier_pkey");

            entity.ToTable("carrier", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Rate)
                .HasPrecision(10, 2)
                .HasColumnName("rate");
        });

        modelBuilder.Entity<Carrier1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("carrier_pkey");

            entity.ToTable("carrier", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ContainerType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("container_type_pkey");

            entity.ToTable("container_type", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Volume)
                .HasPrecision(10, 2)
                .HasColumnName("volume");
        });

        modelBuilder.Entity<ContainerType1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("container_type_pkey");

            entity.ToTable("container_type", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Volume)
                .HasPrecision(10, 2)
                .HasColumnName("volume");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customer_pkey");

            entity.ToTable("customer", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(10)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Customer1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customer_pkey");

            entity.ToTable("customer", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<CustomerOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customer_order_pkey");

            entity.ToTable("customer_order", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.ShippingFee)
                .HasPrecision(10, 2)
                .HasColumnName("shipping_fee");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerOrders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("fk_customer_id");
        });

        modelBuilder.Entity<CustomerOrder1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customer_order_pkey");

            entity.ToTable("customer_order", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.ShippingFee)
                .HasPrecision(10, 2)
                .HasColumnName("shipping_fee");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerOrder1s)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("customer_order_customer_id_fkey");
        });

        modelBuilder.Entity<CustomerOrderToContainer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customer_order_to_container_pkey");

            entity.ToTable("customer_order_to_container", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.ShippingContainer).HasColumnName("shipping_container");

            entity.HasOne(d => d.OrderItem).WithMany(p => p.CustomerOrderToContainers)
                .HasForeignKey(d => d.OrderItemId)
                .HasConstraintName("customer_order_to_container_order_item_id_fkey");

            entity.HasOne(d => d.ShippingContainerNavigation).WithMany(p => p.CustomerOrderToContainers)
                .HasForeignKey(d => d.ShippingContainer)
                .HasConstraintName("customer_order_to_container_shipping_container_fkey");
        });

        modelBuilder.Entity<CustomerOrderToContainer1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customer_order_to_container_pkey");

            entity.ToTable("customer_order_to_container", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.ShippingContainer).HasColumnName("shipping_container");

            entity.HasOne(d => d.OrderItem).WithMany(p => p.CustomerOrderToContainer1s)
                .HasForeignKey(d => d.OrderItemId)
                .HasConstraintName("customer_order_to_container_order_item_id_fkey");

            entity.HasOne(d => d.ShippingContainerNavigation).WithMany(p => p.CustomerOrderToContainer1s)
                .HasForeignKey(d => d.ShippingContainer)
                .HasConstraintName("customer_order_to_container_shipping_container_fkey");
        });

        modelBuilder.Entity<CustomerOrderToItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customer_order_to_items_pkey");

            entity.ToTable("customer_order_to_items", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Qty).HasColumnName("qty");

            entity.HasOne(d => d.Item).WithMany(p => p.CustomerOrderToItems)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("customer_order_to_items_item_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.CustomerOrderToItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("customer_order_to_items_order_id_fkey");
        });

        modelBuilder.Entity<CustomerOrderToItem1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("customer_order_to_items_pkey");

            entity.ToTable("customer_order_to_items", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Qty).HasColumnName("qty");

            entity.HasOne(d => d.Item).WithMany(p => p.CustomerOrderToItem1s)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("customer_order_to_items_item_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.CustomerOrderToItem1s)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("customer_order_to_items_order_id_fkey");
        });

        modelBuilder.Entity<Hebera>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("hebera_pkey");

            entity.ToTable("hebera", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Heberabeforetest>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("heberabeforetest", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Heberb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("heberb_pkey");

            entity.ToTable("heberb", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AId).HasColumnName("a_id");
            entity.Property(e => e.Description).HasColumnName("description");

            entity.HasOne(d => d.AIdNavigation).WithMany(p => p.Heberbs)
                .HasForeignKey(d => d.AId)
                .HasConstraintName("heberb_a_id_fkey");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("item_pkey");

            entity.ToTable("item", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Volume)
                .HasPrecision(10, 2)
                .HasColumnName("volume");
        });

        modelBuilder.Entity<Item1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("item_pkey");

            entity.ToTable("item", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ItemName)
                .HasMaxLength(50)
                .HasColumnName("item_name");
            entity.Property(e => e.ItemSize)
                .HasPrecision(10, 2)
                .HasColumnName("item_size");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
        });

        modelBuilder.Entity<ItemShipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("item_shipment_pkey");

            entity.ToTable("item_shipment", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActionId).HasColumnName("action_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ShipmentId).HasColumnName("shipment_id");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Action).WithMany(p => p.ItemShipments)
                .HasForeignKey(d => d.ActionId)
                .HasConstraintName("action_id_fk");

            entity.HasOne(d => d.Item).WithMany(p => p.ItemShipments)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("item_id_fk");

            entity.HasOne(d => d.Shipment).WithMany(p => p.ItemShipments)
                .HasForeignKey(d => d.ShipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shipment_id_fk");
        });

        modelBuilder.Entity<ItemShipment1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("item_shipment_pkey");

            entity.ToTable("item_shipment", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActionId).HasColumnName("action_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ShipmentId).HasColumnName("shipment_id");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Action).WithMany(p => p.ItemShipment1s)
                .HasForeignKey(d => d.ActionId)
                .HasConstraintName("item_shipment_action_id_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.ItemShipment1s)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("item_shipment_item_id_fkey");

            entity.HasOne(d => d.Shipment).WithMany(p => p.ItemShipment1s)
                .HasForeignKey(d => d.ShipmentId)
                .HasConstraintName("item_shipment_shipment_id_fkey");
        });

        modelBuilder.Entity<OrderVolume>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("order_volume", "test_schema");

            entity.Property(e => e.Sum).HasColumnName("sum");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("purchase_order_pkey");

            entity.ToTable("purchase_order", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateOrdered)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_ordered");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");

            entity.HasOne(d => d.Vendor).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("purchase_order_vendor_id_fkey");
        });

        modelBuilder.Entity<PurchaseOrder1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_pkey");

            entity.ToTable("purchase_order", "warehouse");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('warehouse.order_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.DateOrdered)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_ordered");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");

            entity.HasOne(d => d.Vendor).WithMany(p => p.PurchaseOrder1s)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("purchase_order_vendor_id_fkey");
        });

        modelBuilder.Entity<PurchaseOrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("purchase_order_item_pkey");

            entity.ToTable("purchase_order_item", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Item).WithMany(p => p.PurchaseOrderItems)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("purchase_order_item_item_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.PurchaseOrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("purchase_order_item_order_id_fkey");
        });

        modelBuilder.Entity<PurchaseOrderItem1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("purchase_order_item_pkey");

            entity.ToTable("purchase_order_item", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Item).WithMany(p => p.PurchaseOrderItem1s)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("purchase_order_item_item_id_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.PurchaseOrderItem1s)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("purchase_order_item_order_id_fkey");
        });

        modelBuilder.Entity<Shelf>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shelf_pkey");

            entity.ToTable("shelf", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BayId).HasColumnName("bay_id");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.ShelfNumber).HasColumnName("shelf_number");

            entity.HasOne(d => d.Bay).WithMany(p => p.Shelves)
                .HasForeignKey(d => d.BayId)
                .HasConstraintName("shelf_bay_fkey");
        });

        modelBuilder.Entity<Shelf1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shelf_pkey");

            entity.ToTable("shelf", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BayId).HasColumnName("bay_id");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.ShelfNumber).HasColumnName("shelf_number");

            entity.HasOne(d => d.Bay).WithMany(p => p.Shelf1s)
                .HasForeignKey(d => d.BayId)
                .HasConstraintName("shelf_bay_id_fkey");
        });

        modelBuilder.Entity<Shipment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shipment_pkey");

            entity.ToTable("shipment", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateReceived)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_received");
            entity.Property(e => e.HandlingCost)
                .HasPrecision(10, 2)
                .HasColumnName("handling_cost");
            entity.Property(e => e.OrderId).HasColumnName("order_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Shipments)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("order_id_fk");
        });

        modelBuilder.Entity<Shipment1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shipment_pkey");

            entity.ToTable("shipment", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateReceived)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_received");
            entity.Property(e => e.HandlingCost)
                .HasPrecision(10, 2)
                .HasColumnName("handling_cost");
            entity.Property(e => e.OrderId).HasColumnName("order_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Shipment1s)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("shipment_order_id_fkey");
        });

        modelBuilder.Entity<ShipmentAction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("action_pkey");

            entity.ToTable("shipment_action", "warehouse");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('warehouse.action_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(15)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ShipmentAction1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shipment_action_pkey");

            entity.ToTable("shipment_action", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ShippingContainer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shipping_container_pkey");

            entity.ToTable("shipping_container", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CarrierId).HasColumnName("carrier_id");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.ShippingFee)
                .HasPrecision(10, 2)
                .HasColumnName("shipping_fee");
            entity.Property(e => e.TrackingNumber)
                .HasMaxLength(100)
                .HasColumnName("tracking_number");
            entity.Property(e => e.TypeId).HasColumnName("type_id");

            entity.HasOne(d => d.Carrier).WithMany(p => p.ShippingContainers)
                .HasForeignKey(d => d.CarrierId)
                .HasConstraintName("shipping_container_carrier_id_fkey");

            entity.HasOne(d => d.Type).WithMany(p => p.ShippingContainers)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("shipping_container_type_id_fkey");
        });

        modelBuilder.Entity<ShippingContainer1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("shipping_container_pkey");

            entity.ToTable("shipping_container", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CarrierId).HasColumnName("carrier_id");
            entity.Property(e => e.ContainerType).HasColumnName("container_type");
            entity.Property(e => e.Date)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date");
            entity.Property(e => e.ShippingFee)
                .HasPrecision(10, 2)
                .HasColumnName("shipping_fee");
            entity.Property(e => e.TrackingNumber)
                .HasMaxLength(100)
                .HasColumnName("tracking_number");

            entity.HasOne(d => d.Carrier).WithMany(p => p.ShippingContainer1s)
                .HasForeignKey(d => d.CarrierId)
                .HasConstraintName("shipping_container_carrier_id_fkey");

            entity.HasOne(d => d.ContainerTypeNavigation).WithMany(p => p.ShippingContainer1s)
                .HasForeignKey(d => d.ContainerType)
                .HasConstraintName("fk_container_type");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vendor_pkey");

            entity.ToTable("vendor", "test_schema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Vendor1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vendor_pkey");

            entity.ToTable("vendor", "warehouse");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
