using BackChat.Models;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace BackChat.DataContext
{
    public class Context : DbContext
    {
        public DbSet<Sala> Sala { get; set; }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Enrolado> Enrolado { get; set; }

        public DbSet<ChatTracebility> ChatTracebility { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=chatJaya;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

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