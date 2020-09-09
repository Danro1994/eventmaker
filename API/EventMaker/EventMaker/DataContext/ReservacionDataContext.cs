using EventMaker.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventMaker.DataContext
{
    public class ReservacionDataContext:DbContext
    {
        public DbSet<CategoriaEvento> categoriaEventos{ get; set; }
        public DbSet<Invitado> invitados{ get; set; }
        public DbSet<Evento> eventos{ get; set; }
        public DbSet<Usuario> usuarios{ get; set; }
        public DbSet<Compra> compras { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=KEVIN-PC;DataBase=EventMakerDB;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Compra>().HasKey(q => q.id);
            modelBuilder.Entity<Compra>().Property(e => e.id).IsRequired().UseSqlServerIdentityColumn()
                 .HasMaxLength(50).IsRequired();
        }
    }
}
