using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace PassOne.Models.Mapping
{
    public class CredentialMap : EntityTypeConfiguration<EntityCredential>
    {
        public CredentialMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Website)
                .IsRequired();

            this.Property(t => t.Url)
                .IsRequired();

            this.Property(t => t.Username)
                .IsRequired();

            this.Property(t => t.Email)
                .IsRequired();

            this.Property(t => t.Password)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Credentials");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Website).HasColumnName("Website");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.Username).HasColumnName("Username");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Password).HasColumnName("Password");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.Credentials)
                .HasForeignKey(d => d.UserId);

        }
    }
}
