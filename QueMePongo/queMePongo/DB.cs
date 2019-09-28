using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueMePongo
{

    public class DB : DbContext
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

        }

        public void clear(DbSet dbSet)
        {
            dbSet.RemoveRange(dbSet);
        }

        public Usuario[] consultarUsuarios()
        {
            return usuarios.ToArray();
        }

        public Guardarropa[] consultarGuardarropas()
        {
            return guardarropas.ToArray();
        }

        public Evento[] consultarEventos()
        {
            return eventos.ToArray();
        }

        public Prenda[] consultarPrendas()
        {
            return prendas.ToArray();
        }

        public Atuendo[] consultarAtuendos()
        {
            return atuendos.ToArray();
        }

        public TipoPrenda[] consultarTipoPrendas()
        {
            return tipoprendas.ToArray();
        }

        public Tela[] consultarTelas()
        {
            return telas.ToArray();
        }

        public void limpiarDB()
        {
            clear(usuarios);
            clear(guardarropas);
            clear(eventos);
            clear(prendas);
            clear(atuendos);
            clear(tipoprendas);
            clear(telas);
        }
    }

}