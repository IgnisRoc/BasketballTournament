using BasketballTournament.Entities;
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
    /// Логика взаимодействия для Tournaments.xaml
    /// </summary>
    public partial class Tournaments : Window
    {
        public Tournaments()
        {
            InitializeComponent();
            UpdateList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Profile form = new Profile();
            form.Show();
            Close();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Tournament form = new Tournament((TournamentCombo.SelectedItem as TournamentEntity).Id);
            form.Show();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (Repository.Teams.Count > 7)
            {
                CreateTournament form = new CreateTournament(this);
                form.Show();
            }
            else
            {
                HelperFunctions.Error("Недостаточно команд для создания турнира!");
            }
        }

        public void UpdateList()
        {
            TournamentCombo.Items.Clear();
            foreach (TournamentEntity ent in Repository.Tournaments.Values) 
            {
                TournamentCombo.Items.Add(ent);
            }

            if (Repository.Tournaments.Count > 0)
            {
                TournamentCombo.SelectedIndex = 0;
                OpenButton.IsEnabled = true;
            }
            else 
            {
                OpenButton.IsEnabled = false;
            }
        }
    }
}
