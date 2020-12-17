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
using System.Windows.Navigation;
using System.Windows.Shapes;
//Libraries I added myself.
using System.Management.Automation; //Lets one use powershell functionality
using System.IO; //Lets one navigate file systems (find and open files)

namespace SAFWin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grab_Info_Button_Click(object sender, RoutedEventArgs e)
        {
            string script_path = Directory.GetCurrentDirectory() + "\\Properties";  //Declare path to the PowerShell Script
            PowerShell ps = PowerShell.Create();    //create an instance of Powershell
            ps.AddScript(script_path + "HandyApp_Date.ps1").Invoke();    //add script to PowerShell instance, and invoke it.

            string temp_file_path = System.IO.Path.GetTempPath() + "\\HandyApp.txt";    //Declare path to %TEMP% directory
            string[] sys_info = System.IO.File.ReadAllLines(System.IO.Path.GetTempPath() + "\\HandyApp.txt");   //read every line of HandyApp.txt as a part of an array
            foreach (string item in sys_info)
                SysInfoText.Text += item;
        }
    }
}
