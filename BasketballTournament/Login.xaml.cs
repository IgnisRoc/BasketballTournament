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
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (LoginText.Text.Length > 3)
            {
                if (PasswordText.Password.Length > 3)
                {
                    UserEntity user = DBController.controller.AuthorizeUser(LoginText.Text);
                    if (user != null)
                    {
                        if (AESCrypto.DecryptStringAES(user.SaltedPass) == PasswordText.Password)
                        {
                            user.SaltedPass = "";
                            Repository.User = user;
                            Profile form = new Profile();
                            form.Show();
                            Close();
                        }
                        else 
                        {
                            HelperFunctions.Error("Пароль неверный");
                        }
                    }
                    else 
                    {
                        HelperFunctions.Error("Пользователь не найден");
                    }
                }
                else
                {
                    HelperFunctions.Error("Пароль слишком короткий");
                }
            }
            else 
            {
                HelperFunctions.Error("Логин слишком короткий");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Register form = new Register();
            form.Show();
            Close();
        }
    }
}