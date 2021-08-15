using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PokemonLibrary.Models;
using PokemonServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PokemonServices.Services
{
	public class TranslateService : ITranslateService
	{
		private ILogger<TranslateService> _logger;
		private readonly IOptions<APISettings> _apiSettings;

		public int LastErrorCode { get; private set; }

		public TranslateService(IOptions<APISettings> apiSettings, ILogger<TranslateService> logger)
		{
			_logger = logger;
			_apiSettings = apiSettings;
		}

		public virtual async Task<Pokemon> Translate(Pokemon pokemon)
		{
			if (pokemon == null)
				throw new ArgumentException("translate - pokemon cannot be null", "pokemon");

			if (pokemon.is_legendary || pokemon.IsCaveDweller())
			{
				pokemon.Description = await GetYodaTranslation(pokemon.Description);
			}
			else
			{
				pokemon.Description = await GetShakespeareTranslation(pokemon.Description);
			}

			return pokemon;
		}

		private async Task<string> GetYodaTranslation(string text)
		{
			return await GetTranslation(text, _apiSettings.Value.yoda_translation_api);
		}

		private async Task<string> GetShakespeareTranslation(string text)
		{
			return await GetTranslation(text, _apiSettings.Value.shakespeare_translation_api);
		}

		public virtual async Task<string> GetTranslation(string text, string url)
		{
			try
			{
				HttpClient client = new HttpClient();
				var response = await client.GetAsync(url + HttpUtility.UrlEncode(CleanText(text)));
				if (response.IsSuccessStatusCode)
				{
					var jsonResponse = await response.Content.ReadAsStringAsync();
					var translation = JsonConvert.DeserializeObject<TranslationServiceReponseModel>(jsonResponse);
					if (translation != null)
					{
						if (translation.Success.total > 0)
						{
							return translation.Contents.Translated;
						}
					}
				}
				else
				{
					var jsonResponse = await response.Content.ReadAsStringAsync();
					var error = JsonConvert.DeserializeObject<TranslateError>(jsonResponse);
					if (error != null)
					{
						LastErrorCode = Convert.ToInt32(error.Error.code);
						_logger.LogError($"{error.Error.code} - {error.Error.message}");
					}
				}
			}
			catch (Exception e)
			{
				_logger.LogError($"TranslateService::GetTranslation - {e.ToString()}");
			}
			return text;
		}

		private string CleanText(string text)
		{
			return text.Replace("\n", " ").Replace("\f", " ");
		}
	}
}
