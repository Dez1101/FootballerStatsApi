using FootballerStatsApi.Models;

namespace FootballerStatsApi.Repositories.Interfaces
{
    public interface IPlayersRepository
    {
        Task<List<Footballer>> GetAllPlayersAsync();
        Task<Footballer?> GetPlayerByIdAsync(Guid id);
        Task<Footballer> AddPlayerAsync(Footballer footballer);
        Task<Footballer?> UpdatePlayerAsync(Guid id, Footballer updatedFootballer);
        Task<bool> DeletePlayerAsync(Guid id);
    }

}
