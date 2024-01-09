using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Theme09
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private static string[] SplitText(string text) => text.Split(' ');

        private static string ReverseWords(string inputPhrase)
        {
            var words = SplitText(inputPhrase).ToArray();
            var result = string.Join(" ", words.Reverse()).Trim();
            return result;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var words = SplitText(sentenceTextBox1.Text);
            wordsListBox.ItemsSource = words;
            wordsListBox.Items.Refresh();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var text = ReverseWords(sentenceTextBox2.Text);
            reversedTextLabel.Content = text;
        }
    }
}