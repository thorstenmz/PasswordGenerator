using System.Windows;
using PasswordGenerator.Properties;

namespace PasswordGenerator
{
    /// <summary>
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow(Window owner)
        {
            InitializeComponent();
            Owner = owner;
            PrefixBox.Text = Settings.Default.Prefix;
            SuffixBox.Text = Settings.Default.Suffix;
            PrefixBox.Focus();
        }

        private void OnOkButtonClick(object sender, RoutedEventArgs e)
        {
            Settings.Default.Prefix = PrefixBox.Text;
            Settings.Default.Suffix = SuffixBox.Text;
            DialogResult = true;
            Close();
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
