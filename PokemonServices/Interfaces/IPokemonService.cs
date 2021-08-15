using PokemonLibrary.Models;
using System.Threading.Tasks;

namespace PokemonServices.Interfaces
{
	public interface IPokemonService
	{
		Task<Pokemon> GetPokemon(string name);
	}
}
