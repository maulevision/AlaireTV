using System.Windows;

namespace AlaireTV
{
    public partial class InputDialog : Window
    {
        public string InputText { get; set; }

        public InputDialog(string prompt)
        {
            InitializeComponent();
            PromptText.Text = prompt;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            InputText = InputTextBox.Text;
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
