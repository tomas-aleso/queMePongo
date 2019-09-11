using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{

    class DB : DbContext
    {

        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Guardarropa> guardarropas { get; set; }
        public DbSet<Evento> eventos { get; set; }
        public DbSet<Prenda> prendas { get; set; }
        public DbSet<Atuendo> atuendos { get; set; }
        public DbSet<TipoPrenda> tipoprendas { get; set; }
        public DbSet<Tela> telas { get; set; }

        public DB() : base(nameOrConnectionString: "heroku")
        {

            Database.SetInitializer<DB>(null);

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasMany<Guardarropa>(u => u.Guardarropas)
                .WithMany(g => g.Usuarios)
                .Map(cs =>
                {
                    cs.MapLeftKey("id_usuario");
                    cs.MapRightKey("id_guardarropa");
                    cs.ToTable("guardarropaxusuario");
                });

           /* modelBuilder.Entity<Guardarropa>()
                .HasMany<Prenda>(g => g.Prendas)
                .WithMany(p => p.Guardarropas)
                .Map(cd =>
                {
                    cd.MapLeftKey("id_guardarropa");
                    cd.MapRightKey("id_prenda");
                    cd.ToTable("prendaxguardarropa");
                });*/

        }

        public void clear(DbSet dbSet)
        {
            dbSet.RemoveRange(dbSet);
        }

    }

}