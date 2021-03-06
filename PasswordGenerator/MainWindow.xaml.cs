using System;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using PasswordGenerator.Properties;

namespace PasswordGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private byte[] _key;

        public MainWindow()
        {
            InitializeComponent();
            Top = Settings.Default.Top;
            Left = Settings.Default.Left;
            KeyBox.Focus();
        }

        private void OnGoButtonClick(object sender, RoutedEventArgs e)
        {
            SetKey(KeyBox.Text + " ");
            GoButton.IsEnabled = false;
            CopyButton.IsEnabled = true;
            CopyButton.IsDefault = true;
            InputBox.Visibility = Visibility.Visible;
            ((Storyboard)Resources["FadeOutFadeIn"]).Begin();
            InputBox.Focus();
        }

        private void OnCopyButtonClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(OutputBox.Text);
            InputBox.SelectAll();
            InputBox.Focus();
        }

        private void OnInputBoxKeyUp(object sender, KeyEventArgs e)
            => SetPassword();

        private void OnOptionsButtonClick(object sender, RoutedEventArgs e)
        {
            new OptionsWindow(this).ShowDialog();
            SetPassword();
        }

        private void OnCloseButtonClick(object sender, RoutedEventArgs e)
            => Close();

        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            Settings.Default.Top = Top;
            Settings.Default.Left = Left;
            Settings.Default.Save();
        }

        private void SetKey(string key)
        {
            // Insert your code to scramble the key here.
            _key = Encoding.Unicode.GetBytes(key);
        }

        private void SetPassword()
            => OutputBox.Text = Settings.Default.Prefix + Cipher(InputBox.Text) + Settings.Default.Suffix;

        private string Cipher(string input)
        {
            // Insert your code to cipher the input using the key here.
            byte[] cipher = new byte[input.Length * 2 + _key.Length];
            Encoding.Unicode.GetBytes(input).CopyTo(cipher, 0);
            _key.CopyTo(cipher, input.Length * 2);
            return Convert.ToBase64String(cipher).TrimEnd('=');
        }
    }
}
