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
    /// Логика взаимодействия для CreateTurnir.xaml
    /// </summary>
    public partial class CreateTournament : Window
    {
        private Tournaments Window;
        private List<ComboBox> teams = new List<ComboBox>();

        public CreateTournament(Tournaments window)
        {
            InitializeComponent();
            Window = window;
            teams.Add(Team1);
            teams.Add(Team2);
            teams.Add(Team3);
            teams.Add(Team4);
            teams.Add(Team5);
            teams.Add(Team6);
            teams.Add(Team7);
            teams.Add(Team8);
            UpdateList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string name = TournamentName.Text;
            if (name.Length > 1)
            {
                bool isDuplicated = false;
                foreach (TournamentEntity ent in Repository.Tournaments.Values)
                {
                    if (ent.Name == name)
                    {
                        isDuplicated = true;
                        break;
                    }
                }

                if (isDuplicated)
                {
                    HelperFunctions.Error("Турнир с таким именем уже существует!");
                }
                else 
                {
                    if (IsDuplcatedTeam())
                    {
                        HelperFunctions.Error("Нельзя выбрать одну и туже команду несколько раз!");
                    }
                    else 
                    {
                        int[] ids = new int[8];
                        for (int i = 0; i < 8; i++)
                        {
                            ids[i] = (teams[i].SelectedItem as TeamEntity).Id;
                        }
                        DBController.controller.CreateTournament(name, ids);
                        HelperFunctions.Info("Турнир успешно создан!");
                        Window.UpdateList();
                        Close();
                    }
                }
            }
            else 
            {
                HelperFunctions.Error("Имя турнира слишком короткое");
            }
        }

        private void UpdateList() 
        {
            for (int i = 0; i < 8; i++) 
            {
                teams[i].Items.Clear();
            }

            foreach (TeamEntity entity in Repository.Teams.Values)
            {
                for (int i = 0; i < 8; i++)
                {
                    teams[i].Items.Add(entity);
                }
            }

            for (int i = 0; i < 8; i++)
            {
                teams[i].SelectedIndex = 0;
            }
        }

        private bool IsDuplcatedTeam() 
        {
            bool isDuplicated = false;
            for (int i = 0; i < 7; i++) 
            {
                for (int j = i + 1; j < 8; j++) 
                {
                    if (teams[i].SelectedIndex == teams[j].SelectedIndex) 
                    {
                        isDuplicated = true;
                        break;
                    }
                }
            }
            return isDuplicated;
        }
    }
}
