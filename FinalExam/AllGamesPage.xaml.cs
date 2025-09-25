using Microsoft.Extensions.Configuration;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalExam
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AllGamesPage : Page
    {
        public AllGamesPage()
        {
            this.InitializeComponent();
            PrintAllGames();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AllPlayersPage));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HomePage));
        }
        
        void PrintAllGames()
        {
            string cs = MainPage.GetConnectionString();
            string query = "SELECT Name, Icon, Developer, DevIcon, GameBlurb FROM dbo.Games";
            using (SqlConnection conn = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    //Get data
                    string title = (string)reader["Name"];
                    string gameImageSrc = (string)reader["Icon"];
                    string dev = (string)reader["Developer"];
                    string devImageSrc = (string)reader["DevIcon"];
                    string desc = (string)reader["GameBlurb"];

                    //Add data to elements
                    RelativePanel horizontal = new RelativePanel();
                    //horizontal.Orientation = Orientation.Horizontal;
                    horizontal.Background = new SolidColorBrush(Windows.UI.Colors.DarkSlateBlue);

                    //Game Stuff
                    StackPanel verticalA = new StackPanel();
                    verticalA.Padding = new Windows.UI.Xaml.Thickness(10);

                    TextBlock GameTitle = new TextBlock();
                    GameTitle.Text = title;
                    GameTitle.FontWeight = Windows.UI.Text.FontWeights.Bold;
                    GameTitle.FontSize = 25;
                    GameTitle.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;

                    Image gameImage = new Image();
                    BitmapImage bitmapGameImage = new BitmapImage();
                    bitmapGameImage.UriSource = new Uri(gameImageSrc);
                    gameImage.Source = bitmapGameImage;
                    gameImage.Width = 300;
                    gameImage.Height = 300;
                    gameImage.Stretch = Stretch.Fill;

                    Border gameBorder = new Border();
                    gameBorder.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
                    gameBorder.BorderThickness = new Thickness(5);
                    gameBorder.Child = gameImage;

                    verticalA.Children.Add(GameTitle);
                    verticalA.Children.Add(gameBorder);

                    //Dev Stuff
                    StackPanel verticalB = new StackPanel();
                    verticalB.Padding = new Windows.UI.Xaml.Thickness(10);

                    TextBlock DevName = new TextBlock();
                    DevName.Text = dev;
                    DevName.FontWeight = Windows.UI.Text.FontWeights.Bold;
                    DevName.FontSize = 25;
                    DevName.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;

                    Image devImage = new Image();
                    BitmapImage bitmapDevImage = new BitmapImage();
                    bitmapDevImage.UriSource = new Uri(devImageSrc);
                    devImage.Source = bitmapDevImage;
                    devImage.Width = 300;
                    devImage.Height = 300;
                    devImage.Stretch = Stretch.Fill;

                    Border devBorder = new Border();
                    devBorder.BorderBrush = new SolidColorBrush(Windows.UI.Colors.Black);
                    devBorder.BorderThickness = new Thickness(5);
                    devBorder.Child = devImage;

                    verticalB.Children.Add(DevName);
                    verticalB.Children.Add(devBorder);

                    //Desciption
                    TextBlock gameDesc = new TextBlock();
                    gameDesc.Padding = new Windows.UI.Xaml.Thickness(10);
                    gameDesc.Text = desc;
                    gameDesc.FontSize = 15;
                    gameDesc.FontStyle = Windows.UI.Text.FontStyle.Italic;
                    gameDesc.TextWrapping = TextWrapping.WrapWholeWords;
                    gameDesc.Width = double.NaN;
                    gameDesc.Height = double.NaN;
                    gameDesc.MinWidth = 200;

                    //Add all together
                    horizontal.Children.Add(verticalA);
                    horizontal.Children.Add(verticalB);
                    horizontal.Children.Add(gameDesc);
                    RelativePanel.SetAlignLeftWith(horizontal, verticalA);
                    RelativePanel.SetRightOf(gameDesc, verticalB);
                    RelativePanel.SetRightOf(verticalB, verticalA);
                    

                    listBoxGames.Items.Add(horizontal);
                }
            }
        }
    }
}
