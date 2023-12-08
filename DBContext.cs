using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishFootballChampion
{

    public class Champion : DbContext
    {
        public string connectionString = @"Data Source=HomeDE\SQLEXPRESS;Initial Catalog=Futball;Integrated Security=True;Encrypt=False";

        public DbSet<Players> Players { get; set; }
        public DbSet<Teams> Teams { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<Goals> Goals { get; set; }
        public DbSet<Matches> Matches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
}
