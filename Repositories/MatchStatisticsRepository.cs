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
        public async Task<List<MatchStatistic>> GetAllForFootballerAsync(Guid footballerId)
        {
            return await dbContext.MatchStatistics
                .Where(stat => stat.FootballerId == footballerId)
                .ToListAsync();
        }
        public async Task<MatchStatistic?> GetByIdAsync(Guid id)
        {
            var matchStats = await dbContext.MatchStatistics.FindAsync(id);
            if (matchStats == null) return null; 
            
            return matchStats;
        }           
        public async Task<MatchStatistic> AddAsync(MatchStatistic stat)
        {
            try
            {
                await dbContext.MatchStatistics.AddAsync(stat);
                await dbContext.SaveChangesAsync();
                return stat;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding match statistic", ex);
            }
        }
        public async Task<MatchStatistic?> UpdateAsync(Guid id, MatchStatistic stat)
        {
            if (stat == null)
                throw new ArgumentNullException(nameof(stat));

            try
            {
                var existing = await dbContext.MatchStatistics.FindAsync(id);
                if (existing == null)
                    return null;

                existing.Opposition = stat.Opposition;
                existing.MinutesPlayed = stat.MinutesPlayed;
                existing.Goals = stat.Goals;
                existing.Assists = stat.Assists;
                existing.PassCompletion = stat.PassCompletion;

                await dbContext.SaveChangesAsync();
                return existing;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while updating the match statistic.", ex);
            }
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
