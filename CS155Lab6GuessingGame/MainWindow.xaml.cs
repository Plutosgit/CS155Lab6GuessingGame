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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CS155Lab6GuessingGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public const int RND_MIN = 0;       // random # min
        public const int RND_MAX = 100;     // random # max
        public int PCS_RANDOM_NUM = -1;     // Init
        public Brush defBrush;

        public int nGuessCount = 0;

        public MainWindow()
        {
            InitializeComponent();

            this.Title = "Guessing game";
            txtUsersGuess.Focus();

            Random rndA = new Random();
            PCS_RANDOM_NUM = rndA.Next(RND_MIN, RND_MAX);
            //MessageBox.Show(PCS_RANDOM_NUM.ToString());

            defBrush = lblResult.Background; //Get the label's default color, so as to use for updating status

        }


        private void cmdGuessCheck_OnClick(object sender, RoutedEventArgs e)
        {
            int nUsersGuess;
            bool bResult;
            String s;

            bResult = Int32.TryParse(txtUsersGuess.Text, out nUsersGuess);
            if (!bResult)
            {
                MessageBox.Show("Please enter a valid guess!");
                txtUsersGuess.Focus();
                return;
            }
            else
            {
                if (nUsersGuess < RND_MIN || nUsersGuess > RND_MAX)
                {
                    MessageBox.Show("Please enter a valid guess (0-100 inclusive)!");
                    txtUsersGuess.Focus();
                    return;
                }

                // Now increment the valid guess attempt..
                nGuessCount++;

                if (nUsersGuess < PCS_RANDOM_NUM)
                {
                    lblResult.Content = "Try again! Your guess is less than my guess..";
                    lblResult.Background = Brushes.Yellow;
                    txtUsersGuess.Focus();
                }
                else if(nUsersGuess > PCS_RANDOM_NUM)
                {
                    lblResult.Content = "Try again! Your guess is more than my guess..";
                    lblResult.Background = Brushes.Orange;
                    txtUsersGuess.Focus();
                }
                else
                {
                    s = nGuessCount.ToString() + " attempts!";
                    lblResult.Content = "Nice job! You got my guess correctly in " + s;
                    lblResult.Background = Brushes.LightGreen;
                    txtUsersGuess.Focus();

                    MessageBox.Show("Nice job! You got my guess correctly in " + s);
                    this.Close();
                }

            }

        }

        private void txtUsersGuess_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUsersGuess.SelectAll();
        }

        private void txtUsersGuess_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (lblResult != null)
            {
                lblResult.Background = defBrush; 
                lblResult.Content = "-";
            }
        }
    }
}
