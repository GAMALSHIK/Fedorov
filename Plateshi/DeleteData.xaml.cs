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
        ListView listView;
        public DeleteData(ListView newListView)
        {
            InitializeComponent();
            listView = newListView;
        }

        private void delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var removedData = listView.SelectedItems.Cast<products_users_table>().ToList();
            Instances.db.products_users_table.RemoveRange(removedData);
            Instances.db.SaveChanges();
            Instances.db.ChangeTracker.Entries().ToList().ForEach(q => q.Reload());
            this.Close();
        }
    }
}
