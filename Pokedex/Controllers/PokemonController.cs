using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PokemonLibrary.Models;
using PokemonServices.Interfaces;

namespace Pokedex.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PokemonController : ControllerBase
	{		
		private readonly ILogger<PokemonController> _logger;
		private IOptions<APISettings> _apiSettings;
		private readonly IPokemonService _pokemonService;

		public PokemonController(ILogger<PokemonController> logger, IOptions<APISettings> apiSettings, IPokemonService pokemonService)
		{
			_logger = logger;
			_apiSettings = apiSettings;
			_pokemonService = pokemonService;
		}

		[HttpGet]
		public Pokemon Pokemon(string name)
		{
			return GetPokemon(name);
		}

		[HttpGet]
		[Route("translated")]
		public Pokemon PokemonTranslated(string name)
		{
			var pokemon = GetPokemon(name);

			return pokemon;
		}

		private Pokemon GetPokemon(string name)
		{
			return _pokemonService.GetPokemon(name);
		}
	}
}
