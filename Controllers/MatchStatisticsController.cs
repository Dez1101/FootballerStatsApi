using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FootballerStatsApi.Data;
using FootballerStatsApi.Models;
using AutoMapper;
using FootballerStatsApi.Dtos;
using FootballerStatsApi.Repositories.Interfaces;

namespace FootballerStatsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchStatisticsController : ControllerBase
    {
        private readonly IMatchStatisticsRepository repository;
        private readonly IMapper mapper;

        public MatchStatisticsController(IMatchStatisticsRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stats = await repository.GetAllAsync();
            return Ok(mapper.Map<List<MatchStatisticDto>>(stats));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var stat = await repository.GetByIdAsync(id);
            if (stat == null) return NotFound();
                
            return Ok(mapper.Map<MatchStatisticDto>(stat));
        }

        [HttpGet("footballer/{footballerId:guid}")]
        public async Task<IActionResult> GetAllForFootballer(Guid footballerId)
        {
            var stats = await repository.GetAllForFootballerAsync(footballerId);
            if(stats == null) return NotFound();
            return Ok(mapper.Map<List<MatchStatisticDto>>(stats));
        }

        [HttpGet("summary/footballer/{footballerId:guid}")]
        public async Task<IActionResult> GetSummaryForFootballer(Guid footballerId)
        {
            var summary = await repository.GetSummaryForFootballerAsync(footballerId);
            return summary == null ? NotFound() : Ok(summary);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddMatchStatisticDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var stat = mapper.Map<MatchStatistic>(dto);
                var added = await repository.AddAsync(stat);
                return CreatedAtAction(nameof(Get), new { id = added.Id }, mapper.Map<MatchStatisticDto>(added));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the match statistic.");
            }
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMatchStatisticDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedEntity = mapper.Map<MatchStatistic>(dto);
                var updated = await repository.UpdateAsync(id, updatedEntity);
                return updated == null ? NotFound() : Ok(mapper.Map<MatchStatisticDto>(updated));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the match statistic.");
            }
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await repository.DeleteAsync(id);
                return deleted ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the match statistic.");
            }

        }
    }

}
