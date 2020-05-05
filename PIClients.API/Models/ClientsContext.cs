using Microsoft.EntityFrameworkCore;

namespace PIClients.API.Models
{
  public partial class ClientsContext : DbContext
    {
        public ClientsContext(DbContextOptions<ClientsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cities> Cities { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<GenderDescriptions> GenderDescriptions { get; set; }
        public virtual DbSet<PhoneNumberTypes> PhoneNumberTypes { get; set; }
        public virtual DbSet<PhoneNumbers> PhoneNumbers { get; set; }
        public virtual DbSet<RelatedClients> RelatedClients { get; set; }
        public virtual DbSet<RelationTypes> RelationTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cities>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Clients>(entity =>
            {
                entity.HasKey(e => e.ClientId);

                entity.Property(e => e.BirthDate).HasColumnType("smalldatetime");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PersonalNumber)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.Photo).IsRequired();

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clients_Cities");

                entity.HasOne(d => d.GenderDescription)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.GenderDescriptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clients_GenderDescriptions");
            });

            modelBuilder.Entity<GenderDescriptions>(entity =>
            {
                entity.HasKey(e => e.GenderDescriptionId);

                entity.Property(e => e.GenderDescriptionId).ValueGeneratedNever();

                entity.Property(e => e.GenderDescription).HasMaxLength(50);
            });

            modelBuilder.Entity<PhoneNumberTypes>(entity =>
            {
                entity.HasKey(e => e.PhoneNumberTypeId)
                    .HasName("PK_PhoneTypes");

                entity.Property(e => e.PhoneNumberTypeId).ValueGeneratedNever();

                entity.Property(e => e.PhoneNumberTypeDescription)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PhoneNumbers>(entity =>
            {
                entity.HasKey(e => e.PhoneNumberId);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.PhoneNumbers)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhoneNumbers_Clients");

                entity.HasOne(d => d.PhoneNumberType)
                    .WithMany(p => p.PhoneNumbers)
                    .HasForeignKey(d => d.PhoneNumberTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhoneNumbers_PhoneNumberTypes");
            });

            modelBuilder.Entity<RelatedClients>(entity =>
            {
                //entity.Property(e => e.RelatedClientsId).ValueGeneratedNever();

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.RelatedClientsClient)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RelatedClients_Clients");

                entity.HasOne(d => d.RelatedClientsNavigation)
                    .WithOne(p => p.RelatedClientsRelatedClientsNavigation)
                    .HasForeignKey<RelatedClients>(d => d.RelatedClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RelatedClients_Clients1");

                entity.HasOne(d => d.RelationType)
                    .WithMany(p => p.RelatedClients)
                    .HasForeignKey(d => d.RelationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RelatedClients_RelationTypes");
            });

            modelBuilder.Entity<RelationTypes>(entity =>
            {
                entity.HasKey(e => e.RelationTypeId);

                entity.Property(e => e.RelationTypeId).ValueGeneratedNever();

                entity.Property(e => e.RelationTypeDescription)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
