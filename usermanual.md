---
layout: default
title: User Manual
permalink: /usermanual/
---

## User Manual

### Table of Contents

* [Introduction](#introduction)
* [Getting Started](#getting_started)
* [Tutorial](#tutorial)
* [Hints and Tips](#hints_and_tips)

### Introduction

Have you ever downloaded a zip or rar file only to have it tell you that the file has become corrupted and cannot be opened? This happens because the file has become corrupted. Most compression programs have built in error detection using the same CRC technology that UltraSFV uses. Compressed files can be completely destroyed by even a small file error. Most other files will not warn you if they have become corrupted.

If you download many files then your computer probably has numerous corrupted files that you didn't even know about. UltraSFV can help solve this issue. UltraSFV brings the continence of Windows to SFV based file error detection.

**UltraSFV is extremely easy to use!** To check files using an SFV simply double click on an SFV file listed in My Computer or Windows Explorer, drag and drop an SFV or CRCed onto UltraSFV, or use the standard Open dialog box. The main UltraSFV window features a list with the names, CRCs, and file status information of all files in the open SFV.

### Getting Started

This Guide introduces some file error detection terms, describes some of the initial steps in installing UltraSFV, and provides a first look at using some UltraSFV features. For additional information, see the tutorials that come with UltraSFV, the UltraSFV help file, and the UltraSFV web site at http://www.ultrasfv.com/.

#### What is an SFV file?

The extension SFV stands for Simple File Verification. SFV files are error detection tools used for verifying file integrity and checking for corruption that can occur when files are transferred over unreliable channels, such as the internet, Local Area Network (LAN) or when burning CD or DVD discs.

They contain 32-bit CRC information for one or more files. CRC stands for Cyclic Redundancy Check and is essentially a fingerprint of the binary data a file contains. When a file is corrupted, the binary data in a file is changed. The file may be the exact same size but the data inside has been altered. This kind of corruption will cause the file CRC to change.

#### What does UltraSFV do?

UltraSFV works by calculating the CRC for a file and comparing it to the value listed in the SFV or embedded in the file name. If the values are identical, then the file is okay, if the values are different then the file has become corrupted.

#### Using UltraSFV

Here is a brief introduction to the features in UltraSFV.

**Checking files using an SFV file:**

1. In the UltraSFV window, select **Open SFV** from the **File** menu.
2. A dialog will appear, browse to the directory with the SFV file you want to use and open it.

UltraSFV will begin processing the files immediately. For large files this may take some time. As the files are processed the results will be displayed in the UltraSFV main window.

The SFV file must be in the same directory as the files it references.

**Creating an SFV file:**

1. In the UltraSFV window, select **New SFV** from the **File** menu.
2. A dialog will appear, browse to the directory containing the files you want to lock. Select the directory and click OK.

All files in the selected directory will have their CRC values calculated and saved to the new SFV file. The SFV file will be named after the directory. So if you chose a directory named "My Music" then the SFV file will be named "My Music.sfv".

It is important not to stop this process, you can pause it and resume it later but if you stop it the SFV file will not be created.

### Tutorial

This brief overview tutorial introduces you to each of the main UltraSFV concepts and features to get you working productively as quickly as possible.

#### Introducing UltraSFV

UltraSFV is a file error detection tool, used to check files for corruption that may happen to downloaded or burned files.

#### The UltraSFV window

![UltraSFV Main Window](/images/ultrasfv_mainscreen.jpg)

The UltraSFV window is where you start many of your operations. You can open it through the Start menu. The UltraSFV window also opens automatically if you double click on an SFV file in My Computer or Windows Explorer.

The UltraSFV window includes standard Windows components. Of particular interest:

The **Title Bar** displays not just the UltraSFV product title, but the name of the SFV you are currently working with. The **Main Window Area** displays information about the files you have processed. Finally, the **Status** Line displays the number and status of the files, along with other information.

#### Checking files with an SFV

The first step is to open an SFV file. Select <b>Open SFV</b> from the UltraSFV <b>File</b> pull-down menu to activate the standard Open SFV dialog box.

Then, select the file you want to open in the Open SFV dialog box. This dialog box works just like the Open dialog boxes in other applications: just select the file you want to open from the list of files. (If you want to create an SFV instead of working with an existing one, see the section Tutorial - Creating a new SFV file).

You can also simply double-click the SFV file in My Computer or Windows Explorer, or just drag and drop the file onto the UltraSFV main window.

Once you load an SFV file the program will begin processing it. The files in an SFV are listed in a list box in the main UltraSFV window.

#### Checking files without an SFV

Many people are now distributing files with CRC checksums embedded directly into the file name. These files do not need to be accompanied by and SFV file. UltraSFV fully supports this method of file verification.

You may have seen files with an eight-digit group of alpha-numeric characters wrapped in brackets (e.g. My_File_\[D8E0BAE0\].avi). These are files with CRC information in the name. It is important that you don't delete this information from the file name so that you can check them again later.

Checking these files is even easier than using an SFV. Select **Check Directory** or **Check File** from the **CRC Check** pull-down menu to activate the standard dialog box. Then select the directory or file you want to check.

You can also drag and drop these files onto the UltraSFV main window.

#### Creating a new SFV file

You can create a new SFV file by selecting **New SFV** from the UltraSFV **File** pull-down menu. This activates the New SFV dialog box.

Select the directory containing the files you want to calculate CRC information for and click OK. All files in the directory will be included in the SFV file.

Do not stop this process. You can pause it and resume the process, however if you click stop the SFV file will not be saved.

#### Adding CRC information to file names

To embed CRC checksums in file names simply select **Add CRC To Names** in the UltraSFV **Tools** pull-down menu.

The standard directory browser dialog will open. Browse to the directory containing the files you want to append CRCs to and click OK.

The program will automatically start processing the files and adding CRC information to the file names.

#### Creating an SFV file from file name CRCs

If you want to create an SFV file for files that have already have CRC information embedded in their file name, you can save a lot of processing time by simply using those CRCs. This saves you the time of recalculating CRCs for each file.

Select **SFV From Names** from the UltraSFV **Tools** pull-down menu. The standard folder browser dialog will open. Select the folder containing the files that you want to create the SFV for and click OK.

The program will very quickly process the files and save the SFV.

### Hints and Tips

* FV files can be invaluable when burning files to CDs or DVDs. Before you burn your disc, create and sfv for all of the files you will be adding. Then burn the files (and the SFV(s) to the disc. After the burning process has completed, check the files with UltraSFV to make sure they were burned successfully. You can now periodically check the disc for errors that may happen with age or usage or damage to the surface of the disc.
* Whenever possible you should append CRC information to file names instead of creating SFV files. Only use an SFV file for files where you should not alter the file names.
* You can add or remove columns from the main UltraSFV list box by checking or unchecking the associated checkboxes in the Options.
* SFVs and CRC are not a replacement for virus scanners. Be sure to check everything you download with a high quality virus scanner.
* To switch to another program during an archive operation, click on the window for the other program, or hold down the Alt key and press the Tab key repeatedly until the program's icon is selected.
* You can check for and install new versions of UltraSFV by selecting the Check For Updates item from the Tools menu.
