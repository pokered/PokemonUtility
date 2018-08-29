using PokemonUtility.Models;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Drawing;

namespace PokemonUtility.ViewModels
{
    public class CaptureWindowViewModel : BindableBase
    {
        private int _captureWidth = 100;
        public int CaptureWidth
        {
            get { return _captureWidth; }
            set { SetProperty(ref _captureWidth, value); }
        }

        private int _captureX = 10;
        public int CaptureX
        {
            get { return _captureX; }
            set { SetProperty(ref _captureX, value); }
        }

        public CaptureWindowViewModel()
        {
        }

        private DelegateCommand closeCommand;
        public DelegateCommand CloseCommand
        {
            get { return closeCommand = closeCommand ?? new DelegateCommand(CloseWindow); }
        }

        public InteractionRequest<INotification> NotificationRequest { get; } = new InteractionRequest<INotification>();

        private void CloseWindow()
        {
            NotificationRequest.Raise(new Notification());
        }
    }
}
