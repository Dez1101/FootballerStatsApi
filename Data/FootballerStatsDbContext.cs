using FootballerStatsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FootballerStatsApi.Data
{
    public class FootballerStatsDbContext : DbContext
    {
        public FootballerStatsDbContext(DbContextOptions<FootballerStatsDbContext> options) : base(options) { }

        public DbSet<Footballer> Footballers { get; set; }
        public DbSet<MatchStatistic> MatchStatistics { get; set; }
    }
}
