using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using PassOne.Models.Mapping;

namespace PassOne.Models
{
    public partial class PassOneContext : DbContext
    {
        static PassOneContext()
        {
            Database.SetInitializer<PassOneContext>(null);
        }

        public PassOneContext()
            : base("Name=db90b77393cd784be29cbaa1b2016d9466Context")
        {
        }

        public DbSet<EntityCredential> Credentials { get; set; }
        public DbSet<EntityUser> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CredentialMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
