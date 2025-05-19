using AutoMapper;
using FootballerStatsApi.Dtos;
using FootballerStatsApi.Models;
using FootballerStatsApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FootballerStatsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersRepository playersRepository;
        private readonly IMapper mapper;

        public PlayersController(IPlayersRepository playersRepository, IMapper mapper)
        {
            this.playersRepository = playersRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlayers()
        {
            try
            {
                var players = await playersRepository.GetAllPlayersAsync();
                return Ok(mapper.Map<List<FootballerBasicDto>>(players));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPlayer(Guid id)
        {
            try
            {
                var player = await playersRepository.GetPlayerByIdAsync(id);
                return player == null ? NotFound() : Ok(mapper.Map<FootballerBasicDto>(player));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPlayer([FromBody] AddPlayerDto addPlayerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var footballer = mapper.Map<Footballer>(addPlayerDto);
                var added = await playersRepository.AddPlayerAsync(footballer);

                return CreatedAtAction(nameof(GetPlayer), new { id = added.Id }, mapper.Map<FootballerDto>(added));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePlayer(Guid id, [FromBody] UpdatePlayerDto updatePlayerDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var updatedFootballer = mapper.Map<Footballer>(updatePlayerDto);
                var updated = await playersRepository.UpdatePlayerAsync(id, updatedFootballer);

                return updated == null ? NotFound() : Ok(mapper.Map<FootballerDto>(updated));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePlayer(Guid id)
        {
            try
            {
                var deleted = await playersRepository.DeletePlayerAsync(id);
                return deleted ? NoContent() : NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
