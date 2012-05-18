IExtendFramework - an application framework and collection of useful classes.
Written entirely in C#...
WARNING: There are some problems encrypting/decrypting data

CLASS                                             INFORMATION
-----------------------------------------------------------------------------
IExtendFramework
  Plugins
    IPlugin                                  Basic plugin interface
    IPluginManager                           Plugin manager interface
    DefaultPluginManager                     Example and Default plugin manager using IPluginManager interface
    AvailablePlugin                          A Class containing an IPlugin and a filename
    AvailablePluginCollection                a Collection of 'AvailablePlugin's
    GlobalPluginManager                      Global class for managing an IPluginManager and loaded plugins
    StaticPluginInfo                         Contains IPlugin interface path
  Controls
    AdvancedForm                             Form with a border that can be controlled
    InputBox                                  Similar to VB.NET style InputBox() function, except a class. Use with the Show(title, text) function
    AdvancedNotifyIcon                       A NotifyIcon that can have controls added to it
    AdvancedProgressBar                      A Progress Bar with much more customization than the standard one
    AdvancedMessageBox                        Also a TaskDialog, very useful
    AdvancedPopupWindow                      Similar to Outlook's popup window, Two classes to use
    AdvancedTrackBar                         A TrackBar that is easier to use than the standard one
    AdvancedLabel                            Much better label than the standard one
    ChromeProgressbar                        A Google Chrome like progress bar
    AdvancedTextBox                           Contains a "watermark" or "cue"
  Text
    ITextEditor                              Interface for a text editor
    FileExtensionManager                     Manager for file extensions, use for a DTMS (Document Type Management System)
    IFileExtension                           A File Extension Interface
    IDocument                                A Document interface, used in ITextEditor
    Permutation                              A Class for generating string permutations
    StringFactorials                         Gets the Factorial of a string
    INIDocument                              Read/Write .INI Files
    FileOpened                               Delegate for when a file is opened with FileExtensionManager
  Win32
    NativeSubclasser                         A NativeWindow override
    Win32Messages                            Contains some Win32 Messages in Hex, useful when overriding WndProc in controls
  Compression
    Pack                                     PACK compression, can extract, create, and read filenames from a Pack file
    SevenZip                                 7z archive support
    Tar                                      Tar archive support
    GZip                                     GZip (gz) archive support
    Zip                                      Zip archive support
    BZip2                                    BZip2 (bz2) archive support
  Threading
    Thread                                   Useful for creating a thread with a function(sender, e) and invoking on a new thread
  Random
    RandomNumberGenerator : System.Random    Random Generator, inherits System.Random. Will re-seed the RNG periodically, creating a new level of randomness
    RandomDateGenerator                      Generates random dates using RandomNumberGenerator
    RandomStringGenerator                    Generates random strings using RandomNumberGenerator, has a 50% chance of being an uppercase letter
  Net
    Mail
      GmailMessage                           Class for sending email using GMail (see mailer.txt)
      HotmailMessage                         Class for sending email using Hotmail
      YahooMessage                           Class for sending email using Yahoo
      AOLMessage                             Class for sending email using AOL
  Drawing
    XPoint                                   The Point class used in XImage (Contains an X, Y, and Color)
    XmlFormat
      XImage                                 XImage (Xml Image) class, Render doesn't entirely work...
  Encryption
    SampleObjects                            Provide sample, unchanging Keys, IVs, and Generators
    AESProvider                              Provide simple usage for AES
    ASCIIProvider                            A class for using ASCII encryption
    DESProvider                              Provide simple usage for DES
    EncryptionUI                             A Form to test Encryption Classes
    RC2Provider                              Provides simple RC2 usage
    RijndaelProvider                         Provide simple Rijndael encryption
    RSAProvider                              Provide simple RSA Encryption class
    TripleDESProvider                        A TripleDES provider
    XorProvider                              eXclusive OR ('^' in C#). Provides a class for using this to encrypt
    L1F3Provider                             Not an encryption type, but uses multiple types to encrypt
    H34RTProvider                            A custom encryption type, using bit shifting and XOR. Very basic.
  Converter                                  Simple and useful conversion facility
  AssemblyHelper                             Functions for using System.Reflection.Assembly easier
  Utilites                                   Useful functions
  IExtendFrameworkException                  An exception class
  Mathematics
    AdvancedMathProcessor                    An advanced math formula solver
  TypeExtensions                             Type extensions for very many types
  Enums
    EnumHelper                               Helps with the slow built-in enum methods
    EnumStringAttribute                      An attribute for specifying a way to get enum to readable string
    EnumToStringExtension                    Actually in TypeExtensions.cs...
   AdvancedString                            A much faster, mutable version of System.String, with no unmanaged code, and more built-in functions
  Events                                     
    CommonEventHandler                       Can listen to all types of events of all types of objects
  Xml
    Sgml                                     (namespace) contains stuff for parsing Xml/Html using XmlReader
    Html                                     (namespace) contains stuff for parsing and creating Html documents
  School
    Grade/Grades                             Provides a way to manipulate grades (from Preschool-12th grade)
LICENSE INFO: 
CPOL (CodeProject Open License): AdvancedForm (gTitleBar), AdvancedLabel (gLabel), AdvancedTrackBar (gTrackBar) Copyright (C) 2009
CPOL (CodeProject Open License): ChromeProgressBar Copyright (C) 2011
CPOL (CodeProject Open License): AdvancedNotifyIcon (WNotifyIcon) Copyright (C) 2009
CPOL (CodeProject Open License): AdvancedPopupWindow Copyright (C) 2011 by Simon B.
CPOL (CodeProject Open License): AdvancedProgressBar (ProgBarPlus) Copyright (C) 2008
CPOL (CodeProject Open License): AdvancedMessageBox (WPFTaskDialog) (C) Sean A. Hanley 2010
CPOL (CodeProject Open License): INIDocument (TA_INIDocument)
CPOL (CodeProject Open License): EnumHelper (Enum) Ideafixxxer 2011 (see notice in file)
CPOL : AdvancedTextBox (AdvancedTextBox/ChreneLib)
CPOL : CommonEventHandler (EventHandlerFactory)

Freeware: UnRar archiving (NUnrar http://nunrar.codeplex.com)

Compression: BZip2, GZip, Tar under LGPL License from DotNetFireball (dotnetfireball.org), ICSharpCode.SharpZipLib
7z compression from 7zip (7-zip.org), DotNetFireball (dotnetfireball.org)
Zip Compression from ICSharpCode.SharpZipLib, DotNetFireball - LGPL License (dotnetfireball.org)
Win32.NativeSubclasser under LGPL License - dotnetfireball.org

Some of the TypeExtensions have the MIT License: http://www.opensource.org/licenses/mit-license.php
Some of the TypeExtensions have the BSD License: http://www.opensource.org/licenses/bsd-license.php
Inflector also is BSD license, and comes from the Subsonic Project http://subsonicproject.com

Everything else has the WTFPL license and was probably created by me (Elijah Frederickson)
Just dont kill yourself with it.