using PokemonUtility.Models;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Linq;

namespace PokemonUtility.ViewModels
{
    public class MyPartyWindowViewModel : BindableBase
    {
        public ReactiveProperty<bool> IsShowWindow { get; private set; }
        public ReactiveProperty<string> Mess { get; private set; }

        // ReactiveCommand
        public ReactiveCommand WindowCloseCommand { get; }

        // モデル
        private MyPartyWindowModel model;

        // リクエスト
        public InteractionRequest<INotification> CloseWindowRequest { get; } = new InteractionRequest<INotification>();

        public MyPartyWindowViewModel()
        {
            model = MyPartyWindowModel.GetInstance();

            Mess = model.ObserveProperty(m => m.Mess).ToReactiveProperty();
            IsShowWindow = model.ObserveProperty(m => m.IsShowWindow).ToReactiveProperty();

            //WindowCloseCommand = model.ObserveProperty(m => m.IsShowWindow).ToReactiveCommand();
            //WindowCloseCommand.Subscribe(CloseWindowCommandExecute);
        }
    }
}
