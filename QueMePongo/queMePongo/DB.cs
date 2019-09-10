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

        public DB() : base(nameOrConnectionString: "heroku")
        {

            Database.SetInitializer<DB>(null);

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            base.OnModelCreating(modelBuilder);

            /*modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Guardarropas)
                .WithMany(g => g.Usuarios)
                .Map(cs =>
                {
                    cs.MapLeftKey("id_usuario");
                    cs.MapRightKey("id_guardarropa");
                    cs.ToTable("guardarropaXusuario");
                });*/

        }

        public void clear(DbSet dbSet)
        {
            dbSet.RemoveRange(dbSet);
        }

    }

}