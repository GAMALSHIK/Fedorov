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

namespace Plateshi
{
    /// <summary>
    /// Логика взаимодействия для DeleteData.xaml
    /// </summary>
    public partial class DeleteData : Window
    {
        Main main;
        public DeleteData(Main newMainWindow)
        {
            InitializeComponent();
            main = newMainWindow;
        }

        private void delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var removedData = main.spisok_ListView.SelectedItems.Cast<products_users_table>().ToList();
            Instances.db.products_users_table.RemoveRange(removedData);
            Instances.db.SaveChanges();
            Instances.db.ChangeTracker.Entries().ToList().ForEach(q => q.Reload());


            //Refresh Visual and LogFile
            //main.spisok_ListView.ItemsSource = Instances.db.products_users_table.Where(q => q.fk_user_id == main.User.pk_user_id).Take(100).ToList();
            main.RefreshListView();
            main.DeletedCount = removedData.Count;

            this.Close();
        }
    }
}
