/***************************************************************************
 *   FileManager.cs
 *   Part of UltimaXNA: http://code.google.com/p/ultimaxna
 *   Based on code from UltimaSDK: http://ultimasdk.codeplex.com/
 *   
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using System;
using System.IO;
using Microsoft.Win32;
using UltimaXNA.Diagnostics;
#endregion

namespace UltimaXNA.Data
{
    class FileManager
    {
        static private bool _isDataPresent = false;
        static public bool IsUODataPresent
        {
            get { return _isDataPresent; }
        }

        private static string m_FileDirectory;

        static FileManager()
        {
            Logger.Debug("Checking UO path");
            string exePath = @"c:\UltimaOnline\";
                
            if (exePath != null && Directory.Exists(exePath))
            {
                Logger.Debug("UO path seems legit", exePath);

                m_FileDirectory = exePath;
                _isDataPresent = true;
            }
            
            if (m_FileDirectory == null)
            {
                Logger.Fatal("Did not find UO Installation.");
                _isDataPresent = false;
            }
        }

        private static string GetExePath(string subName)
        {
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(string.Format(@"SOFTWARE\{0}", subName));

                if (key == null)
                {
                    key = Registry.CurrentUser.OpenSubKey(string.Format(@"SOFTWARE\{0}", subName));

                    if (key == null)
                    {
                        return null;
                    }
                }

                string path = key.GetValue("ExePath") as string;

                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    path = key.GetValue("Install Dir") as string;

                    if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                    {
                        path = key.GetValue("InstallDir") as string;

                        if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                        {
                            return null;
                        }
                    }
                }

                if (File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                }

                if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                {
                    return null;
                }

                return path;
            }
            catch
            {
                return null;
            }
        }

        public static string GetFilePath(string name)
        {
            if (m_FileDirectory != null)
            {
                name = Path.Combine(m_FileDirectory, name);
                // Fix for opening files which don't exist -Smjert
                if (File.Exists(name))
                    return name;
            }

            return null;
        }

        public static bool Exists(string name)
        {
            try
            {
                name = Path.Combine(m_FileDirectory, name);
                Logger.Debug("Checking if file exists [{0}]", name);

                if (File.Exists(name))
                {
                    return true;
                }

                return false;
            }
            catch { return false; }
        }

        public static bool Exists(string name, int index)
        {
            return Exists(String.Format(name, index));
        }

        public static bool Exists(string name, int index, string type)
        {
            return Exists(String.Format("{0}{1}.{2}", name, index, type));
        }

        public static bool Exists(string name, string type)
        {
            return Exists(String.Format("{0}.{1}", name, type));
        }

        public static FileStream GetFile(string name)
        {
            try
            {
                name = Path.Combine(m_FileDirectory, name);

                return new FileStream(name, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch { return null; }
        }

        public static FileStream GetFile(string name, int index)
        {
            return GetFile(String.Format(name, index));
        }

        public static FileStream GetFile(string name, int index, string type)
        {
            return GetFile(String.Format("{0}{1}.{2}", name, index, type));
        }

        public static FileStream GetFile(string name, string type)
        {
            return GetFile(String.Format("{0}.{1}", name, type));
        }
    }
}