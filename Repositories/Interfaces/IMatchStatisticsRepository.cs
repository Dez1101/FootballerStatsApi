﻿using FootballerStatsApi.Dtos;
using FootballerStatsApi.Models;

namespace FootballerStatsApi.Repositories.Interfaces
{
    public interface IMatchStatisticsRepository
    {
        Task<List<MatchStatistic>> GetAllAsync();
        Task<List<MatchStatistic>> GetAllForFootballerAsync(Guid footballerId);
        Task<MatchStatistic?> GetByIdAsync(Guid id);
        Task<FootballerStatsSummaryDto?> GetSummaryForFootballerAsync(Guid footballerId);
        Task<MatchStatistic> AddAsync(MatchStatistic stat);
        Task<MatchStatistic?> UpdateAsync(Guid id, MatchStatistic stat);
        Task<bool> DeleteAsync(Guid id);
    }
}
