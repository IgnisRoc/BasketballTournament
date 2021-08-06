using BasketballTournament.DataBase;
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
    /// Логика взаимодействия для ChangeTeam.xaml
    /// </summary>
    public partial class ChangeTeam : Window
    {
        public int Id;
        Teams Window;

        public ChangeTeam(Teams window,int id = -1)
        {
            InitializeComponent();
            Id = id;
            if (!(id < 0)) 
            {
                TeamName.Text = Repository.Teams[id].Name;
            }
            this.Window = window;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string tempName = TeamName.Text;
            
            if (tempName.Length > 1)
            {
                bool isDuplicated = false;
                foreach (TeamEntity team in Repository.Teams.Values) 
                {
                    if(team.Name == tempName) 
                    {
                        isDuplicated = true;
                        break;
                    }
                }

                if (isDuplicated)
                {
                    HelperFunctions.Error("Команда с таким именем уже существует!");
                }
                else
                {
                    if (Id < 0)
                    {
                        DBController.controller.CreateNewTeam(TeamName.Text);
                    }
                    else
                    {
                        DBController.controller.UpdateTeam(Id, TeamName.Text);
                    }
                    HelperFunctions.Info("Команда успешно сохранена");
                    this.Window.UpdateList();
                    Close();
                }
            }
            else 
            {
                HelperFunctions.Error("Название команды слишком короткое!");
            }
        }
    }
}
