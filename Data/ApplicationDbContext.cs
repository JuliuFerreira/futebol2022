using futebol2022.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace futebol2022.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Jogador> TB_Jogadores { get; set; }

        public DbSet<Storage> TB_Storage { get; set; }

        public DbSet<Partidas> TB_Partidas { get; set; }
        public DbSet <JogadorStorage> TB_JogadorStorage { get; set; }


    }

}
