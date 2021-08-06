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
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (RegName.Text.Length > 0 && RegLastName.Text.Length > 0 && LoginText.Text.Length > 0 && Password.Password.Length > 0)
            {
                if (Password.Password.Length > 3)
                {
                    if (LoginText.Text.Length > 3)
                    {
                        if (DBController.controller.AddUser(RegName.Text, RegLastName.Text, LoginText.Text, AESCrypto.EncryptStringAES(Password.Password)))
                        {
                            HelperFunctions.Info("Вы успешно зарегистрированы!");
                            int id = DBController.controller.GetUserIdAfterReg();
                            Repository.User = new UserEntity(id, RegName.Text, RegLastName.Text);
                            
                            Profile form = new Profile();
                            form.Show();
                            Close();
                        }
                        else 
                        {
                            HelperFunctions.Error("Логин уже существует");
                        }
                    }
                    else 
                    {
                        HelperFunctions.Error("Логин слишком короткий");
                    }
                }
                else 
                {
                    HelperFunctions.Error("Пароль слишком короткий");
                }
            }
            else 
            {
                HelperFunctions.Error("Не все поля заполнены");
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Login form = new Login();
            form.Show();
            Close();
        }
    }
}
