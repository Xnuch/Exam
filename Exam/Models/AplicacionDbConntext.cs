using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Exam.Models
{
    class AplicacionDbConntext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Cine; Integrated Security=True").EnableSensitiveDataLogging(true);

        }
        public DbSet<Peli11> Peli11 { get; set; }
        public DbSet<Video> VideoJuego { get; set; }

    }
}
