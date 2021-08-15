using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PokemonLibrary.Models;
using PokemonServices;
using PokemonServices.Interfaces;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace UnitTests.PokedexAPI_Tests.Steps
{
    [Binding]
    public class PokemonServiceSteps
    {
        private Pokemon _pokemonFound;
        private IPokemonService _pokemonService;
        
        public PokemonServiceSteps()
		{
            _pokemonService = CreateService();            
        }

        [Given(@"the pokemon api service is down")]
        public void GivenThePokemonApiServiceIsDown()
        {
            _pokemonService = CreateService(true);
        }


        [When(@"I retrieve pokemon '(.*)'")]
        public async Task WhenIRetrievePokemon(string name)
        {            
            _pokemonFound = await _pokemonService.GetPokemon(name);           
        }
        
        [Then(@"the pokemon should be found")]
        public void ThenThePokemonShouldBeFound()
        {
            Assert.NotNull(_pokemonFound);
        }

        [Then(@"the pokemon should not be found")]
        public void ThenThePokemonShouldNotBeFound()
        {
            Assert.Null(_pokemonFound);
        }


        [Then(@"the name should be '(.*)'")]
        public void ThenTheNameShouldBe(string expectedName)
        {
            Assert.Equal(expectedName, _pokemonFound.Name);
        }

        private PokemonService CreateService(bool mimicApiDown = false)
		{
            var logger = new Mock<ILogger<PokemonService>>();
            var options = Options.Create(new APISettings()
            {
                pokemon_api_url = mimicApiDown ?  "https://fakeapi" : "https://pokeapi.co/api/v2/pokemon-species/",
                shakespeare_translation_api = mimicApiDown ? "https://fakeapi" : "https://api.funtranslations.com/translate/shakespeare.json?text=",
                yoda_translation_api = mimicApiDown ? "https://fakeapi" : "https://api.funtranslations.com/translate/yoda.json?text="
            });
            
            return new PokemonService(options, logger.Object);
        }        
    }
}
