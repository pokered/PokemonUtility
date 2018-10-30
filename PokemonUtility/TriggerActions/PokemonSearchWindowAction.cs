using PokemonUtility.Models.Notifications;
using PokemonUtility.ViewModels;
using PokemonUtility.Views;
using Prism.Interactivity.InteractionRequest;
using System.Windows;

namespace PokemonUtility.TriggerActions
{
    class PokemonSearchWindowAction : PopupWindowActionBase
    {
        protected override Window CreateWindow(INotification notification)
        {
            PokemonSearchWindowNotification pokemonSearchWindowNotification = (PokemonSearchWindowNotification)notification;

            // モーダル
            IsModal = pokemonSearchWindowNotification.IsModal;

            // 生成するウィンドウ
            if (pokemonSearchWindowNotification.WindowId == PokemonSearchWindowNotification.POKEMON_SEARCH_WINDOW) return new PokemonSearchWindow();

            //TODO エラー画面を表示する？
            return new Window();
        }

        protected override void ApplyNotificationToWindow(Window window, INotification notification)
        {
            PokemonSearchWindowViewModel content = (PokemonSearchWindowViewModel)window.DataContext;
            PokemonSearchWindowNotification pokemonSearchWindowNotification = (PokemonSearchWindowNotification)notification;

            content.PokemonSearchWindowModel.PokemonId = pokemonSearchWindowNotification.PokemonId;
        }

        protected override void ApplyWindowToNotification(Window windown, INotification notification)
        {
            PokemonSearchWindowViewModel content = (PokemonSearchWindowViewModel)windown.DataContext;
            PokemonSearchWindowNotification pokemonSearchWindowNotification = (PokemonSearchWindowNotification)notification;

            if (content.PokemonSearchWindowModel.IsChangePokemonId)
            {
                pokemonSearchWindowNotification.PokemonId = content.PokemonSearchWindowModel.PokemonId;
            }
        }
    }
}
