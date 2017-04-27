namespace Menhely
{
using System;
using System.Data.Entity;
    using System.Linq;

    public class Menhelyek : DbContext 
    {
        public Menhelyek()
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Telephely> Telephelyek { get; set; }
        public DbSet<Ketrec> Ketrecek { get; set; }
        public DbSet<Allat> Allatok { get; set; }
        public DbSet<Gondozo> Gondozok { get; set; }
        public DbSet<Orokbefogado> Orokbefogadok { get; set; }
       
      
    }
}
