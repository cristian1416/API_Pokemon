using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Pokemon.Application.Implementation;
using Pokemon.Controllers;
using Pokemon.Domain.Entities;
using Pokemon.Domain.Interfaces;
using Pokemon.Persistence.Repository;
using System.Security.Cryptography.X509Certificates;

namespace PokemonTest
{
    [TestClass]
    public class UnitTestPokemon
    {
        private readonly IPokemon _pokemonService;
        private readonly HttpClient _httpClient;
        private readonly IPokemonRepository _pokemonRepository;
        private readonly ILogger<PokemonService> _logger;
        private readonly PokemonController _controller;

        public UnitTestPokemon()
        {
            _httpClient = new HttpClient();
            _pokemonRepository = new PokemonRepository(_httpClient);
            _pokemonService = new PokemonService(_pokemonRepository, _logger);
            _controller = new PokemonController(_pokemonService);
        }
        [TestMethod]
        public async Task TestGetHabilidadesOcultasSuccess()
        {
            // Arrange
            var pokemonName = "pikachu";
            var expectedResponse = new PokemonResponse
            {
                ocultas = new List<Ocultas>
        {
            new Ocultas { name = "lightning-rod" },
        }
            };

            var result = await _controller.GetHabilidadesOcultas(pokemonName) as OkObjectResult;
            Assert.IsNotNull(result, "El resultado no debe ser null.");
            Assert.AreEqual(200, result.StatusCode, "El código de estado no es 200 OK.");

            var response = result.Value as PokemonResponse;
            Assert.IsNotNull(response, "El contenido de la respuesta no debe ser null.");
            Assert.AreEqual(expectedResponse.ocultas.Count, response.ocultas.Count, "El conteo de habilidades ocultas no coincide.");
            for (int i = 0; i < expectedResponse.ocultas.Count; i++)
            {
                Assert.AreEqual(expectedResponse.ocultas[i].name, response.ocultas[i].name, $"El nombre de la habilidad oculta en la posición {i} no coincide.");
            }
        }

        [TestMethod]
        public async Task TestGetHabilidadesOcultasFailed()
        {
            var pokemonName = "pikachu";
            var expectedResponse = new PokemonResponse
            {
                ocultas = new List<Ocultas>
        {
            new Ocultas { name = "lightning-rod1" },
        }
            };

            var result = await _controller.GetHabilidadesOcultas(pokemonName) as OkObjectResult;
            Assert.IsNotNull(result, "El resultado no debe ser null.");
            Assert.AreEqual(200, result.StatusCode, "El código de estado no es 200 OK.");
            var response = result.Value as PokemonResponse;
            Assert.IsNotNull(response, "El contenido de la respuesta no debe ser null.");
            Assert.AreEqual(expectedResponse.ocultas.Count, response.ocultas.Count, "El conteo de habilidades ocultas no coincide.");
            for (int i = 0; i < expectedResponse.ocultas.Count; i++)
            {
                Assert.AreEqual(expectedResponse.ocultas[i].name, response.ocultas[i].name, $"El nombre de la habilidad oculta en la posición {i} no coincide.");
            }
        }
    }
}