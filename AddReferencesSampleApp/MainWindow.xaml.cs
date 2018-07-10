using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AddReferencesSampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Declaration
        public string filepath = null;// @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.1";//
        public string fileextension = ".dll";
        public string versionselecteditem = null;
        public string versioncombined = null;
        public string namespaceselectedItem = null;
        public string namespacecombined = null;
        public string getfileinformation = null;        
        
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            this.txtblockDescription.Background = new SolidColorBrush(Colors.LightGray);
            if (Environment.Is64BitOperatingSystem)
            {
                filepath = @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5.1";
            }
            else
            {
                filepath = @"C:\Program Files\Reference Assemblies\Microsoft\Framework\v3.5";
            }
            txtPath.Text = filepath.ToString();
            GetDirectories(filepath);
            lvNamespaces.SelectedIndex = 0;        

        }

        public class Namespaces
        {
            //public string Namespace { get; set; }
            //// public bool Selection { get; set; }
            //public bool IsChecked { get; set; }

            private string nameValue;
            public string Namespace
            {
                get
                {
                    return nameValue;
                }
                set
                {
                    nameValue = value;
                }
            }

            private bool ageValue;
            public bool IsChecked
            {
                get { return ageValue; }

                set
                {
                    if (value != ageValue)
                    {
                        ageValue = value;
                    }
                }
            }

            private string properties;
            public string Property
            {
                get { return properties; }

                set
                {
                    if (value != properties)
                    {
                        properties = value;
                    }
                }
            }

        }        

        #region Click events

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GetDirectories(filepath);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            int D = 0;           
            try
            {
                //Namespaces namespacesOK = (Namespaces)lvNamespaces.SelectedItems;
                string[] selecteditems = new string[lvNamespaces.Items.Count];
                if (lvNamespaces.SelectedItems.Count > 0)
                {

                    //string[] xr = new string[20];
                    //int y = 0;
                    //foreach (Namespaces check in lvNamespaces.Items)
                    //{
                    //    if (check.IsChecked == true)
                    //    {
                    //        for (int i = y; i < lvNamespaces.Items.Count;)
                    //        {
                    //            xr[i] = check.Namespace.ToString();                                
                    //            y = i + 1;
                    //            break;

                    //        }
                    //    }
                        
                    //}

                    foreach (Namespaces item in lvNamespaces.SelectedItems) /*After select all check-boxes, then want to get all listview items here.*/
                    {
                        if (item.IsChecked == true)
                        {
                            for (int x = D; x < lvNamespaces.SelectedItems.Count;)
                            {
                                selecteditems[x] = item.Namespace.ToString();
                                D = x + 1;
                                break;
                                #region
                                //if(item.IsChecked == true)
                                //{
                                //}

                                //var du = (Namespaces) as lvNamespaces.SelectedItems[x];
                                // do your stuff here with du
                                //}
                                //Namespaces ;
                                // Namespaces selecteditems = (Namespaces)lvNamespaces.SelectedItems;

                                // foreach (var item in lvNamespaces.SelectedItems)
                                //{

                                //}
                                #endregion
                            }
                        }
                    }

                    selecteditems = selecteditems.Where(c => c != null).ToArray();

                    if (cmbboxselecteditems.Items.Count > 0)
                    {
                        string[] browseselecteditems = new string[cmbboxselecteditems.Items.Count];

                        for (int i = 0; i < cmbboxselecteditems.Items.Count; i++)
                        {
                            browseselecteditems[i] = cmbboxselecteditems.Items[i].ToString();
                        }
                        GettheselectedItemsandOtheritems(selecteditems, browseselecteditems);
                    }                  
                    else
                        GettheselectedItems(selecteditems);
                }
                else
                    MessageBox.Show("Select Atlease One Item", "",MessageBoxButton.OK);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            #region
            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //openFileDialog1.InitialDirectory = @"C:\Program Files(x86)\Reference Assemblies";
            //openFileDialog1.Title = "Browse Text Files";
            //openFileDialog1.CheckFileExists = true;
            //openFileDialog1.CheckPathExists = true;
            //openFileDialog1.DefaultExt = "txt";
            //openFileDialog1.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            //openFileDialog1.FilterIndex = 2;
            //openFileDialog1.RestoreDirectory = true;
            //openFileDialog1.ReadOnlyChecked = true;
            //openFileDialog1.ShowReadOnly = true;
            //if (openFileDialog1.ShowDialog() == btnBrowse.Focus())
            //{
            //    //textBox1.Text = openFileDialog1.FileName;
            //}
            #endregion

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "DLL (*.dll)|*.dll|All files (*.*)|*.*";
                //openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
                openFileDialog.InitialDirectory = @"C:\Program Files(x86)\Reference Assemblies";
                if (openFileDialog.ShowDialog() == true)
                {
                    foreach (string filename in openFileDialog.FileNames)
                    {
                        DirectoryInfo di1 = new DirectoryInfo(filename);
                        if (!cmbboxselecteditems.Items.Contains(di1.Name))
                            cmbboxselecteditems.Items.Add(di1.Name);
                        else
                            MessageBox.Show("This item already Added" + di1.Name, "Alert", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                        #region
                        //if (cmbboxselecteditems.Items.Count == 0)
                        //{
                        //    cmbboxselecteditems.Items.Add(di1.Name);
                        //}
                        //else if(cmbboxselecteditems.Items.Count > 0)
                        //{
                        //    if (cmbboxselecteditems.Items.Contains(new ComboBoxItem { Content = di1.Name }))// if (!di1.Name.Contains(cmbboxselecteditems.SelectedItem.ToString()))
                        //    {
                        //        cmbboxselecteditems.Items.Add(di1.Name);
                        //    }
                        //    else
                        //    {
                        //        MessageBox.Show("Already the Item is added in the List...");
                        //    }                           
                        //}
                        #endregion

                        #region
                        // FileInfo[] files = di1.GetFiles(".dll");
                        //if (files != null && files.Length > 0)
                        //{
                        //foreach (var item in di1.Name)//di1.Name
                        //{
                        //    if (item != null)//&& item.Length > 0
                        //    {
                        //cmbboxselecteditems.SelectedItem = files[0];

                        #endregion
                    }
                    cmbboxselecteditems.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
          
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Events
        private void lvNamespaces_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Namespaces namespaces;
            try
            {
                #region
                //if (lvNamespaces.SelectedItems.Count > 0)
                //{
                //    for (int i = 0; i < lvNamespaces.SelectedItems.Count; i++)
                //    {
                //        if (lvNamespaces.SelectedItems[i].Selected)
                //        {
                //            int i2 = lvNamespaces.SelectedItems[i].Index;
                //            item = lvNamespaces.Items[i2].Text;
                //            //sColl.Add(item);
                //        }
                //    }
                //}





                //Namespaces namespaces = (Namespaces)lvNamespaces.SelectedItems[aq];

                //if (lvNamespaces.SelectedItems.Count > 0)
                //{
                //    for (int lcount = 0; lcount <= lvNamespaces.Items.Count - 1; lcount++)
                //    {
                //        if (lvNamespaces.Items[lcount]. == true)
                //        {
                //            var2 = lcount;
                //            break;
                //        }
                //    }
                //}

                //string a = lvNamespaces.SelectedItems.Count.ToString();
                //int b = Convert.ToInt32(a);

                // int aq =  lvNamespaces.SelectedIndex;
                #endregion
                namespaces = (Namespaces)lvNamespaces.SelectedItem;//SelectedItems[0]; //System.Windows.MessageBox.Show(namespaces.Namespace);
                if (namespaces != null)
                {
                    namespacecombined = filepath + '\\' + namespaces.Namespace;
                    if (namespacecombined != null && namespacecombined.Length > 0)
                    {
                        //getfileinformation = namespacecombined;
                        // if (getfileinformation != null && getfileinformation.Length > 0)
                        // {
                        FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(namespacecombined);
                      //  txtblockDescription.Text = myFileVersionInfo.ToString(); //MessageBox.Show(myFileVersionInfo.ToString());  
                        Namespaces person = new Namespaces { Property = myFileVersionInfo.ToString() };
                        this.DataContext = person;
                        //}

                    }
                    else
                        txtblockDescription.Text = "";
                }
                else
                txtblockDescription.Text = "";
                #region

                //if (lvNamespaces.SelectedIndex.Count <= 0)
                //{
                //    return;
                //}
                //ListItem vae intselectedindex = lvNamespaces.SelectedIndex[0];//SelectedIndices[0];
                //if (intselectedindex >= 0)
                //{
                //    String text = listView1.Items[intselectedindex].Text;

                //    //do something
                //    //MessageBox.Show(listView1.Items[intselectedindex].Text); 
                //}


                //var chapters = new List<Namespaces>();
                //foreach (var item in lvNamespaces.SelectedItems)
                //    //.Add((Namespaces)item);

                //this.txtblockDescription.Text = this.lvNamespaces.Items[lvNamespaces.SelectedIndex].ToString().Trim();

                //if (lvNamespaces.SelectedItems.Count > 0)
                //    lvNamespaces.SelectedItems[0].ToString();


                //if (lvNamespaces.SelectedIndex >= 0)
                //{
                //    var selectedItems = lvNamespaces.SelectedItems;
                //    foreach (Namespaces selectedItem in selectedItems)
                //    {
                //        lvNamespaces.Items.Remove(selectedItem);
                //        break;
                //    }
                //}


                //txtblockDescription.Text = updateCartTotal().ToString();

                // String text = lvNamespaces.SelectedItems[0].ToString();
                //int aq =  lvNamespaces.SelectedIndex;
                //DataRowView CompRow1 = lvNamespaces.Items.GetItemAt(aq) as DataRowView;
                //if (e.AddedItems != null && e.AddedItems.Count > 0)
                //{
                //    DataRow selectedRow = e.AddedItems[0] as DataRow;
                //    if (selectedRow != null)
                //    {
                //        object[] items = selectedRow.ItemArray;
                //        //to sth with these items...
                //    }
                //}



                ////int

                //int SComp = lvNamespaces.SelectedIndex;
                //DataRowView CompRow = lvNamespaces.Items.GetItemAt(SComp) as DataRowView;
                ////string ka = Convert.ToString(CompRow["Namespace"]);



                //ListViewItem namespaceselectedItem = ((sender as ListView).SelectedItem as ListViewItem);

                //ListView lv = e.OriginalSource as ListView;
                //ListViewItem lvi = lv.SelectedItem as ListViewItem;





                ////  versionselecteditem = cmbboxVersion.SelectedItem.ToString();
                ////  namespacecombined = filepath + '\\' + versionselecteditem;
                //if (lvNamespaces.SelectedItem != null)
                // //  namespaceselectedItem =  e.AddedItems[0].ToString();
                //    //namespaceselectedItem = lvNamespaces.SelectedItem.ToString();
                // //  namespaceselectedItem = lvNamespaces.SelectedItem.ToString();


                ////ListView item = lvNamespaces.SelectedItems[0];
                //////fill the text boxes
                ////textBoxID.Text = item.Text;
                ////textBoxName.Text = item.SubItems[0].Text;
                ////textBoxPhone.Text = item.SubItems[1].Text;
                ////textBoxLevel.Text = item.SubItems[2].Text;


                //getfileinformation = filepath + '\\' + namespaceselectedItem;
                //// string d = System.IO.Path.GetDirectoryName(x);
                //if (namespaceselectedItem != null)// && namespaceselectedItem.Length > 0
                //{
                //    FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(getfileinformation);
                //    //MessageBox.Show(myFileVersionInfo.ToString()); 
                //    txtblockDescription.Text = myFileVersionInfo.ToString();
                //}
                //else
                //    txtblockDescription.Text = "";
                #endregion
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //namespaces = null;
                //FirstEntry = false;
                //lvNamespaces.SelectedItem = null;
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvNamespaces.SelectedItems.Count > 0)
            {
                var itm = lvNamespaces.SelectedItems[0];
                //textBox1.Text = itm.SubItems[0].Text;
                //textBox2.Text = itm.SubItems[1].Text;

            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {         

            try
            {             

                foreach (Namespaces item in lvNamespaces.SelectedItems) /*After select all check-boxes, then want to get all listview items here.*/
                {
                    item.IsChecked = true;
                }               

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            #region

            //for (int x = 0; x < lvNamespaces.SelectedItems.Count; x++)
            //{
            //    var du = (Namespaces) as lvNamespaces.SelectedItems[x];
            //    // do your stuff here with du
            //}

            //Namespaces  namespaces1 = (Namespaces)lvNamespaces.SelectedItem;
            //  string[] qw = new string[50];

            //  if(namespaces1.Namespace)

            //  foreach (CheckBox item in namespaces1.IsChecked)
            //  {
            //      if (item.IsChecked == true)
            //      {
            //          System.Windows.MessageBox.Show((item.Content + " is checked."));


            //      }
            //  }


            //var selectedTags = this.lvNamespaces.CheckedItems
            //                     .Cast<ListViewItem>()
            //                     .Select(x => x.Tag);
            //foreach (var tag in selectedTags)
            //{
            //    // do some operation using tag
            //}

            //foreach (ListViewItem item in this.lvNamespaces.)
            //{
            //    var tag = item.Tag;
            //    // do some operation using tag
            //}

            //foreach (CheckBox item in lvNamespaces.Items)
            //{
            //    if (item.IsChecked == true)
            //    {
            //        System.Windows.MessageBox.Show((item.Content + " is checked."));

            //    }
            //}
            #endregion
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                //foreach (Namespaces item in lvNamespaces.SelectedItems)
                //{
                //    item.IsChecked = false;
                //}
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion

        #region Function

        public void GetDirectories(string stringfilepath)
        {
            try
            {
                List<Namespaces> namespaces = new List<Namespaces>();
                DirectoryInfo di = new DirectoryInfo(stringfilepath);
                //FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(stringfilepath);
                //string[] productversion = myFileVersionInfo.ProductVersion.ToString();
                FileInfo[] files = di.GetFiles("*.dll");
                if (files != null && files.Length > 0)
                {
                    foreach (var item in files)
                    {
                        if (item != null && item.Length > 0)
                        {
                            namespaces.Add(new Namespaces() { Namespace = item.ToString() });
                            //  namespaces.Add(new Namespaces() { Selection = false });
                        }
                    }

                    //foreach (var item in myFileVersionInfo)
                    //{
                    //    if (item != null && item.Length > 0)
                    //    {
                    //        namespaces.Add(new Namespaces() { Namespace = item.ToString() });
                    //        //  namespaces.Add(new Namespaces() { Selection = false });
                    //    }
                    //}

                    lvNamespaces.ItemsSource = namespaces;
                }
                else
                {
                    lvNamespaces.Items.Clear();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }
        }

        private decimal updateCartTotal()
        {
            decimal runningTotal = 0;
            foreach (ListViewItem l in lvNamespaces.Items)
            {

            }
            return runningTotal;
        }

        public static void ProcessDirectory(string targetDirectory)
        {

            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
                ProcessFile(fileName);


            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            foreach (string subdirectory in subdirectoryEntries)
                ProcessDirectory(subdirectory);
        }

        public static void ProcessFile(string path)
        {
            Console.WriteLine("Processed file '{0}'.", path);
        }

        public static string[] GetFiles(string directoryPath, string fileExtension)
        {
            if (string.IsNullOrEmpty(fileExtension))
                return Directory.GetFiles(directoryPath);
            else
            {
                if (!fileExtension.StartsWith("*") && !fileExtension.StartsWith("."))
                    fileExtension = "*." + fileExtension;
                else if (!fileExtension.StartsWith("*") && fileExtension.StartsWith("."))
                    fileExtension = "*" + fileExtension;

                return Directory.GetFiles(directoryPath, fileExtension);
            }
        }

        private void GettheselectedItems(string[] vs)
        {
            try
            {
                SelectedItems selectedItems = new SelectedItems();
                selectedItems.Getlistselecteditems(vs);
                this.Hide();
                selectedItems.Show();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void GettheselectedItemsandOtheritems(string[] selectedItems,string[] otheraddeditems)
        {
            try
            {
                SelectedItems selectedItems1 = new SelectedItems();
                selectedItems1.Getlistselecteditems(selectedItems,otheraddeditems);
                this.Hide();
                selectedItems1.Show();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private void GettheselectedItemsandOtheritems1(string[] selectedItems, string[] otheraddeditems)
        {
            try
            {
                SelectedItems selectedItems1 = new SelectedItems();
                selectedItems1.Getlistselecteditems(selectedItems, otheraddeditems);
                this.Hide();
                selectedItems1.Show();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        #endregion

        #region Window Events
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)//siMaximized)
            {
                WindowState = WindowState.Normal;
            }
        }
        #endregion
       
    }
}
