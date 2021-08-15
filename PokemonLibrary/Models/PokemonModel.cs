﻿namespace PokemonLibrary.Models
{
	public class Pokemon
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string Habitat { get; set; }
		public bool is_legendary { get; set; }

		public bool IsCaveDweller()
		{
			return Habitat == Enums.C_CAVE_DWELLING_NAME;
		}
	}
}
