using BasketballTournament.Entities;
using BasketballTournament.Views;
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
    /// Логика взаимодействия для Tournament.xaml
    /// </summary>
    public partial class Tournament : Window
    {
        private int Id;
        private List<TournamentView> tournamentViews = new List<TournamentView>();
        public Tournament(int id = -1)
        {
            InitializeComponent();
            Id = id;
            UpdateList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddGameButton_Click(object sender, RoutedEventArgs e)
        {
            AddGame form = new AddGame(Id,this);
            form.Show();
            //Close();
        }

        public void UpdateList() 
        {
            TournamentNet.Items.Clear();
            tournamentViews.Clear();
            for (int i = 0; i < 8; i++) 
            {
                tournamentViews.Add(new TournamentView() { Id = i+1, Name = Repository.Tournaments[Id].Teams[i].Name,Scores = 0});
                tournamentViews[i].Game = new string[8];
                tournamentViews[i].Game[i] = "x";
            }
            foreach(GameEntity ent in Repository.Games.Values) 
            {
                if (ent.Tournament.Id == Id) 
                {
                    for (int i = 0; i < 8; i++) 
                    {
                        if (Repository.Tournaments[Id].Teams[i].Id == ent.Team1.Id || Repository.Tournaments[Id].Teams[i].Id == ent.Team2.Id) 
                        {
                            for (int j = 0; j < 8; j++) 
                            {
                                if ((Repository.Tournaments[Id].Teams[i].Id == ent.Team1.Id && Repository.Tournaments[Id].Teams[j].Id == ent.Team2.Id) || (Repository.Tournaments[Id].Teams[i].Id == ent.Team2.Id && Repository.Tournaments[Id].Teams[j].Id == ent.Team1.Id)) 
                                {
                                    tournamentViews[i].Game[j] = (((ent.WinTeam && Repository.Tournaments[Id].Teams[i].Id == ent.Team1.Id)|| (!ent.WinTeam && Repository.Tournaments[Id].Teams[i].Id == ent.Team2.Id)) ? i+1 : j+1) + "/" + ent.Scores;
                                    if (ent.WinTeam && Repository.Tournaments[Id].Teams[i].Id == ent.Team1.Id)
                                    {
                                        tournamentViews[i].Scores += ent.Scores;
                                    }
                                    else if(!ent.WinTeam && Repository.Tournaments[Id].Teams[i].Id == ent.Team2.Id)
                                    {
                                        tournamentViews[i].Scores += ent.Scores;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            tournamentViews.Sort();


            int place = 1;
            tournamentViews[7].Place = place;

            for(int i = 6; i >= 0; i--) 
            {
                if (tournamentViews[i].Scores != tournamentViews[i + 1].Scores)
                {
                    place++;  
                }
                tournamentViews[i].Place = place;
            }

            tournamentViews.Sort(delegate (TournamentView x, TournamentView y)
            {
                if (x.Id > y.Id)
                {
                    return 1;
                }
                else if (x.Id < y.Id)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            });

            for (int i = 0; i < 8; i++) 
            {
                TournamentNet.Items.Add(tournamentViews[i]);
            }
            
        }
    }
}
