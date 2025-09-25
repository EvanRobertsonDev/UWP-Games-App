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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalExam
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamesByPlayers : Page
    {
        public GamesByPlayers()
        {
            this.InitializeComponent();
            PrintAllGamesPlayers();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AllPlayersPage));
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddPlayerPage));
        }

        void PrintAllGamesPlayers()
        {
            string cs = MainPage.GetConnectionString();
            string query = "SELECT Nickname, g.Name, g.Icon, gameId, PlayerId FROM dbo.Players p JOIN dbo.GamePlayers pg ON p.id = pg.playerid JOIN dbo.Games g ON g.id = pg.gameid";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    string nickname = (string)reader["Nickname"];
                    string gameName = (string)reader["Name"];
                    string imageSrc = (string)reader["Icon"];
                    int gameId = (int)reader["gameId"];
                    int playerId = (int)reader["PlayerId"];

                    RelativePanel horizontal = new RelativePanel();
                    horizontal.Background = new SolidColorBrush(Windows.UI.Colors.DarkSlateBlue);

                    TextBlock nicknameText = new TextBlock();
                    nicknameText.Foreground = new SolidColorBrush(Windows.UI.Colors.Black);
                    nicknameText.FontSize = 50;
                    nicknameText.Text = nickname;
                    nicknameText.Width = 300;
                    RelativePanel.SetAlignLeftWith(horizontal, nicknameText);

                    ListBox gamesList = new ListBox();
                    gamesList.Tag = playerId;
                    gamesList.Background = new SolidColorBrush(Windows.UI.Colors.SlateBlue);
                    StackPanel game = new StackPanel();
                    game.Orientation = Orientation.Horizontal;
                    game.Background = new SolidColorBrush(Windows.UI.Colors.DarkSlateBlue);

                    Image gameImage = new Image();
                    BitmapImage bitmapGameImage = new BitmapImage();
                    bitmapGameImage.UriSource = new Uri(imageSrc);
                    gameImage.Source = bitmapGameImage;
                    gameImage.Width = 80;
                    gameImage.Height = 80;

                    TextBlock title = new TextBlock();
                    title.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                    title.FontSize = 25;
                    title.Height = 30;
                    title.Text = gameName;
                    title.Margin = new Windows.UI.Xaml.Thickness(10);
                    title.VerticalAlignment = VerticalAlignment.Center;

                    game.Children.Add(gameImage);
                    game.Children.Add(title);
                    gamesList.Items.Add(game);
                    
                    horizontal.Children.Add(nicknameText);
                    horizontal.Children.Add(gamesList);

                    listBoxGamesPlayers.Items.Add(horizontal);
                    RelativePanel.SetRightOf(gamesList, nicknameText);

                }
            }
        }
    }
}
