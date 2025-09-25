using System;
using System.Collections.Generic;
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
using Microsoft.Extensions.Configuration;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FinalExam
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    { 
        public MainPage()
        {
            this.InitializeComponent();
            frame.Navigate(typeof(HomePage));
        }

        private void MenuItemHome_Tapped(object sender, TappedRoutedEventArgs e)
        {
            frame.Navigate(typeof(HomePage));
        }

        private void MenuItemAllGames_Tapped(object sender, TappedRoutedEventArgs e)
        {
            frame.Navigate(typeof(AllGamesPage));
        }

        private void MenuItemAllPlayers_Tapped(object sender, TappedRoutedEventArgs e)
        {
            frame.Navigate(typeof(AllPlayersPage));
        }

        private void MenuItemGamesByPlayers_Tapped(object sender, TappedRoutedEventArgs e)
        {
            frame.Navigate(typeof(GamesByPlayers));
        }

        private void MenuItemAddGame_Tapped(object sender, TappedRoutedEventArgs e)
        {
            frame.Navigate(typeof(AddGamePage));
        }

        private void MenuItemAddPlayer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            frame.Navigate(typeof(AddPlayerPage));
        }
        public static string GetConnectionString()
        {
            ConfigurationBuilder cb = new ConfigurationBuilder();
            cb.SetBasePath(Directory.GetCurrentDirectory());
            cb.AddJsonFile("config.json");
            IConfiguration config = cb.Build();
            return config["ConnectionStrings:GamesInfo"];
        }
    }
}
