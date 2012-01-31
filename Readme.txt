IExtendFramework - an application framework and collection of useful classes.
Written entirely in C#...
WARNING: There are some problems encrypting/decrypting data

CLASS                                             INFORMATION
-----------------------------------------------------------------------------
IExtendFramework
  Plugins
X   IPlugin                                  Basic plugin interface
X   IPluginManager                           Plugin manager interface
X   DefaultPluginManager                     Example and Default plugin manager using IPluginManager interface
X   AvailablePlugin                          A Class containing an IPlugin and a filename
X   AvailablePluginCollection                a Collection of 'AvailablePlugin's
X   GlobalPluginManager                      Global class for managing an IPluginManager and loaded plugins
X   StaticPluginInfo                         Contains IPlugin interface path
  Controls
X   AdvancedForm                             Form with a border that can be controlled
X   InputBox                                 Similar to VB.NET style InputBox() function, except a class. Use with the Show(title, text) function
X   AdvancedNotifyIcon                       A NotifyIcon that can have controls added to it
X   AdvancedProgressBar                      A Progress Bar with much more customization than the standard one
X   AdvancedMessageBox                       Also a TaskDialog, very useful
X   AdvancedPopupWindow                      Similar to Outlook's popup window, Two classes to use
X   AdvancedTrackBar                         A TrackBar that is easier to use than the standard one
X   AdvancedLabel                            Much better label than the standard one
X   ChromeProgressbar                        A Google Chrome like progress bar
X   AdvancedTextBox                          Contains a "watermark" or "cue"
  Text
X   ITextEditor                              Interface for a text editor
X   FileExtensionManager                     Manager for file extensions, use for a DTMS (Document Type Management System)
X   IFileExtension                           A File Extension Interface
X   IDocument                                A Document interface, used in ITextEditor
X   Permutation                              A Class for generating string permutations
X   StringFactorials                         Gets the Factorial of a string
X   INIDocument                              Read/Write .INI Files
X   FileOpened                               Delegate for when a file is opened with FileExtensionManager
  Compiler
X   ILanguage                                Interface for a language
X   IParser                                  Interface for a parser
X   ICompiler                                Interface for a compiler
  Win32
X   NativeSubclasser                         A NativeWindow override
X   Win32Messages                            Contains some Win32 Messages in Hex, useful when overriding WndProc in controls
  Compression
X   Pack                                     PACK compression, can extract, create, and read filenames from a Pack file
X   SevenZip                                 7z archive support
X   Tar                                      Tar archive support
X   GZip                                     GZip (gz) archive support
X   Zip                                      Zip archive support
X   BZip2                                    BZip2 (bz2) archive support
  Threading
X   Thread                                   Useful for creating a thread with a function(sender, e) and invoking on a new thread
  Random
X   RandomNumberGenerator : System.Random    Random Generator, inherits System.Random. Will re-seed the RNG periodically, creating a new level of randomness
X   RandomDateGenerator                      Generates random dates using RandomNumberGenerator
X   RandomStringGenerator                    Generates random strings using RandomNumberGenerator, has a 50% chance of being an uppercase letter
  Net
    Mail
X     GmailMessage                           Class for sending email using GMail (see mailer.txt)
X     HotmailMessage                         Class for sending email using Hotmail
X     YahooMessage                           Class for sending email using Yahoo
X     AOLMessage                             Class for sending email using AOL
  Drawing
X   XPoint                                   The Point class used in XImage (Contains an X, Y, and Color)
    XmlFormat
X     XImage                                 XImage (Xml Image) class, Render doesn't entirely work...
  Encryption
X   SampleObjects                            Provide sample, unchanging Keys, IVs, and Generators
X   AESProvider                              Provide simple usage for AES
X   ASCIIProvider                            A class for using ASCII encryption
X   DESProvider                              Provide simple usage for DES
X   EncryptionUI                             A Form to test Encryption Classes
X   RC2Provider                              Provides simple RC2 usage
X   RijndaelProvider                         Provide simple Rijndael encryption
X   RSAProvider                              Provide simple RSA Encryption class
X   TripleDESProvider                        A TripleDES provider
X   XorProvider                              eXclusive OR ('^' in C#). Provides a class for using this to encrypt
X   L1F3Provider                             Not an encryption type, but uses multiple types to encrypt
X   H34RTProvider                            A custom encryption type, using bit shifting. Very basic.
X Converter                                  Simple and useful conversion facility
X AssemblyHelper                             Functions for using System.Reflection.Assembly easier
X Utilites                                   Useful functions
X IExtendFrameworkException                  An exception class
  Mathematics
X   AdvancedMathProcessor                    An advanced math formula solver
X TypeExtensions                             Type extensions for very many types
  Enums
X   EnumHelper                               Helps with the slow built-in enum methods
X   EnumStringAttribute                      An attribute for specifying a way to get enum to readable string
X   EnumToStringExtension                    Actually in TypeExtensions.cs...
X  AdvancedString                            A much faster, mutable version of System.String, with no managed code, and more built-in functions
  Events                                     
    CommonEventHandler                       Can listen to all types of events of all types of objects
  Xml
    Sgml                                     (namespace) contains stuff for parsing Xml/Html using XmlReader
    Html                                     (namespace) contains stuff for parsing and creating Html documents
LICENSE INFO: 
CPOL (CodeProject Open License): AdvancedForm (gTitleBar), AdvancedLabel (gLabel), AdvancedTrackBar (gTrackBar) Copyright (C) 2009
CPOL (CodeProject Open License): ChromeProgressBar Copyright (C) 2011
CPOL (CodeProject Open License): AdvancedNotifyIcon (WNotifyIcon) Copyright (C) 2009
CPOL (CodeProject Open License): AdvancedPopupWindow Copyright (C) 2011 by Simon B.
CPOL (CodeProject Open License): AdvancedProgressBar (ProgBarPlus) Copyright (C) 2008
CPOL (CodeProject Open License): AdvancedMessageBox (WPFTaskDialog) (C) Sean A. Hanley 2010
CPOL (CodeProject Open License): INIDocument (TA_INIDocument)
CPOL (CodeProject Open License): EnumHelper (Enum) Ideafixxxer 2011 (see notice in file)
CPOL : AdvancedTextBox (CTextBox/ChreneLib)
CPOL : CommonEventHandler (EventHandlerFactory)

Freeware: UnRar archiving (Nunrar http://nunrar.codeplex.com)

Compression: BZip2, GZip, Tar under LGPL License from DotNetFireball (dotnetfireball.org)
7z compression from 7zip (7-zip.org), DotNetFireball (dotnetfireball.org)
Zip Compression from ICSharpCode.SharpZipLib, DotNetFireball - LGPL License (dotnetfireball.org)
Win32.NativeSubclasser under LGPL License - dotnetfireball.org

Some of the TypeExtensions have the MIT License: http://www.opensource.org/licenses/mit-license.php
Some of the TypeExtensions have the BSD License: http://www.opensource.org/licenses/bsd-license.php
Inflector also is BSD license, and comes from the Subsonic Project http://subsonicproject.com

Everything else has the WTFPL license and was created by me (Elijah Frederickson)
Just dont kill yourself with it.