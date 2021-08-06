using BasketballTournament.DataBase;
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
using System.Windows.Shapes;

namespace BasketballTournament
{
    /// <summary>
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {
        public Profile()
        {
            InitializeComponent();
            DBController.controller.UpdateInfo();
            StringBuilder name = new StringBuilder();
            name.Append("Тренер: ");
            name.Append(Repository.User.FirstName);
            name.Append(" ");
            name.Append(Repository.User.LastName);
            NameText.Text = name.ToString();
        }

        private void TeamsButton_Click(object sender, RoutedEventArgs e)
        {
            Teams form = new Teams();
            form.Show();
            Close();
        }

        private void TournamentsButton_Click(object sender, RoutedEventArgs e)
        {
            Tournaments form = new Tournaments();
            form.Show();
            Close();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Repository.User = null;
            MainWindow form = new MainWindow();
            form.Show();
            Close();
        }
    }
}
