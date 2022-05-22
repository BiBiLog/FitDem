using FitnessApp.Model;
using FitnessApp.MyWindows.Client;
using FitnessApp.MyWindows.Emploee;
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

namespace FitnessApp.MyWindows.Auth
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        fitness_dbEntities2 fitness = new fitness_dbEntities2();

        public AuthWindow()
        {
            InitializeComponent();
        }

        private void auth_btn_Click(object sender, RoutedEventArgs e)
        {
            if(isChecked())
            {
                try
                {
                    User user = fitness.User.FirstOrDefault(item => item.login == login_box.Text && item.password == password_box.Password);

                    if(user != null)
                    {
                        if (user.role_id == 1)
                        {
                            ClientWindow clientWindow = new ClientWindow(user);
                            clientWindow.Show();
                            this.Close();
                        }
                        if (user.role_id == 2)
                        {
                            EmploeeWindow emploeeWindow = new EmploeeWindow();
                            emploeeWindow.Show();
                            this.Close();
                        }
                        if (user.role_id == 3)
                        {
                            //открыть окно админа
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Упс!");
                }
            }
        }

        private bool isChecked()
        {
            StringBuilder error = new StringBuilder();

            if(string.IsNullOrEmpty(login_box.Text))
            {
                error.AppendLine("Введите логин");
            }
            if (string.IsNullOrEmpty(password_box.Password))
            {
                error.AppendLine("Введите пароль");
            }
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString(), "Ошибка!");
                return false;
            }
            else 
            {
                return true;
            }
        }

    }
}
