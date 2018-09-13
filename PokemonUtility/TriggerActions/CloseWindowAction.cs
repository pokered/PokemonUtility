using System.Windows;
using System.Windows.Interactivity;

namespace PokemonUtility.TriggerActions
{
    class CloseWindowAction : TriggerAction<Window>
    {
        /// <summary>Windowを「閉じる」アクションクラス</summary>
        protected override void Invoke(object parameter)
        {
            DependencyPropertyChangedEventArgs property = (DependencyPropertyChangedEventArgs)parameter;
            
            if (!(bool)property.NewValue)
            {
                AssociatedObject.Close();
            }
        }
    }
}
