using Autodesk.Revit.UI;
using DWGManager.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DWGManager
{
    /// <summary>
    /// Логика взаимодействия для MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        internal List<DWGFile> DWGFiles { get; set; }
        internal UIDocument _uiDocumnet;
        public MainForm(UIDocument uIDocument )
        {
            _uiDocumnet = uIDocument;
            InitializeComponent();

            NLogU.Set("Read DWG");
            DWGFiles = MainLogic.ReadDWG(_uiDocumnet);
            NLogU.Set("Add DWG to DataGrid");
            this.DataGrid.ItemsSource = DWGFiles;
            this.comboBox.SelectedIndex = 0;
            DeleteButton.PreviewMouseDown += DeleteButton_PreviewMouseDown;
            comboBox.SelectionChanged += ComboBox_SelectionChanged;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboAction();
        }

        private void DeleteButton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            DWGFile fileForRemove = DataGrid.SelectedItem as DWGFile;
            if (fileForRemove != null)
            {
                MainLogic.DeleteDwg(_uiDocumnet, fileForRemove);
                NLogU.Set("Remove dwg from list");
                DWGFiles.Remove(fileForRemove);
                this.DataGrid.ItemsSource = null;
                NLogU.Set("Update datagrid");
                comboAction();

            }
        }
        private void comboAction()
        {
            ComboBoxItem typeItem = (ComboBoxItem)comboBox.SelectedItem;
            string value = typeItem.Content.ToString();
            if (value == "Все")
            {
                this.DataGrid.ItemsSource = DWGFiles;
            }   
            else
            {
                this.DataGrid.ItemsSource = DWGFiles
                .Where(s => s.FileType == value)
                .ToList();
            }
            
        }
    }
}
