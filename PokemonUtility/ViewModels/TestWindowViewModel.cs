using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PokemonUtility.ViewModels
{
    public class TestWindowViewModel : BindableBase, IInteractionRequestAware
    {
        // IInteractionRequestAware
        public Action FinishInteraction { get; set; }
        private INotification notification;
        public INotification Notification
        {
            get { return notification; }
            set { notification = value; }
        }

        private int _captureWidth = 600;
        public int CaptureWidth
        {
            get { return _captureWidth; }
            set { SetProperty(ref _captureWidth, value); }
        }

        public TestWindowViewModel()
        {

        }
    }
}
