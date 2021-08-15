using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
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
		public async Task<ActionResult> Pokemon(string name)
		{
			var pokemon = await _pokemonService.GetPokemon(name);
			if (pokemon == null)
				return NotFound();
			else
				return Ok(pokemon);
		}

		[HttpGet]
		[Route("translated")]
		public async Task<ActionResult> PokemonTranslated(string name)
		{
			var pokemon = _pokemonService.GetPokemon(name);
			if (pokemon == null)
				return NotFound();
			else
				return Ok(pokemon);
		}
		
	}
}
