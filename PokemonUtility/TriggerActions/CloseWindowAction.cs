using System.Windows;
using System.Windows.Interactivity;

namespace PokemonUtility.TriggerActions
{
    class CloseWindowAction : TriggerAction<Window>
    {
        /// <summary>Windowを「閉じる」アクションクラス</summary>
        protected override void Invoke(object parameter)
        {
            DependencyPropertyChangedEventArgs iswin = (DependencyPropertyChangedEventArgs)parameter;

            if ((bool)iswin.NewValue == false)
            {
                AssociatedObject.Close();
            }
        }
    }
}
