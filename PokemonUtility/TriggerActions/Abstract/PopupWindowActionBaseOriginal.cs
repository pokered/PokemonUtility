//using Prism.Interactivity.DefaultPopupWindows;
//using Prism.Interactivity.InteractionRequest;
//using System;
//using System.Windows;
//using System.Windows.Interactivity;

//namespace PokemonUtility.TriggerActions
//{
//    /// <summary>
//    /// 独自のView(Window)をInteractionRequestで表示するためのPopupWindowActionを汎用化したもの
//    /// </summary>
//    abstract class PopupWindowActionBase : TriggerAction<FrameworkElement>
//    {
//        #region WindowType
//        public Type WindowType
//        {
//            get { return (Type)GetValue(WindowTypeProperty); }
//            set { SetValue(WindowTypeProperty, value); }
//        }
//        public static readonly DependencyProperty WindowTypeProperty =
//            DependencyProperty.Register("WindowType", typeof(Type), typeof(PopupWindowActionBase), new PropertyMetadata(null));
//        #endregion
//        #region IsModal
//        public bool IsModal
//        {
//            get { return (bool)GetValue(IsModalProperty); }
//            set { SetValue(IsModalProperty, value); }
//        }
//        public static readonly DependencyProperty IsModalProperty =
//            DependencyProperty.Register("IsModal", typeof(bool), typeof(PopupWindowActionBase), new PropertyMetadata(null));
//        #endregion
//        #region CenterOverAssociatedObject
//        public bool CenterOverAssociatedObject
//        {
//            get { return (bool)GetValue(CenterOverAssociatedObjectProperty); }
//            set { SetValue(CenterOverAssociatedObjectProperty, value); }
//        }
//        public static readonly DependencyProperty CenterOverAssociatedObjectProperty =
//            DependencyProperty.Register("CenterOverAssociatedObject", typeof(bool), typeof(PopupWindowActionBase), new PropertyMetadata(null));
//        #endregion
//        #region WindowStartupLocation
//        public WindowStartupLocation? WindowStartupLocation
//        {
//            get { return (WindowStartupLocation?)GetValue(WindowStartupLocationProperty); }
//            set { SetValue(WindowStartupLocationProperty, value); }
//        }
//        public static readonly DependencyProperty WindowStartupLocationProperty =
//            DependencyProperty.Register("WindowStartupLocation", typeof(WindowStartupLocation?), typeof(PopupWindowActionBase), new PropertyMetadata(null));
//        #endregion
//        #region WindowStyle
//        public Style WindowStyle
//        {
//            get { return (Style)GetValue(WindowStyleProperty); }
//            set { SetValue(WindowStyleProperty, value); }
//        }
//        public static readonly DependencyProperty WindowStyleProperty =
//            DependencyProperty.Register("WindowStyle", typeof(Style), typeof(PopupWindowActionBase), new PropertyMetadata(null));
//        #endregion
//        #region Window(読取専用)
//        public Window Window
//        {
//            get { return (Window)GetValue(WindowProperty); }
//            private set { SetValue(WindowPropertyKey, value); }
//        }
//        private static readonly DependencyPropertyKey WindowPropertyKey =
//            DependencyProperty.RegisterReadOnly("Window", typeof(Window), typeof(PopupWindowActionBase), new PropertyMetadata(null));
//        public static readonly DependencyProperty WindowProperty = WindowPropertyKey.DependencyProperty;
//        #endregion

//        protected override void Invoke(object parameter)
//        {
//            if (!(parameter is InteractionRequestedEventArgs args)) return;

//            //Windowを生成する
//            this.Window = this.CreateWindow(args.Context);
//            if (this.Window == null) return;

//            //Notificationの適用
//            this.ApplyNotificationToWindow(this.Window, args.Context);

//            //Windowのオーナーをセット
//            this.SetWindowOwner(this.Window);

//            //Windowへプロパティをセット
//            this.Window.WindowStartupLocation = this.WindowStartupLocation ?? System.Windows.WindowStartupLocation.CenterOwner;

//            //Windowを閉じた時にコールバックを発火
//            var callback = args.Callback;
//            void handler(object o, EventArgs e)
//            {
//                this.Window.Closed -= handler;
//                this.ApplyWindowToNotification(this.Window, args.Context);
//                callback?.Invoke();
//            }

//            this.Window.Closed += handler;
//            //Windowを表示
//            if (this.IsModal) this.Window.ShowDialog();
//            else this.Window.Show();
//        }
//        /// <summary>
//        /// Windowを生成して返す
//        /// </summary>
//        /// <returns></returns>
//        protected virtual Window CreateWindow(INotification notification)
//        {
//            Window window;
//            if (this.WindowType == null)
//            {
//                if (notification == null)
//                {
//                    window = new DefaultWindow() { Width = 300, Height = 150 };
//                }
//                else if (notification is IConfirmation)
//                {
//                    window = new DefaultConfirmationWindow() { Confirmation = (IConfirmation)notification };
//                }
//                else
//                {
//                    window = new DefaultNotificationWindow() { Notification = notification };
//                }
//            }
//            else window = this.WindowType.GetConstructor(Type.EmptyTypes).Invoke(null) as Window;
//            return window;
//        }
//        /// <summary>
//        /// Windowのオーナーをセットする
//        /// </summary>
//        /// <param name="window"></param>
//        protected virtual void SetWindowOwner(Window window)
//        {
//            if (this.AssociatedObject != null) window.Owner = Window.GetWindow(this.AssociatedObject);
//        }
//        /// <summary>
//        /// INotificationで渡された内容をWindowへ適用する
//        /// DataContextを手動で追加したりViewModelの初期値をいじる場合はここで行う
//        /// デフォルトではINotificationのTitleとContentをWindowに適用するようにしている
//        /// Contentが勝手に設定されたりするのが困るなら使わなくてもoverrideしてね
//        /// </summary>
//        /// <param name="window"></param>
//        /// <param name="notification"></param>
//        protected virtual void ApplyNotificationToWindow(Window window, INotification notification)
//        {
//            if (!(window is DefaultConfirmationWindow) && !(window is DefaultNotificationWindow))
//            {
//                if (notification == null) return;
//                window.Title = notification.Title;
//                if (this.WindowType == null) if (notification.Content != null) window.Content = notification.Content;
//            }

//        }
//        /// <summary>
//        /// Windowでの操作結果をNotifictionへ適用する
//        /// Window.DataContextからViewModelを取得して値の取り出しを行う
//        /// </summary>
//        /// <param name="windown"></param>
//        /// <param name="notification"></param>
//        protected virtual void ApplyWindowToNotification(Window windown, INotification notification)
//        {
//        }
//    }
//}
