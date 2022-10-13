using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Plateshi
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        user user;
        DateTime startSessionTime;
        DateTime endSessionTime;
        public Main(user newUser)
        {
            InitializeComponent();

            startSessionTime = DateTime.Now;
            user = newUser;
            spisok_ListView.ItemsSource = Instances.db.products_users_table.Take(1000).ToList();
        }

        private void add_Button_Click(object sender, RoutedEventArgs e)
        {
            AddData addData = new AddData();
            addData.Show();
        }

        private void delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (spisok_ListView.SelectedItem != null)
            {
                DeleteData deleteData = new DeleteData(spisok_ListView);
                deleteData.Show();
                spisok_ListView.ItemsSource = Instances.db.products_users_table.Take(100).ToList();
            }
        }
        private void otchet_Button_Click(object sender, RoutedEventArgs e)
        {
            Otchet otchet = new Otchet();
            otchet.Show();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            endSessionTime = DateTime.Now;
            using (StreamWriter streamWriter = new StreamWriter("C:/UsersProfile/pr10fedorov/Documents/Payments/" + user.login + ".csv"))
            {
                streamWriter.WriteLine("Session Start " + startSessionTime);
                streamWriter.WriteLine("Session End " + endSessionTime);

                streamWriter.WriteLine("А здесь должно быть 'Сколько записей было добавлено за сеанс'");
                streamWriter.WriteLine("А здесь должно быть 'Сколько записей было изменено за сеанс'");
                streamWriter.WriteLine("А здесь должно быть 'Сколько записей было удалено за сеанс'");
                streamWriter.WriteLine("А здесь должно быть 'Общее кол-во затронутых за сеанс записей для данного пользователя'");
            }
            Login login = new Login();
            login.Show();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            
        }
    }
}
