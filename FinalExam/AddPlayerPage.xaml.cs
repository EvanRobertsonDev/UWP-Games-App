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
    public sealed partial class AddPlayerPage : Page
    {
        public AddPlayerPage()
        {
            this.InitializeComponent();
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamesByPlayers));
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddGamePage));
        }

        void AddPlayer(string nickname, string name, string surname)
        {
            string cs = MainPage.GetConnectionString();
            string query = "INSERT INTO dbo.Players (Nickname, Name, Surname) VALUES  (@nick, @name, @surname)";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("nick", nickname);
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("surname", surname);
                conn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                savePlayer.IsEnabled = true;
            }
            else
            {
                savePlayer.IsEnabled = false;
            }
        }

        private void savePlayer_Click(object sender, RoutedEventArgs e)
        {
            AddPlayer(textBox1.Text, textBox2.Text, textBox3.Text);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
    }
}
