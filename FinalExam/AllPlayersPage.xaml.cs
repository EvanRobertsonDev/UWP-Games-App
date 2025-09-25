using System;
using System.Collections;
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
    public sealed partial class AllPlayersPage : Page
    {
        private ArrayList playerIDs = new ArrayList();

        public AllPlayersPage()
        {
            this.InitializeComponent();
            PrintAllPlayers();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamesByPlayers));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AllGamesPage));
        }

        void PrintAllPlayers()
        {
            string cs = MainPage.GetConnectionString();
            string query = "SELECT id, Nickname, Name, Surname FROM dbo.Players";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string nickname = (string)reader["Nickname"];
                    string name = (string)reader["Name"];
                    string surname = (string)reader["Surname"];
                    playerIDs.Add((int)reader["id"]);

                    TextBlock data = new TextBlock();
                    data.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
                    data.FontSize = 25;
                    data.Text = nickname + " (" + surname + ", " + name + ")";

                    data.Tapped += new TappedEventHandler(tappedItem);

                    listBoxPlayers.Items.Add(data);
                }
            }
        }

        void DeletePlayer(object _player)
        {
            string cs = MainPage.GetConnectionString();
            string query = "DELETE FROM dbo.Players WHERE id=@Player";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("Player", _player);
                conn.Open();

                cmd.ExecuteNonQuery();


            }

            query = "DELETE FROM dbo.GamePlayers WHERE PlayerId=@Player";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("Player", _player);
                conn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        void tappedItem(object sender, TappedRoutedEventArgs e)
        {
            DeleteButton.IsEnabled = true;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePlayer(playerIDs[listBoxPlayers.Items.IndexOf(listBoxPlayers.SelectedItem)]);
            listBoxPlayers.Items.Remove(listBoxPlayers.SelectedItem);
            DeleteButton.IsEnabled = false;
        }
    }
}
