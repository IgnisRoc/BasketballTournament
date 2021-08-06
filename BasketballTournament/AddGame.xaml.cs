using BasketballTournament.DataBase;
using BasketballTournament.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddGame.xaml
    /// </summary>
    public partial class AddGame : Window
    {
        private int Id;
        private Tournament Window;
        public AddGame(int id, Tournament window)
        {
            InitializeComponent();
            Id = id;
            Window = window;
            UpdateList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if ((Team1.SelectedItem as TeamEntity).Id != (Team2.SelectedItem as TeamEntity).Id)
            {
                if (int.Parse(Scores.Text) > 0) 
                {
                    bool isDuplicated = false;
                    foreach (GameEntity ent in Repository.Games.Values) 
                    {
                        if (ent.Tournament.Id == Id && (((Team1.SelectedItem as TeamEntity).Id == ent.Team1.Id) && ((Team2.SelectedItem as TeamEntity).Id == ent.Team2.Id)) || ((Team1.SelectedItem as TeamEntity).Id == ent.Team2.Id) && ((Team2.SelectedItem as TeamEntity).Id == ent.Team1.Id)) 
                        {
                            isDuplicated = true;
                            break;
                        }
                    }

                    if (isDuplicated)
                    {
                        HelperFunctions.Error("Данные об игре уже существуют!");
                    }
                    else 
                    {
                        DBController.controller.CreateGame(Id, (Team1.SelectedItem as TeamEntity).Id, (Team2.SelectedItem as TeamEntity).Id, int.Parse(Scores.Text) * ((bool)Team1RadioCheck.IsChecked ? 1 : -1));
                        HelperFunctions.Info("Игра успешно создана");
                        Window.UpdateList();
                        Close();
                    }
                }
                else 
                {
                    HelperFunctions.Error("Очков должно быть больше 0!");
                }
            }
            else 
            {
                HelperFunctions.Error("Команда не может соревноваться сама с собой!");
            }
        }

        private void Scores_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void UpdateList() 
        {
            Team1.Items.Clear();
            Team2.Items.Clear();
            foreach (TeamEntity entity in Repository.Teams.Values) 
            {
                Team1.Items.Add(entity);
                Team2.Items.Add(entity);
            }

            Team1.SelectedIndex = 0;
            Team2.SelectedIndex = 0;

            Scores.Clear();
        }
    }
}
