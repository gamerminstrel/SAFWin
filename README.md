# SAFWin (Setup And Fix Windows 10)
SAFWin is a (currently Alpha/Beta) standalone application that acts as a one-stop shop for common tasks performed on Windows, mostly for first time setups. 

# Key Features:
-Check for the latest upgrade to Windows 10
-One-Button click shortcuts to commonly used System Settings
-Check for and automatically fix OS corruption
-Download the latest version of the Support Application from your Device Manufacturer (HP, Dell, etc.), as well as for CPU and GPU makers AMD + Intel + nVidia

# Example Use-Cases:==========
-Let's say you just replaced the hard drive on a desktop or a laptop, and have to set everything back up. Once you install Windows 10 and make it to the Desktop, you can run this app and just click the buttons to download the latest updates for windows 10, set the timezone, Download updater utilities from your device's manufacturer, and use the download links directly to Firefox and Chrome.

-Let's say you were handed a laptop from someone else, loaded up with viruses. This cannot remove the viruses, but once an antivirus program has been run successfully this can run the Windows commands to repair Windows 10. 

# Binaries 
This app is a Work In Progress and no builds are provided yet. You will need to compile this yourself. 

# Support
If you have an issue, file a bug report here! 

# Compiling
This app is being made using Visual Studio Community Edition (2019). It currently uses two extensions that need to be installed using the NuGet Extension manager:
-Microsoft.PowerShell.5.ReferenceAssemblies
-System.Management

Once those are added, you should be able to compile and build this application. If not, please make a bug report with more information.
