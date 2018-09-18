using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Threading;

namespace PokemonUtility.TriggerActions
{
    class WindowReDisplayAction : TriggerAction<Window>
    {
        /// <summary>Windowを「閉じる」アクションクラス</summary>
        protected override void Invoke(object parameter)
        {
            AssociatedObject.UpdateLayout();
        }
    }
}
