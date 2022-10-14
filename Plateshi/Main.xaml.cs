using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace Plateshi
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Window
    {

        #region Variables

        private user user;
        DateTime startSessionTime;
        DateTime endSessionTime;
        private int addedItemsCount = 0;
        private int editedItemsCount = 0;
        private int deletedItemsCount = 0;
        private int operationsCount = 0;
        public user User { get => user; set => user = value; }
        public int AddedCount { get => addedItemsCount; set => addedItemsCount = value; }
        public int EditedCount { get => editedItemsCount; set => editedItemsCount = value; }
        public int DeletedCount { get => deletedItemsCount; set => deletedItemsCount = value; }
        public int OprationsCount { get => operationsCount; set => operationsCount = value; }

        #endregion

        public Main(user newUser)
        {
            InitializeComponent();

            //Init Variables
            user = newUser;

            //Log
            startSessionTime = DateTime.Now;

            //Init Elements
            category_ComboBox.ItemsSource = Instances.db.categories.AsParallel().Take(100).ToList();
            RefreshListView();
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
                DeleteData deleteData = new DeleteData(this);
                deleteData.Show();
            }
        }
        private void otchet_Button_Click(object sender, RoutedEventArgs e)
        {
            Otchet otchet = new Otchet();
            otchet.Show();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            endSessionTime = DateTime.Now;
            using (StreamWriter streamWriter = new StreamWriter("C:/UsersProfile/pr10fedorov/Documents/Payments/" + user.login + ".csv"))
            {
                streamWriter.WriteLine("Session Start " + startSessionTime.ToString());
                streamWriter.WriteLine("Session End " + endSessionTime.ToString());
                streamWriter.WriteLine("Сколько записей было добавлено за сеанс: " + addedItemsCount);
                streamWriter.WriteLine("Сколько записей было изменено за сеанс: " + editedItemsCount);
                streamWriter.WriteLine("Сколько записей было удалено за сеанс: " + deletedItemsCount);
                streamWriter.WriteLine("Общее кол-во затронутых записей за сеанс: " + operationsCount);

                streamWriter.Close();
            }
            Login login = new Login();
            login.Show();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void setCategory_Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshListView();
        }

        private void clearCategory_Button_Click(object sender, RoutedEventArgs e)
        {
            category_ComboBox.Text = "";
            spisok_ListView.ItemsSource = Instances.db.products_users_table.Where(q => q.fk_user_id == user.pk_user_id).Take(100).ToList();
        }
        public void RefreshListView()
        {
            //MessageBox.Show();
            if (category_ComboBox.Text != "")
            {
                spisok_ListView.ItemsSource = Instances.db.products_users_table.Where(q => q.fk_user_id == user.pk_user_id && q.product.category.category_name == category_ComboBox.Text).Take(100).ToList();
            }
            else
                spisok_ListView.ItemsSource = Instances.db.products_users_table.Where(q => q.fk_user_id == user.pk_user_id).Take(100).ToList();
        }
    }
}
