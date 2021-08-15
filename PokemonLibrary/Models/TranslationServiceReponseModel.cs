
namespace PokemonLibrary.Models
{
	public class TranslationServiceReponseModel
	{
		public Success Success;
		public Contents Contents;
	}

	public class Success
	{
		public int total;
	}

	public class Contents
	{
		public string Translated;
		public string Text;
		public string Translation;
	}

	public class Error
	{
		public string Code;
		public string Message;
	}
}
