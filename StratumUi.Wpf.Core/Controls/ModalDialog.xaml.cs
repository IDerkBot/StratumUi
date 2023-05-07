using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;
using StratumUi.Wpf.Core.Controls.Enums;

namespace StratumUi.Wpf.Core.Controls
{
    public partial class ModalDialog
    {
        private static ModalDialog _modalDialog;
        private ModalDialogResult _result;
        public ModalDialog(string message, string caption, ModalDialogButtons buttons, ModalDialogType type)
        {
            InitializeComponent();

            Message = message;
            Caption = caption;
            Buttons = buttons;
            Type = type;
        }

        #region Variables

        public EIcons ModalIcon
        {
            get => (EIcons)GetValue(ModalIconProperty);
            set => SetValue(ModalIconProperty, value);
        }

        public static readonly DependencyProperty ModalIconProperty =
            DependencyProperty.Register(nameof(ModalIcon), typeof(EIcons), typeof(ModalDialog), new PropertyMetadata());

        public SolidColorBrush ModalIconColor
        {
            get => (SolidColorBrush)GetValue(ModalIconColorProperty);
            set => SetValue(ModalIconColorProperty, value);
        }

        public static readonly DependencyProperty ModalIconColorProperty =
            DependencyProperty.Register(nameof(ModalIconColor), typeof(SolidColorBrush), typeof(ModalDialog), new PropertyMetadata());

        public Visibility ModalIconVisibility
        {
            get => (Visibility)GetValue(ModalIconVisibilityProperty);
            set => SetValue(ModalIconVisibilityProperty, value);
        }

        public static readonly DependencyProperty ModalIconVisibilityProperty =
            DependencyProperty.Register(nameof(ModalIconVisibility), typeof(Visibility), typeof(ModalDialog), new PropertyMetadata(Visibility.Visible));
        
        public string Caption
        {
            get => (string)GetValue(CaptionProperty);
            set => SetValue(CaptionProperty, value);
        }

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register(nameof(Caption), typeof(string), typeof(ModalDialog), new PropertyMetadata("Caption"));

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register(nameof(Message), typeof(string), typeof(ModalDialog), new PropertyMetadata("Message"));

        public ModalDialogType Type
        {
            get => (ModalDialogType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register(nameof(Type), typeof(ModalDialogType), typeof(ModalDialog), new PropertyMetadata());

        public ModalDialogButtons Buttons
        {
            get => (ModalDialogButtons)GetValue(ButtonsProperty);
            set => SetValue(ButtonsProperty, value);
        }

        public static readonly DependencyProperty ButtonsProperty =
            DependencyProperty.Register(nameof(Buttons), typeof(ModalDialogButtons), typeof(ModalDialog), new PropertyMetadata());

        public static readonly DependencyProperty VisibleOkProperty = DependencyProperty.Register(
            nameof(VisibleOk), typeof(Visibility), typeof(ModalDialog), new PropertyMetadata(default(Visibility)));

        public Visibility VisibleOk
        {
            get => (Visibility)GetValue(VisibleOkProperty);
            set => SetValue(VisibleOkProperty, value);
        }

        public static readonly DependencyProperty VisibleYesProperty = DependencyProperty.Register(
            nameof(VisibleYes), typeof(Visibility), typeof(ModalDialog), new PropertyMetadata(default(Visibility)));

        public Visibility VisibleYes
        {
            get => (Visibility)GetValue(VisibleYesProperty);
            set => SetValue(VisibleYesProperty, value);
        }

        public static readonly DependencyProperty VisibleNoProperty = DependencyProperty.Register(
            nameof(VisibleNo), typeof(Visibility), typeof(ModalDialog), new PropertyMetadata(default(Visibility)));

        public Visibility VisibleNo
        {
            get => (Visibility)GetValue(VisibleNoProperty);
            set => SetValue(VisibleNoProperty, value);
        }

        public static readonly DependencyProperty VisibleCancelProperty = DependencyProperty.Register(
            nameof(VisibleCancel), typeof(Visibility), typeof(ModalDialog), new PropertyMetadata(default(Visibility)));

        public Visibility VisibleCancel
        {
            get => (Visibility)GetValue(VisibleCancelProperty);
            set => SetValue(VisibleCancelProperty, value);
        }

        #endregion
        
        private static bool CheckExist() => _modalDialog != null;

        public static ModalDialogResult Show( string caption, string message, ModalDialogButtons buttons, ModalDialogType type,
            Window windowForBlur = null)
        {
            if (CheckExist()) return ModalDialogResult.Cancel;
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                _modalDialog = new ModalDialog(message, caption, buttons, type);
                if (windowForBlur != null) windowForBlur.Effect = new BlurEffect { Radius = 3 };
                _modalDialog.ShowDialog();
                if (windowForBlur != null) windowForBlur.Effect = new BlurEffect { Radius = 0 };
            });

            var result = _modalDialog._result;
            _modalDialog = null;
            return result;
        }
        
        public static ModalDialogResult Show(string caption, string message = "")
        {
            if (CheckExist()) return ModalDialogResult.Cancel;
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                _modalDialog = new ModalDialog(message, caption, ModalDialogButtons.Ok, ModalDialogType.Info);
                _modalDialog.ShowDialog();
            });

            var result = _modalDialog._result;
            _modalDialog = null;
            return result;
        }
        
        public static ModalDialogResult Show(string caption, string message, ModalDialogButtons buttons)
        {
            if (CheckExist()) return ModalDialogResult.Cancel;
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                _modalDialog = new ModalDialog(message, caption, buttons, ModalDialogType.Info);
                _modalDialog.ShowDialog();
            });

            var result = _modalDialog._result;
            _modalDialog = null;
            return result;
        }
        
        public static ModalDialogResult Show(string caption, string message, ModalDialogType type)
        {
            if (CheckExist()) return ModalDialogResult.Cancel;
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                _modalDialog = new ModalDialog(message, caption, ModalDialogButtons.Ok, type);
                _modalDialog.ShowDialog();
            });

            var result = _modalDialog._result;
            _modalDialog = null;
            return result;
        }

        #region Methods

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            _result = ModalDialogResult.Cancel;
            Close();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            _result = ModalDialogResult.Ok;
            Close();
        }

        private void BtnYes_OnClick(object sender, RoutedEventArgs e)
        {
            _result = ModalDialogResult.Yes;
            Close();
        }

        private void BtnNo_OnClick(object sender, RoutedEventArgs e)
        {
            _result = ModalDialogResult.No;
            Close();
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            _result = ModalDialogResult.Cancel;
            Close();
        }

        public static void Shutdown()
        {
            if (_modalDialog != null)
            {
                _modalDialog._result = ModalDialogResult.Ok;
                _modalDialog.Close();
            }
        }
        
        #endregion
    }
}