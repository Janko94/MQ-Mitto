using Microsoft.EntityFrameworkCore;
using Model.Model;

namespace Model.Context
{
    public class MittoContext : DbContext
    {
        public MittoContext()
        {
        }

        public MittoContext(DbContextOptions<MittoContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        public virtual DbSet<CountryCode> CountryCode { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region CountryCode
            modelBuilder.Entity<CountryCode>(entity =>
            {
                entity.HasKey(e => new { e.AUID });

                entity.Property(e => e.AUID)
                    .HasColumnName("CC_AUID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.mcc)
                    .HasColumnName("CC_MobileCountryCode")
                    .HasMaxLength(4);

                entity.Property(e => e.cc)
                    .HasColumnName("CC_CountryCode")
                    .HasMaxLength(4);

                entity.Property(e => e.Name)
                    .HasColumnName("CC_Name");

                entity.Property(e => e.pricePerSMS)
                    .HasColumnName("CC_PricePerSMS");
                

            });
            #endregion

            #region SMS
            modelBuilder.Entity<SMS>(entity =>
            {
                entity.HasKey(e => new { e.AUID });

                entity.Property(e => e.AUID)
                    .HasColumnName("SMS_AUID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.From)
                    .HasColumnName("SMS_From")
                    .HasMaxLength(100);

                entity.Property(e => e.To)
                    .HasColumnName("SMS_To")
                    .HasMaxLength(100);

                entity.Property(e => e.Text)
                    .HasColumnName("SMS_Text");

                entity.Property(e => e.CC_From)
                    .HasColumnName("SMS_CC_From");

                entity.Property(e => e.CC_To)
                    .HasColumnName("SMS_CC_To");

                entity.Property(e => e.State)
                    .HasColumnName("SMS_State");


                entity.Property(e => e.dateTime)
                    .HasColumnName("SMS_DateTime");
            });
            #endregion
        }
    }
}
