using FootballerStatsApi.Data;
using FootballerStatsApi.Dtos;
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
            var playerStats = await dbContext.MatchStatistics
                .Where(stat => stat.FootballerId == footballerId)
                .ToListAsync();

            if (playerStats == null) return null;
            return playerStats;
        }
        public async Task<MatchStatistic?> GetByIdAsync(Guid id)
        {
            var matchStats = await dbContext.MatchStatistics.FindAsync(id);
            if (matchStats == null) return null; 
            
            return matchStats;
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
        public async Task<FootballerStatsSummaryDto?> GetSummaryForFootballerAsync(Guid footballerId)
        {
            var stats = await dbContext.MatchStatistics
                .Where(ms => ms.FootballerId == footballerId)
                .ToListAsync();

            if (!stats.Any()) return null;
            var footballer = await dbContext.Footballers.FindAsync(footballerId);

            return new FootballerStatsSummaryDto
            {
                FootballerId = footballerId,
                Name = footballer.Name,
                TotalMatches = stats.Count,
                TotalGoals = stats.Sum(ms => ms.Goals),
                TotalAssists = stats.Sum(ms => ms.Assists),
                TotalMinutesPlayed = stats.Sum(ms => ms.MinutesPlayed),
                AveragePassCompletion = stats.Average(ms => ms.PassCompletion)
            };
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
