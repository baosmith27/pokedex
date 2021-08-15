using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Pokedex.Controllers;
using PokemonLibrary.Models;
using PokemonServices.Interfaces;
using PokemonServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace UnitTests.PokedexAPI_Tests.Steps
{
    [Binding]
    public class RetrievePokemonInformationFromTheApiSteps
    {
        private Pokemon _pokemonCharacterRetrieved;
        private PokemonController _controller;
        private List<Pokemon> _testCharacters;
        private Mock<IPokemonService> _mockPokemonService;
        private int _controllerStatusCode;
        private Mock<TranslateService> _mockTranslateService;
        private IOptions<APISettings> _options;
        
        public RetrievePokemonInformationFromTheApiSteps()
		{            
            _testCharacters = new List<Pokemon>();
            _mockPokemonService = new Mock<IPokemonService>();            

            _controller = CreateController();
        }

        [Given(@"I have a pokemon ""(.*)"", ""(.*)"", ""(.*)"", is_legendary ""(.*)""")]
        public void GivenIHaveAPokemonIs_Legendary(string name, string description, string habitat, bool is_legendary)
        {
            _testCharacters.Add(new Pokemon { 
                Name = name, 
                Description = description, 
                Habitat = habitat,
                is_legendary = is_legendary
            });
        }

        [When(@"I get pokemon information for '(.*)'")]
        public async Task WhenIGetPokemonInformationFor(string pokemonName)
        {
            Setupmocks();

            var response = await _controller.Pokemon(pokemonName);            
            
            var statusCodeResult = (response as StatusCodeResult);
            if(statusCodeResult != null) 
                _controllerStatusCode = statusCodeResult.StatusCode;
            
            var result = (response as OkObjectResult);
            if(result != null)
                _pokemonCharacterRetrieved = (result.Value as Pokemon);
        }

        [When(@"I get pokemon translated information for '(.*)'")]
        public async Task WhenIGetPokemonTranslatedInformationFor(string pokemonName)
        {
            Setupmocks();

            var response = await _controller.PokemonTranslated(pokemonName);
            ParseControllerResult(response);
        }

        [Then(@"the description should be '(.*)'")]
        public void ThenTheDescriptionShouldBe(string expectedDescription)
        {
            Assert.Equal(expectedDescription, _pokemonCharacterRetrieved.Description);
        }
        
        [Then(@"is_legendary is '(.*)'")]
        public void ThenIs_LegendaryIs(bool expectedIsLegendaryStatus)
        {
            Assert.Equal(expectedIsLegendaryStatus, _pokemonCharacterRetrieved.is_legendary);
        }

        [Then(@"the return code should be '(.*)'")]
        public void ThenTheReturnCodeShouldBe(int expectedStatusCode)
        {
            Assert.Equal(expectedStatusCode, _controllerStatusCode);
        }

        [Then(@"the description should be '(.*)' translated")]
        public void ThenTheDescriptionShouldBeTranslated(string translator)
        {
            if (translator == "yoda")
                _mockTranslateService.Verify(t => t.GetTranslation(It.IsAny<String>(), It.Is<string>(s => s == _options.Value.yoda_translation_api)), Times.Once);
            else if (translator == "shakespeare")
                _mockTranslateService.Verify(t => t.GetTranslation(It.IsAny<String>(), It.Is<string>(s => s == _options.Value.shakespeare_translation_api)), Times.Once);
        }

        private PokemonController CreateController()
		{
            var logger = new Mock<ILogger<PokemonController>>();
            _options = Options.Create(new APISettings() { 
                pokemon_api_url = "https://pokeapi.co/api/v2/pokemon-species/",
                shakespeare_translation_api = "https://api.funtranslations.com/translate/shakespeare.json?text=",
                yoda_translation_api = "https://api.funtranslations.com/translate/yoda.json?text="
            });

            _mockTranslateService = new Mock<TranslateService>(new object[] { _options, new Mock<ILogger<TranslateService>>().Object } );

            return new PokemonController(logger.Object, _options, _mockPokemonService.Object, _mockTranslateService.Object);
		}

        private void Setupmocks()
		{            
            _mockPokemonService.Setup(t => t.GetPokemon(It.IsAny<string>())).ReturnsAsync((string name) => {
                return _testCharacters.Where(x => x.Name == name).FirstOrDefault();
            });

            _mockTranslateService.Setup(t => t.Translate(It.IsAny<Pokemon>())).CallBase();            
        }

        private void ParseControllerResult(ActionResult response)
        {
            var statusCodeResult = (response as StatusCodeResult);
            if (statusCodeResult != null)
                _controllerStatusCode = statusCodeResult.StatusCode;

            var result = (response as OkObjectResult);
            if (result != null)
                _pokemonCharacterRetrieved = (result.Value as Pokemon);
        }
    }
}
