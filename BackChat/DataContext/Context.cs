using BackChat.Models;
using Microsoft.EntityFrameworkCore;

namespace BackChat.DataContext
{
    public class Context : DbContext
    {
        public DbSet<Sala> Sala { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Enrolado> Enrolado { get; set; }

        public DbSet<ChatTracebility> ChatTracebility { get; set; }

        public Context(DbContextOptions dbContext) : base(dbContext) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sala>(entity =>
            {
                entity
                .HasOne(sa => sa.Enrolado)
                .WithOne(en => en.Sala)
                .HasForeignKey<Enrolado>(En => En.SalaId);
            });
                
            modelBuilder.Entity<Usuario>( entiry => {
                entiry
                .HasOne(us => us.Enrolado)
                .WithOne(en => en.Usuario)
                .HasForeignKey<Enrolado>(En => En.UsuarioId);
            });

            modelBuilder.Entity<ChatTracebility>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.HasOne(c => c.enrolado);
            });
        }
    }
}