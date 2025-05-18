using FootballerStatsApi.Data;
using FootballerStatsApi.Models;
using FootballerStatsApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootballerStatsApi.Repositories
{
    public class MatchStatisticsRepository : IMatchStatisticsRepository
    {
        private readonly FootballerStatsDbContext dbContext;

        public MatchStatisticsRepository(FootballerStatsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<MatchStatistic>> GetAllAsync()
        {
            return await dbContext.MatchStatistics.ToListAsync();
        }
            

        public async Task<MatchStatistic?> GetByIdAsync(Guid id)
        {
            return await dbContext.MatchStatistics.FindAsync(id);
        }
            

        public async Task<MatchStatistic> AddAsync(MatchStatistic stat)
        {
            await dbContext.MatchStatistics.AddAsync(stat);
            await dbContext.SaveChangesAsync();
            return stat;
        }

        public async Task<MatchStatistic?> UpdateAsync(Guid id, MatchStatistic stat)
        {
            var existing = await dbContext.MatchStatistics.FindAsync(id);
            if (existing == null) return null;

            existing.Opposition = stat.Opposition;
            existing.MinutesPlayed = stat.MinutesPlayed;
            existing.Goals = stat.Goals;
            existing.Assists = stat.Assists;
            existing.PassCompletion = stat.PassCompletion;

            await dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var stat = await dbContext.MatchStatistics.FindAsync(id);
            if (stat == null) return false;

            dbContext.MatchStatistics.Remove(stat);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }

}
