###Handy App Powershell Script No. 1 ###
# Version 1- goal here is to make a simple text file in the %TEMP% directory that contains todays date.
#Let's set up all variables here.
$Date = Get-Date -Format MM.dd.yyyy
$TimeZone = Get-TimeZone
$AboutPC = Get-ComputerInfo | FindStr 'WindowsProductName WindowsVersion CsManufacturer CsModel CsProcessors InstalledMemory'

#$LogFile = "$Env:UserProfile\Desktop\Geek Squad\Work Performed($Date).txt"
$TempFile = "$Env:TEMP\HandyApp.txt"

#check temp for if this file already exists
#if it already exists, then 
Function PrepTextFile
{
	if (!(Test-Path $TempFile))
	{
		New-Item -Path $TempFile -ItemType File -Force
	}
	else
	{
		Clear-Content -Path $TempFile -Force
	}	
}

Function PopulateTextFile
{
	Add-Content -Path $TempFile `
	("=====Today's Date:=====",
	$Date,
	$TimeZone,
	$AboutPC)
	
#	"`n `n")
}
# Run all the functions here. 
PrepTextFile
PopulateTextFile