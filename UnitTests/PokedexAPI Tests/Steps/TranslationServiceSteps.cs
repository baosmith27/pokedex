using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PokemonLibrary.Models;
using PokemonServices.Interfaces;
using PokemonServices.Services;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace UnitTests.PokedexAPI_Tests.Steps
{
    [Binding]
    public class TranslationServiceSteps
    {
        private ITranslateService _translationService;
        private string _translatedText;
        private Pokemon pokemonCharacterToTranslate;

        public TranslationServiceSteps()
		{
            pokemonCharacterToTranslate = new Pokemon();
            _translationService = CreateService();
        }

        [Given(@"I am using the Shakespeare translation service")]
        public void GivenIAmUsingTheShakespeareTranslationService()
        {
            pokemonCharacterToTranslate.is_legendary = false;
        }

        [Given(@"I am using the Yoda translation service")]
        public void GivenIAmUsingTheYodaTranslationService()
        {
            pokemonCharacterToTranslate.is_legendary = true;
        }

        [Given(@"the translation api service is down")]
        public void GivenTheTranslationApiServiceIsDown()
        {
            _translationService = CreateService(true);
        }

        [When(@"I translate '(.*)'")]
        public async Task WhenITranslate(string text)
        {
            pokemonCharacterToTranslate.Description = text;
            _translatedText = (await _translationService.Translate(pokemonCharacterToTranslate)).Description;
        }
        
        [Then(@"the text should be translated as '(.*)'")]
        public void ThenTheTextShouldBeTranslatedAs(string expectedText)
        {
            Assert.Equal(expectedText, _translatedText);
        }

        private ITranslateService CreateService(bool mimicApiDown = false)
		{
            var options = Options.Create(new APISettings()
            {
                pokemon_api_url = mimicApiDown ? "https://fakeapi" : "https://pokeapi.co/api/v2/pokemon-species/",
                shakespeare_translation_api = mimicApiDown ? "https://fakeapi" : "https://api.funtranslations.com/translate/shakespeare.json?text=",
                yoda_translation_api = mimicApiDown ? "https://fakeapi" : "https://api.funtranslations.com/translate/yoda.json?text="
            });

            return new TranslateService(options, new Mock<ILogger<TranslateService>>().Object);            
        }
    }
}
