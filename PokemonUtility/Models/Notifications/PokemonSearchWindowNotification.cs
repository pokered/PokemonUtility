namespace PokemonUtility.Models.Notifications
{
    class PokemonSearchWindowNotification : WindowNotification
    {
        public static readonly int POKEMON_SEARCH_WINDOW = 6;

        public int PokemonId { get; set; } = -1;
    }
}
