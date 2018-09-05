using PokemonUtility.Models;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;

namespace PokemonUtility.ViewModels
{
    public class CaptureWindowViewModel : BindableBase
    {
        private int _x;
        public int X
        {
            get { return _x; }
            set { SetProperty(ref _x, value); }
        }

        private int _y;
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        private int _width;
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private int _height;
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public CaptureWindowViewModel()
        {
        }

        private DelegateCommand closeCommand;
        public DelegateCommand CloseCommand
        {
            get { return closeCommand = closeCommand ?? new DelegateCommand(CloseWindow); }
        }

        public InteractionRequest<RectangleNotification> NotificationRequest { get; } = new InteractionRequest<RectangleNotification>();

        private void CloseWindow()
        {
            NotificationRequest.Raise(null);
        }
    }
}
