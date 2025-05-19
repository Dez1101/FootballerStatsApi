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
            try
            {
                await dbContext.Footballers.AddAsync(footballer);
                await dbContext.SaveChangesAsync();
                return footballer;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the player.", ex);
            }
        }

        public async Task<Footballer?> UpdatePlayerAsync(Guid id, Footballer updatedFootballer)
        {
            try
            {
                var existing = await dbContext.Footballers.FindAsync(id);
                if (existing == null) return null;

                dbContext.Entry(existing).CurrentValues.SetValues(updatedFootballer);
                await dbContext.SaveChangesAsync();
                return existing;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the player.", ex);
            }
        }

        public async Task<bool> DeletePlayerAsync(Guid id)
        {
            try
            {
                var existing = await dbContext.Footballers.FindAsync(id);
                if (existing == null) return false;

                dbContext.Footballers.Remove(existing);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the player.", ex);
            }
        }
    }
}
