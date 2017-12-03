namespace Homework8.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ArtContext : DbContext
    {
        public ArtContext()
            : base("name=ArtContext")
        {
        }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<ArtWork> ArtWorks { get; set; }
        public virtual DbSet<Classification> Classifications { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>()
                .HasMany(e => e.ArtWorks)
                .WithOptional(e => e.Artist)
                .HasForeignKey(e => e.ArtistID);

            modelBuilder.Entity<Artist>()
                .HasMany(e => e.ArtWorks1)
                .WithOptional(e => e.Artist1)
                .HasForeignKey(e => e.ArtistID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ArtWork>()
                .HasMany(e => e.Classifications)
                .WithRequired(e => e.ArtWork)
                .HasForeignKey(e => e.AWID);

            modelBuilder.Entity<Genre>()
                .Property(e => e.Genre1)
                .IsUnicode(false);
        }
    }
}
