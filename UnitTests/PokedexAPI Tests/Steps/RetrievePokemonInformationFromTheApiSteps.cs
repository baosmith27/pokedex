﻿using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Pokedex.Controllers;
using PokemonLibrary.Models;
using PokemonServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void WhenIGetPokemonInformationFor(string pokemonName)
        {
            Setupmocks();

            _pokemonCharacterRetrieved = _controller.Pokemon(pokemonName);
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

        private PokemonController CreateController()
		{
            var logger = new Mock<ILogger<PokemonController>>();
            var options = Options.Create(new APISettings() { 
                pokemon_api_url = "https://pokeapi.co/api/v2/pokemon-species/", 
                shakespeare_translation_api = "https://funtranslations/com/api/shakespeare", 
                yoda_translation_api = "https://funtranslations/com/api/yoda"
            });
            return new PokemonController(logger.Object, options, _mockPokemonService.Object);
		}

        private void Setupmocks()
		{            
            _mockPokemonService.Setup(t => t.GetPokemon(It.IsAny<string>())).Returns((string name) => {
                return _testCharacters.Where(x => x.Name == name).FirstOrDefault();
            });
        }
    }
}
