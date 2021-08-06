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
    /// Логика взаимодействия для Team.xaml
    /// </summary>
    public partial class Teams : Window
    {
        public Teams()
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ChangeTeam form = new ChangeTeam(this);
            form.Show();
        }

        public void UpdateList() 
        {
            TeamsList.Items.Clear();
            foreach (TeamEntity entity in Repository.Teams.Values)
            {    
                TeamsList.Items.Add(entity);
            }
        }

        private void TeamsList_Click(object sender, RoutedEventArgs e)
        {
            ChangeTeam form = new ChangeTeam(this,((sender as Button).CommandParameter as TeamEntity).Id);
            form.Show();
        }
    }
}