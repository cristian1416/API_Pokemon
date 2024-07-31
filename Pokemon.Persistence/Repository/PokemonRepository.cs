using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pokemon.Domain.Entities;
using Pokemon.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Pokemon.Persistence.Repository
{
    public class PokemonRepository : IPokemonRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public PokemonRepository(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public async Task<InfoPokemonResponse> GetInfoPokemon(string pokemon)
        {
            var url = _configuration["PokemonApi:Url"].Replace("{namePokemon}", pokemon);
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var infoPokemon = JsonConvert.DeserializeObject<InfoPokemonResponse>(json);
                return infoPokemon;
            }
            else
            {
                return null;
            }
        }
    }
}
