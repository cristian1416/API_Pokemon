using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Domain.Entities;
using Pokemon.Domain.Interfaces;

namespace Pokemon.Controllers
{
    [Route("api/")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemon _pokemonService;

        public PokemonController(IPokemon pokemonService)
        {
            _pokemonService = pokemonService;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("pokemon/habilidadesOcultas/{pokemon}")]
        public async Task<ActionResult> GetHabilidadesOcultas(string pokemon)
        {
            var serviceResult = await _pokemonService.GetHabilidadesOcultas(pokemon);

            if (serviceResult.IsSuccess)
            {
                return Ok(serviceResult.Data);
            }
            return StatusCode(serviceResult.StatusCode, serviceResult.ErrorMessage);
        }
    }
}
