using PokemonUtility.Models;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Drawing;

namespace PokemonUtility.ViewModels
{
    public class CaptureWindowViewModel : BindableBase, IInteractionRequestAware
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

        private void CloseWindow()
        {
            Rectangle test = new Rectangle(110, 110, 300, 110);
            ((CaptureRectangleNotification)Notification).CaptureRectangle = test;
            FinishInteraction();
        }

    }
}
