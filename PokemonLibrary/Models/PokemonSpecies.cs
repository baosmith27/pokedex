using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokemonLibrary.Models
{
	public class PokemonSpecies
	{
		public int id;
		public string name;
		public int order;
		public int gender_rate;
		public int capture_rate;
		public int base_happiness;
		public bool is_baby;
		public bool is_legendary;
		public bool is_mythical;
		public int hatch_counter;
		public bool has_gender_differences;
		public bool forms_switchable;
		public List<FlavorTextEntry> flavor_text_entries;
		public Habitat habitat;

		public string GetEnglishDescription()
		{
			return flavor_text_entries.Where(x => x.language.name == "en").FirstOrDefault().flavor_text;
		}
	}

	public class FlavorTextEntry
	{
		public string flavor_text;
		public Language language;
	}

	public class Language
	{
		public string name;
		public string url;
	}

	public class Habitat
	{
		public string name;
		public string url;
	}
}
