using Microsoft.Extensions.Options;
using PokemonLibrary.Models;
using PokemonServices.Interfaces;
using System;

namespace PokemonServices
{
	

	public class PokemonService : IPokemonService
	{
		private readonly string _url;

		public PokemonService(IOptions<APISettings> apiSettings)
		{
			_url = apiSettings.Value.pokemon_api_url;
		}

		public Pokemon GetPokemon(string name)
		{
			var pokemonFromAPI = GetPokemonFromApi(name);

			return new Pokemon { Name = name, Description = pokemonFromAPI.Description, Habitat = pokemonFromAPI.Habitat, is_legendary = pokemonFromAPI.is_legendary };
		}

		private Pokemon GetPokemonFromApi(string name)
		{
			return new Pokemon();
		}
	}
}
