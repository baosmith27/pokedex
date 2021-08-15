using PokemonLibrary.Models;

namespace PokemonServices.Interfaces
{
	public interface IPokemonService
	{
		Pokemon GetPokemon(string name);
	}
}
