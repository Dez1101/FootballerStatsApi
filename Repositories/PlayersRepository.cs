using FootballerStatsApi.Data;
using FootballerStatsApi.Models;
using FootballerStatsApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FootballerStatsApi.Repositories
{
    public class PlayersRepository : IPlayersRepository
    {
        private readonly FootballerStatsDbContext dbContext;

        public PlayersRepository(FootballerStatsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Footballer>> GetAllPlayersAsync()
        {
            return await dbContext.Footballers
        .Include(f => f.MatchStatistics)
        .ToListAsync();
        }

        public async Task<Footballer?> GetPlayerByIdAsync(Guid id)
        {
            return await dbContext.Footballers.FindAsync(id);
        }

        public async Task<Footballer> AddPlayerAsync(Footballer footballer)
        {
            await dbContext.Footballers.AddAsync(footballer);
            await dbContext.SaveChangesAsync();
            return footballer;
        }

        public async Task<Footballer?> UpdatePlayerAsync(Guid id, Footballer updatedFootballer)
        {
            var existing = await dbContext.Footballers.FindAsync(id);
            if (existing == null) return null;

            dbContext.Entry(existing).CurrentValues.SetValues(updatedFootballer);
            await dbContext.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeletePlayerAsync(Guid id)
        {
            var existing = await dbContext.Footballers.FindAsync(id);
            if (existing == null) return false;

            dbContext.Footballers.Remove(existing);
            await dbContext.SaveChangesAsync();
            return true;
        }

    }
}
