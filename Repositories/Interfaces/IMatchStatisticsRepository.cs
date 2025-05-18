using FootballerStatsApi.Models;

namespace FootballerStatsApi.Repositories.Interfaces
{
    public interface IMatchStatisticsRepository
    {
        Task<List<MatchStatistic>> GetAllAsync();
        Task<MatchStatistic?> GetByIdAsync(Guid id);
        Task<MatchStatistic> AddAsync(MatchStatistic stat);
        Task<MatchStatistic?> UpdateAsync(Guid id, MatchStatistic stat);
        Task<bool> DeleteAsync(Guid id);
    }
}
