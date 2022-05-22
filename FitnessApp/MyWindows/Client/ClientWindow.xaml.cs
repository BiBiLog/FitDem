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

namespace FitnessApp.MyWindows.Client
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        fitness_dbEntities2 fitness = new fitness_dbEntities2();
        User _user = new User();

        public ClientWindow(User user)
        {
            InitializeComponent();
            _user = user;
            servicesGrid.ItemsSource = fitness.Service.ToList();
            bookingGrid.ItemsSource = fitness.Booking.Where(item => item.client_id == user.client_id).ToList();
        }

        private void add_btn_Click(object sender, RoutedEventArgs e)
        {
            Service service = (Service)servicesGrid.SelectedItems[0];

            if (servicesGrid.SelectedItems.Count == 1)
            {
                try
                {
                    Booking booking = new Booking();
                    booking.client_id = Convert.ToInt32(_user.client_id);
                    booking.service_id = service.service_id;
                    booking.isRegular = true;
                    

                    fitness.Booking.Add(booking);
                    fitness.SaveChanges();

                    MessageBox.Show("Запись добавлена", "Ура!");
                    bookingGrid.ItemsSource = fitness.Booking.ToList();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Упс!");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (bookingGrid.SelectedItems.Count == 1)
            {
                try
                {
                    Booking booking = (Booking)bookingGrid.SelectedItems[0];

                    fitness.Booking.Remove(booking);
                    fitness.SaveChanges();

                    MessageBox.Show("Успешно удалено", "Yeee");

                    bookingGrid.ItemsSource = fitness.Booking.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "oops");
                }
            }
        }
    }
}
