using FitnessApp.Model;
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
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        fitness_dbEntities2 fitness = new fitness_dbEntities2();

        public RegWindow()
        {
            InitializeComponent();
        }

        private void reg_btn_Click(object sender, RoutedEventArgs e)
        {
            if(isChecked())
            {
                try
                {

                    Model.Client client = new Model.Client();
                    User user = new User();


                    client.lastname = lastname_box.Text;
                    client.name = name_box.Text;
                    client.surname = surname_box.Text;
                    client.telephone = telephone_box.Text;

                    fitness.Client.Add(client);
                    fitness.SaveChanges();

                    user.login = login_box.Text;
                    user.password = password_box.Password;

                    var currentClient = fitness.Client.ToList();
                    user.client_id = currentClient.Last().client_id;

                    user.role_id = 1;

                    fitness.User.Add(user);
                    fitness.SaveChanges();

                    MessageBox.Show("Вы добавлены в систему как клиент", "Ура!");


                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message.ToString(), "Упс!");
                }
            }
        }

        private bool isChecked()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(surname_box.Text))
            {
                error.AppendLine("Введите фамилию");
            }
            if (string.IsNullOrEmpty(name_box.Text))
            {
                error.AppendLine("Введите имя");
            }
            if (string.IsNullOrEmpty(lastname_box.Text))
            {
                error.AppendLine("Введите отчество");
            }
            if (string.IsNullOrEmpty(telephone_box.Text))
            {
                error.AppendLine("Введите телефон");
            }
            if (string.IsNullOrEmpty(login_box.Text))
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
