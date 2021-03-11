using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MeterWeb
{
    public partial class DBLibraryContext : DbContext
    {
        public DBLibraryContext()
        {
        }

        public DBLibraryContext(DbContextOptions<DBLibraryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Flat> Flats { get; set; }
        public virtual DbSet<Meter> Meters { get; set; }
        public virtual DbSet<MeterType> MeterTypes { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Reading> Readings { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Tariff> Tariffs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= SCYRITYS\\SERVER; Database=DBLibrary; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Flat>(entity =>
            {
                entity.ToTable("FLATS");

                entity.Property(e => e.FlatId).HasColumnName("FLAT_ID");

                entity.Property(e => e.FlatAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("FLAT_ADDRESS");
            });

            modelBuilder.Entity<Meter>(entity =>
            {
                entity.ToTable("METERS");

                entity.Property(e => e.MeterId).HasColumnName("METER_ID");

                entity.Property(e => e.MeterDataLastReplacement)
                    .HasColumnType("date")
                    .HasColumnName("METER_DATA_LAST_REPLACEMENT");

                entity.Property(e => e.MeterFlatId).HasColumnName("METER_FLAT_ID");

                entity.Property(e => e.MeterNumbers).HasColumnName("METER_NUMBERS");

                entity.Property(e => e.MeterTypeId).HasColumnName("METER_TYPE_ID");

                entity.HasOne(d => d.MeterFlat)
                    .WithMany(p => p.Meters)
                    .HasForeignKey(d => d.MeterFlatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_METERS_FLATS");

                entity.HasOne(d => d.MeterType)
                    .WithMany(p => p.Meters)
                    .HasForeignKey(d => d.MeterTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_METERS_METER_TYPES");
            });

            modelBuilder.Entity<MeterType>(entity =>
            {
                entity.ToTable("METER_TYPES");

                entity.Property(e => e.MeterTypeId).HasColumnName("METER_TYPE_ID");

                entity.Property(e => e.MeterServiceId).HasColumnName("METER_SERVICE_ID");

                entity.Property(e => e.MeterTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("METER_TYPE_NAME");

                entity.HasOne(d => d.MeterService)
                    .WithMany(p => p.MeterTypes)
                    .HasForeignKey(d => d.MeterServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_METER_TYPES_SERVICES");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("PAYMENTS");

                entity.Property(e => e.PaymentId).HasColumnName("PAYMENT_ID");

                entity.Property(e => e.PaymentDataOfCurrrentPayment)
                    .HasColumnType("date")
                    .HasColumnName("PAYMENT_DATA_OF_CURRRENT_PAYMENT");

                entity.Property(e => e.PaymentDiscount)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("PAYMENT_DISCOUNT");

                entity.Property(e => e.PaymentSumOfCurrentMonthPayment)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("PAYMENT_SUM_OF_CURRENT_MONTH_PAYMENT");

                entity.Property(e => e.PaymentTariffId).HasColumnName("PAYMENT_TARIFF_ID");

                entity.HasOne(d => d.PaymentTariff)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.PaymentTariffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PAYMENTS_TARIFFS");
            });

            modelBuilder.Entity<Reading>(entity =>
            {
                entity.ToTable("READINGS");

                entity.Property(e => e.ReadingId)
                    .ValueGeneratedNever()
                    .HasColumnName("READING_ID");

                entity.Property(e => e.ReadingDataOfCurrentReading)
                    .HasColumnType("date")
                    .HasColumnName("READING_DATA_OF_CURRENT_READING");

                entity.Property(e => e.ReadingMeterId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("READING_METER_ID");

                entity.Property(e => e.ReadingPaymentId).HasColumnName("READING_PAYMENT_ID");

                entity.HasOne(d => d.ReadingMeter)
                    .WithMany(p => p.Readings)
                    .HasForeignKey(d => d.ReadingMeterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_READINGS_METERS");

                entity.HasOne(d => d.ReadingPayment)
                    .WithMany(p => p.Readings)
                    .HasForeignKey(d => d.ReadingPaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_READINGS_PAYMENTS");

                entity.Property(e => e.ReadingNumber).HasColumnName("READING_NUMBER");




            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("SERVICES");

                entity.Property(e => e.ServiceId).HasColumnName("SERVICE_ID");

                entity.Property(e => e.ServiceName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("SERVICE_NAME");
            });

            modelBuilder.Entity<Tariff>(entity =>
            {
                entity.ToTable("TARIFFS");

                entity.Property(e => e.TariffId).HasColumnName("TARIFF_ID");

                entity.Property(e => e.TariffPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("TARIFF_PRICE");

                entity.Property(e => e.TariffPrivilege)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("TARIFF_PRIVILEGE");

                entity.Property(e => e.TariffServiceId).HasColumnName("TARIFF_SERVICE_ID");

                entity.HasOne(d => d.TariffService)
                    .WithMany(p => p.Tariffs)
                    .HasForeignKey(d => d.TariffServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TARIFFS_SERVICES");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
