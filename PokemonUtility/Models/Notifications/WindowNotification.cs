using Prism.Interactivity.InteractionRequest;

namespace PokemonUtility.Models.Notifications
{
    class WindowNotification : Notification
    {
        public static readonly int MAIN_WINDOW = 0;
        public static readonly int CAPTURE_WINDOW = 1;
        public static readonly int MY_PARTY_WINDOW = 2;
        public static readonly int OPPONENT_PARTY_WINDOW = 3;
        public static readonly int TODAY_BATTLE_RECORD_WINDOW = 4;
        public static readonly int BATTLE_HISTORY_WINDOW = 5;

        public int WindowId { get; set; }

        public bool IsModal { get; set; } = true;
    }
}
