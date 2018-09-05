﻿using PokemonUtility.Models;
using PokemonUtility.Views;
using Prism.Interactivity.InteractionRequest;
using System.Windows;

namespace PokemonUtility
{
    class SampleAction : PopupWindowActionBase
    {
        protected override Window CreateWindow(INotification notification)
        {
            return new CaptureWindow() {};
        }

        protected override void ApplyWindowToNotification(Window windown, INotification notification)
        {
            ((RectangleNotification)notification).X = (int)windown.Left;
        }
    }
}
