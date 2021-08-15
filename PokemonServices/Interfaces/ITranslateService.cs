using PokemonLibrary.Models;
using System.Threading.Tasks;

namespace PokemonServices.Interfaces
{
	public interface ITranslateService
	{
		Task<Pokemon> Translate(Pokemon pokemon);
		Task<string> GetTranslation(string text, string url);
	}
}
