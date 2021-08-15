using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PokemonLibrary.Models;
using PokemonServices.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PokemonServices
{
	

	public class PokemonService : IPokemonService
	{
		private readonly string _url;
		private ILogger<PokemonService> _logger;
		
		public PokemonService(IOptions<APISettings> apiSettings, ILogger<PokemonService> logger)
		{
			_url = apiSettings.Value.pokemon_api_url;
			_logger = logger;
		}

		public async Task<Pokemon> GetPokemon(string name)
		{
			var pokemonFromAPI = await GetPokemonFromApi(name);

			if (pokemonFromAPI == null)
				return null;
			
			return new Pokemon { Name = name, Description = pokemonFromAPI.GetEnglishDescription(), Habitat = pokemonFromAPI.habitat.name, is_legendary = pokemonFromAPI.is_legendary };
		}		

		private async Task<PokemonSpecies> GetPokemonFromApi(string name)
		{
			try
			{
				HttpClient client = new HttpClient();
				var response = await client.GetAsync(_url + name);
				if (response.IsSuccessStatusCode)
				{
					var jsonResponse = await response.Content.ReadAsStringAsync();
					var pokemonSpecies = JsonConvert.DeserializeObject<PokemonSpecies>(jsonResponse);

					return pokemonSpecies;
				}
				return null;				
			}
			catch(Exception e)
			{
				_logger.LogError($"PokemonService::GetPokemonFromApi - {e.ToString()}");
				return null;
			}
		}

		
	}
}
