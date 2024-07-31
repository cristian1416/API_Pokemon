using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokemon.Domain.Entities;
using Pokemon.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Application.Implementation
{
    public class PokemonService : IPokemon
    {
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ILogger<PokemonService>? _logger;
        public PokemonService(IPokemonRepository pokemonRepository, ILogger<PokemonService>? logger = null)
        {
            _logger = logger;
            _pokemonRepository = pokemonRepository;
        }
        public async Task<ResultPokemon<PokemonResponse>> GetHabilidadesOcultas(string pokemon)
        {
            _logger?.LogInformation($"Obteniendo habilidades ocultas para {pokemon}");

            try
            {
                var infoPokemon = await _pokemonRepository.GetInfoPokemon(pokemon);
                if (infoPokemon == null)
                {
                    _logger?.LogWarning($"No se encontró información para el Pokémon {pokemon}");
                    return new ResultPokemon<PokemonResponse>
                    {
                        StatusCode = 404,
                        ErrorMessage = $"No se encontró información para el Pokémon {pokemon}",
                        Data = null
                    };
                }

                var habilidadesOcultas = infoPokemon.abilities
                                     .Where(a => a.is_hidden)
                                     .Select(a => new Ocultas { name = a.ability.name })
                                     .ToList();
                var pokemonResponse = new PokemonResponse
                {
                    ocultas = habilidadesOcultas
                };
                _logger?.LogInformation("Se encontraron {Count} habilidades ocultas para el Pokémon {PokemonName}", pokemonResponse.ocultas.Count, pokemon);

                return new ResultPokemon<PokemonResponse>
                {
                    StatusCode = 200,                  
                    Data = pokemonResponse
                }; 
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error al obtener habilidades ocultas para el Pokémon {PokemonName}", pokemon);
                return new ResultPokemon<PokemonResponse>
                {
                    StatusCode = 500,
                    ErrorMessage = "Ocurrió un error al procesar la solicitud."
                };
            }
        }
    }
}
