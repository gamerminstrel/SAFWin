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
using System.Globalization; //Lets one grab the TimeZone
using System.Collections; //lets me use the Environment example & library
using System.Management; //Lets one grab system info like CPU model, RAM, etc.

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
            //GetSystemInfo_PowerShell();   //BROKEN
            GetSystemInfo_CSharp();
        }

        private void Grab_Info_Button_Click(object sender, RoutedEventArgs e)
        {
            //string script_path = Directory.GetCurrentDirectory() + "\\Properties";  //Declare path to the PowerShell Script
            PowerShell ps = PowerShell.Create();    //create an instance of Powershell
            string ps_script = Directory.GetCurrentDirectory() + "\\Properties\\HandyApp_Date.ps1";
            ps.AddScript(System.IO.File.ReadAllText(ps_script)).Invoke();
            //ps.AddScript(script_path + "\\HandyApp_Date.ps1").Invoke();    //add script to PowerShell instance, and invoke it.

            string temp_file_path = System.IO.Path.GetTempPath() + "\\HandyApp.txt";    //Declare path to %TEMP% directory
            string[] sys_info = System.IO.File.ReadAllLines(System.IO.Path.GetTempPath() + "\\HandyApp.txt");   //read every line of HandyApp.txt as a part of an array
            foreach (string item in sys_info)
                SysInfoText.Text += item;
            //SysInfoText.Text = "Script Path is: \n" + script_path + "\\HandyApp_Date.ps1";
        }

        void EnvironmentInfo()
        {
            SysInfoText.Text = "System Info:";
            // create management class object
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            //collection to store all management objects
            ManagementObjectCollection moc = mc.GetInstances();
            if (moc.Count != 0)
            {
                foreach (ManagementObject mo in mc.GetInstances())
                {
                    // display general system information
                    SysInfoText.Text +="\nManufacturer: \t" + mo["Manufacturer"].ToString();
                }
            }
        }
        void example_Environment()
        {
            string str;
            string nl = Environment.NewLine;
            //
            
            Console.WriteLine("-- Environment members --");

            //  Invoke this sample with an arbitrary set of command line arguments.
            Console.WriteLine("CommandLine: {0}", Environment.CommandLine);

            string[] arguments = Environment.GetCommandLineArgs();
            Console.WriteLine("GetCommandLineArgs: {0}", String.Join(", ", arguments));

            //  <-- Keep this information secure! -->
            Console.WriteLine("CurrentDirectory: {0}", Environment.CurrentDirectory);

            Console.WriteLine("ExitCode: {0}", Environment.ExitCode);

            Console.WriteLine("HasShutdownStarted: {0}", Environment.HasShutdownStarted);

            //  <-- Keep this information secure! -->
            Console.WriteLine("MachineName: {0}", Environment.MachineName);

            Console.WriteLine("NewLine: {0}  first line{0}  second line{0}  third line",
                                  Environment.NewLine);

            Console.WriteLine("OSVersion: {0}", Environment.OSVersion.ToString());

            Console.WriteLine("StackTrace: '{0}'", Environment.StackTrace);

            //  <-- Keep this information secure! -->
            Console.WriteLine("SystemDirectory: {0}", Environment.SystemDirectory);

            Console.WriteLine("TickCount: {0}", Environment.TickCount);

            //  <-- Keep this information secure! -->
            Console.WriteLine("UserDomainName: {0}", Environment.UserDomainName);

            Console.WriteLine("UserInteractive: {0}", Environment.UserInteractive);

            //  <-- Keep this information secure! -->
            Console.WriteLine("UserName: {0}", Environment.UserName);

            Console.WriteLine("Version: {0}", Environment.Version.ToString());

            Console.WriteLine("WorkingSet: {0}", Environment.WorkingSet);

            //  No example for Exit(exitCode) because doing so would terminate this example.

            //  <-- Keep this information secure! -->
            string query = "My system drive is %SystemDrive% and my system root is %SystemRoot%";
            str = Environment.ExpandEnvironmentVariables(query);
            Console.WriteLine("ExpandEnvironmentVariables: {0}  {1}", nl, str);

            Console.WriteLine("GetEnvironmentVariable: {0}  My temporary directory is {1}.", nl,
                                   Environment.GetEnvironmentVariable("TEMP"));

            Console.WriteLine("GetEnvironmentVariables: ");
            IDictionary environmentVariables = Environment.GetEnvironmentVariables();
            foreach (DictionaryEntry de in environmentVariables)
            {
                Console.WriteLine("  {0} = {1}", de.Key, de.Value);
            }

            Console.WriteLine("GetFolderPath: {0}",
                         Environment.GetFolderPath(Environment.SpecialFolder.System));

            string[] drives = Environment.GetLogicalDrives();
            Console.WriteLine("GetLogicalDrives: {0}", String.Join(", ", drives));
        }
        void GetSystemInfo_PowerShell()
        {
            /*
             * This Method opens a Powershell instance and runs commands through to gather System Info.
             * Once gathered, it converts into an array and formats/outputs into the TextBox.
             */

            SysInfoText.Text = "System Info:";
            PowerShell ps = PowerShell.Create();
            List<string> sysinfo_list = new List<string>(); //this variable will contain all system info.

            //This will run a single command to Powershell then output its results back into the sysinfo_list variable.
            //foreach (PSObject result in ps.AddCommand("Get-Date").Invoke())
            //{
            //    sysinfo_list.Add(result.ToString());
            //}
            sysinfo_list.Add(ps.AddCommand("Get-Date").Invoke().ToString());
            sysinfo_list.Add(ps.AddCommand("Get-TimeZone").Invoke().ToString());

            ////Same as above butfor getting TimeZone
            //foreach (PSObject result in ps.AddCommand("Get-TimeZone").Invoke())
            //{
            //    sysinfo_list.Add(result.ToString());
            //}

            String[] sysinfo_str_array = sysinfo_list.ToArray();    //this converts the List into an array that can be formatted in SysInfoText.Text
            //foreach (string item in sysinfo_str_array)
            foreach (string item in sysinfo_str_array)
            {
                SysInfoText.Text += ("\n", item);
                //MessageBox.Show(string.Join(Environment.NewLine, sysinfo_str_array));
            }
        }

        void GetSystemInfo_CSharp()
        {
            /*The point here is to gather as much important System Info as Possible using c#
             * 
             */
            SysInfoText.Text = "System Information:";
            List<string> sysinfo_list = new List<string>();
            TimeZoneInfo localZone = TimeZoneInfo.Local;
            DateTime currentDate = DateTime.Now;

            SysInfoText.Text += "\n Today's Date:\t " + currentDate;
            SysInfoText.Text += "\n Local TimeZone:\t " + localZone.DisplayName;
            SysInfoText.Text += "\n Machine Name:\t " + Environment.MachineName;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("Powershell.exe", "dism /online /cleanup-image /restorehealth; sfc /scannow; Read-Host -Prompt 'press any key to continue'");
            //System.Diagnostics.Process.Start("PowerShell.exe", "Get-TimeZone; Get-Date; Read-Host -Prompt 'press any key to continue'"); //demo line to use in PowerShell
        }

        private void DL_WinUpdater_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://go.microsoft.com/fwlink/?LinkID=799445");
        }

        private void Check_For_Updates_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("ms-settings:appsfeatures");
        }
  

        private void Intel_Updater_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://downloadmirror.intel.com/28425/a08/Intel-Driver-and-Support-Assistant-Installer.exe");
        }

        private void AMD_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.amd.com/en/support");
        }

        private void NVidia_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.nvidia.com/en-us/geforce/drivers/");
        }

        private void HP_SupportAssistant_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www8.hp.com/us/en/campaigns/hpsupportassistant/hpsupport.html");
        }

        private void HP_Support_Page_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://support.hp.com/us-en/drivers");
        }

        private void Dell_SupportAssist_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://downloads.dell.com/serviceability/catalog/SupportAssistInstaller.exe");
        }

        private void Dell_Support_Page_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.dell.com/support/home/en-us");
        }

        private void Lenovo_Vantage_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.microsoft.com/en-us/p/lenovo-vantage/9wzdncrfj4mv?activetab=pivot:overviewtab");
        }

        private void Lenovo_Support_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://support.lenovo.com/us/en/");
        }

        private void Chrome_Download_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com/chrome/");
        }

        private void Firefox_Download_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.mozilla.org/firefox/download/thanks/");
        }

        private void ASUS_Support_Page_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.asus.com/support/Download-Center/");
        }

        private void Customizer_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(@"\\10.37.185.242\gsiso");
        }

        private void Reset_Chrome_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("Chrome.exe", "chrome://settings/");
        }

        private void Set_TimeZone_Button_Click(object sender, RoutedEventArgs e)
        {
            PowerShell ps = PowerShell.Create();
            ps.AddCommand("Set-TimeZone")
                .AddParameter("Id", "Eastern Standard Time")
                .Invoke();
            //Set - TimeZone - Id "Eastern Standard Time"
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e)
        {
            GetSystemInfo_CSharp();
        }

        private void Test_Button_Click(object sender, RoutedEventArgs e)
        {
            EnvironmentInfo();
        }
    }

}
