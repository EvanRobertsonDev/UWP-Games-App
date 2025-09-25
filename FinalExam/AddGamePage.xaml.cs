using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalExam
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddGamePage : Page
    {
        public AddGamePage()
        {
            this.InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddPlayerPage));
        }

        void AddGame(string title, string gameImg, string dev, string devImg, string desc)
        {
            string cs = MainPage.GetConnectionString();
            string query = "INSERT INTO dbo.Games (Name, Icon, Developer, DevIcon, GameBlurb) VALUES  (@name, @icon, @dev, @devicon, @desc)";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("name", title);
                cmd.Parameters.AddWithValue("icon", gameImg);
                cmd.Parameters.AddWithValue("dev", dev);
                cmd.Parameters.AddWithValue("devicon", devImg);
                cmd.Parameters.AddWithValue("desc", desc);
                conn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        private void saveGame_Click(object sender, RoutedEventArgs e)
        {
            AddGame(textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox4.Text) && !string.IsNullOrWhiteSpace(textBox5.Text))
            {
                saveGame.IsEnabled = true;
            }
            else
            {
                saveGame.IsEnabled = false;
            }
        }
    }
}
