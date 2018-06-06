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

namespace AddReferencesSampleApp
{
    /// <summary>
    /// Interaction logic for SelectedItems.xaml
    /// </summary>
    public partial class SelectedItems : Window
    {
        public SelectedItems()
        {
            InitializeComponent();
        }

        public void Getlistselecteditems(string[] items)
        {
            try
            {
                for (int i = 0; i < items.Length; i++)
                {
                    //txtblockItems.Text = i + items[i] + "\n";
                    cmbboxselectedValues.Items.Add(items[i]);
                }
                //cmbboxselectedValues.SelectedIndex[0];

            }
            catch (Exception)
            {
                throw;
            }

        }

        public void Getlistselecteditems(string[] items,string[] otheritems)
        {
            try
            {
                for (int i = 0; i < items.Length; i++)
                {                   
                    cmbboxselectedValues.Items.Add(items[i]);
                }

                for (int J = 0; J < otheritems.Length; J++)
                {
                    cmbboxselectedValues.Items.Add(otheritems[J]);
                }
                //cmbboxselectedValues.SelectedIndex[0];
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow();
            this.Hide();
            mainWindow.Show();
        }
    }
}
