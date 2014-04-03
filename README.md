Importer Hub for CRYENGINE
==========================

This tool is an easy-to-use texture and mesh importer that allow to improve dramatically the asset pipeline for CRYENGINE.

![alt tag](http://www.guillaume-puyal.com/uploads/Mar-2014/ImporterHub_v0-1_screen2.png)

Download binaries
--
https://github.com/Guillaume201/Importer-Hub-for-CRYENGINE/releases

Be aware, some antivirus may be blocking this program. This is a false positive and you can review the source code here on GitHub. 

Installation
--
Before installing the program, be sure to use a 64 bits version of Windows with the .NET Framework 4.5 installed.
Once downloaded, extract all files in one place.

Note that the Magick.NET-x64.dll file is only required for the conversion of PSD and TGA textures. This means that you can run the program without this dll if you don't need the support of those extensions.

Key features
--
Import files with a simple drag and drop
Import textures from the clipboard
Specify a custom output folder
Custom links allowing the launch of exe, files, folders and web pages with command line arguments
Settings stored after the application close
Multiple file import

Assets import
--
With this tool you can import your files with a simple drag and drop on the application or by selecting them in your folders.
You can also import your textures from the clipboard, simple as a copy and paste!

Technically, this tool converts the textures in an understandable format for the Resource Compiler and opens the CryTif interface without Photoshop or any additional software.
For meshes, the files are directly sent to the Resource Compiler so they share the same limitations. That works great for static meshes in fbx.

The Resource Compiler used is the one defined by the Crytek's Settings Manager tool.

By default, the files are stored in the same directory than the input file. However, you can set a custom output folder affecting all the imported files!
For the textures provided by the clipboard, a dialog box lets you choose the filename and the output folder.

Custom links
--
Not exactly related to the asset pipeline, this tool allow you to define up to six links available directly in the interface.

For each link, you can define its title, its target path and some command line arguments.
This links can be used to open or execute every files, folders and web pages!
Also, you can use the [SDK_FOLDER] variable for retrieve the SDK path defined by the Crytek's Settings Manager.

Supported files
--
Textures: jpg, tga, psd, png, bmp, tif
Meshes: fbx, dae

License and source code
--
The Importer Hub for CRYENGINE is free for any use and open source under Creative Common license (CC BY 4.0).
In short, you are free to do whatever you want as long as you leave the credits.

This tool uses the Crytek's Resource Compiler and the Magick.NET library with the following license: http://magick.codeplex.com/license

You can access to the full source code here on GitHub.

![alt tag](http://www.guillaume-puyal.com/uploads/Mar-2014/ImporterHub_v0-1_screen.png)
