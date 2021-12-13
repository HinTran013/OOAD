using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DigitalPhotographyManagementSystem.View
{
    /// <summary>
    /// Interaction logic for MsgBox.xaml
    /// </summary>
    public enum MessageBoxTyp
    {
        ConfirmationWithYesNo = 0,
        ConfirmationWithYesNoCancel,
        Information,
        Error,
        Warning
    }

    public enum MessageBoxImg
    {
        Warning = 0,
        Question,
        Information,
        Error,
        None
    }
    public partial class MsgBox : Window
    {
        private MsgBox()
        {
            InitializeComponent();
        }

        static MsgBox _messageBox;
        public static string Value
        {
            set; get;
        }
        static MessageBoxResult _result = MessageBoxResult.No;
        public static MessageBoxResult Show(string caption, string msg, MessageBoxTyp type)
        {
            switch (type)
            {
                case MessageBoxTyp.ConfirmationWithYesNo:
                    return Show(caption, msg, MessageBoxButton.YesNo,
                    MessageBoxImg.Question);
                case MessageBoxTyp.ConfirmationWithYesNoCancel:
                    return Show(caption, msg, MessageBoxButton.YesNoCancel,
                    MessageBoxImg.Question);
                case MessageBoxTyp.Information:
                    return Show(caption, msg, MessageBoxButton.OK,
                    MessageBoxImg.Information);
                case MessageBoxTyp.Error:
                    return Show(caption, msg, MessageBoxButton.OK,
                    MessageBoxImg.Error);
                case MessageBoxTyp.Warning:
                    return Show(caption, msg, MessageBoxButton.OK,
                    MessageBoxImg.Warning);
                default:
                    return MessageBoxResult.No;
            }
        }

        public static MessageBoxResult Show(string caption, string hint)
        {
            _messageBox = new MsgBox { MessageTitle = { Text = caption } };
            MaterialDesignThemes.Wpf.HintAssist.SetHint(_messageBox.ValueTxt, hint);
            SetVisibilityOfButtons(MessageBoxButton.OKCancel);
            SetImageOfMessageBox(MessageBoxImg.Information);
            _messageBox.ValueTxt.Focus();
            _messageBox.ShowDialog();
            return _result;
        }

        public static MessageBoxResult Show(string msg, MessageBoxTyp type)
        {
            return Show(string.Empty, msg, type);
        }
        public static MessageBoxResult Show(string msg)
        {
            return Show(string.Empty, msg,
            MessageBoxButton.OK, MessageBoxImg.None);
        }
        /*public static MessageBoxResult Show (string caption, string text)
        {
            return Show(caption, text,
            MessageBoxButton.OK, MessageBoxImg.None);
        }*/
        public static MessageBoxResult Show(string caption, string text, MessageBoxButton button)
        {
            return Show(caption, text, button,
            MessageBoxImg.None);
        }
        public static MessageBoxResult Show(string caption, string text, MessageBoxButton button, MessageBoxImg image)
        {
            _messageBox = new MsgBox
            { txtMsg = { Text = text }, MessageTitle = { Text = caption } };
            _messageBox.ValueBd.Visibility = Visibility.Hidden;
            SetVisibilityOfButtons(button);
            SetImageOfMessageBox(image);
            _messageBox.ShowDialog();
            return _result;
        }
        private static void SetVisibilityOfButtons(MessageBoxButton button)
        {
            switch (button)
            {
                case MessageBoxButton.OK:
                    _messageBox.btnCancel.Visibility = Visibility.Collapsed;
                    _messageBox.btnNo.Visibility = Visibility.Collapsed;
                    _messageBox.btnYes.Visibility = Visibility.Collapsed;
                    _messageBox.btnOk.Focus();
                    break;
                case MessageBoxButton.OKCancel:
                    _messageBox.btnNo.Visibility = Visibility.Collapsed;
                    _messageBox.btnYes.Visibility = Visibility.Collapsed;
                    _messageBox.btnCancel.Focus();
                    break;
                case MessageBoxButton.YesNo:
                    _messageBox.btnOk.Visibility = Visibility.Collapsed;
                    _messageBox.btnCancel.Visibility = Visibility.Collapsed;
                    _messageBox.btnNo.Focus();
                    break;
                case MessageBoxButton.YesNoCancel:
                    _messageBox.btnOk.Visibility = Visibility.Collapsed;
                    _messageBox.btnCancel.Focus();
                    break;
                default:
                    break;
            }
        }
        private static void SetImageOfMessageBox(MessageBoxImg image)
        {
            switch (image)
            {
                case MessageBoxImg.Warning:
                    _messageBox.SetImage("Warning.png");
                    break;
                case MessageBoxImg.Question:
                    _messageBox.SetImage("Question.png");
                    break;
                case MessageBoxImg.Information:
                    _messageBox.SetImage("Information.png");
                    break;
                case MessageBoxImg.Error:
                    _messageBox.SetImage("Error.png");
                    break;
                default:
                    _messageBox.img.Visibility = Visibility.Collapsed;
                    break;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender == btnOk)
            {
                _result = MessageBoxResult.OK;
            }
            else if (sender == btnYes)
            {
                _result = MessageBoxResult.Yes;
            }
            else if (sender == btnNo)
            {
                _result = MessageBoxResult.No;
            }
            else if (sender == btnCancel)
            {
                _result = MessageBoxResult.Cancel;
            }
            else
            {
                _result = MessageBoxResult.None;
            }

            Value = _messageBox.ValueTxt.Text;
            _messageBox.Close();
            _messageBox = null;
        }
        private void SetImage(string imageName)
        {
            string uri = string.Format("/ImageSrc/{0}", imageName);
            var uriSource = new Uri(uri, UriKind.RelativeOrAbsolute);
            img.Source = new BitmapImage(uriSource);
        }

        private void MessageTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
