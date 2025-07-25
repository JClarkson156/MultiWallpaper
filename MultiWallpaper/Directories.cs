﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.IO;
using System.Drawing;

namespace MultiWallpaper
{
    class Directories
    {
        public Directories(string[] arrFolders)//, System.Windows.Forms.NotifyIcon notifyIcon)
        {
            m_arrFolders = arrFolders;

            ImagesSetToScreens = new string[System.Windows.Forms.Screen.AllScreens.Length];

            m_timer = new Timer();

            m_timer.Interval = 1 * 30 * 60 * 1000; //1 Hour
            m_timer.Elapsed += M_timer_Elapsed;

            //NotifyIcon = notifyIcon;

            store = new Storage();

            Change();
            m_timer.Start();

        }

        private void M_timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Change();
        }

        public System.Windows.Forms.NotifyIcon NotifyIcon;
        private string[] m_arrFolders;
        private Timer m_timer;
        private string[] Paths = new string[6]{ ".bmp", ".jpg", ".jpeg", ".png", ".jfif", ".webp" };

        public string[] ImagesSetToScreens { get; set; }

        private List<string> m_arrFiles;

        private Storage store;

        public int ScreenCount
        {
            get
            {
                return ImagesSetToScreens.Length;
            }
        }
        public void Reset(string[] arrFolders)
        {
            m_arrFolders = arrFolders;

            Change();
        }

        private void ChooseFiles()
        {
            m_arrFiles = new List<string>();

            for (int i = 0; i < m_arrFolders.Length; i++)
            {
                FileSystemInfo[] infos = new DirectoryInfo(m_arrFolders[i].Trim()).GetFileSystemInfos();
                m_arrFiles.AddRange(CountFiles(infos));
            }
        }

        private List<string> CountFiles(FileSystemInfo[] infos)
        {
            List<string> files = new List<string>();
            var dateCheck = DateTime.Now.AddMonths(-6);
            for (int i = 0; i < infos.Length; i++)
            {
                if (infos[i] is DirectoryInfo)
                {
                    files.AddRange(CountFiles(((DirectoryInfo)infos[i]).GetFileSystemInfos()));
                }
                else if (infos[i] is FileInfo)
                {
                    if(Paths.Contains((((FileInfo)infos[i]).Extension).ToLower()) &&
                        ((((FileInfo)infos[i]).LastAccessTime < dateCheck) ||
                            (((FileInfo)infos[i]).LastAccessTime >= dateCheck && ((FileInfo)infos[i]).Attributes == (FileAttributes)5242912))
                    )
                    {
                        files.Add(infos[i].FullName);
                    }
                }
            }
            return files;
        }

        private string GetNextFile(Random rnd)
        {
            var dateCheck = DateTime.Now.AddDays(-7);

            while (true)
            {
                var nextFile = m_arrFiles[rnd.Next(0, m_arrFiles.Count - 1)];
                var nextFileInfo = new FileInfo(nextFile);
                if (nextFileInfo.LastAccessTime > dateCheck &&
                    (nextFileInfo.CreationTime < dateCheck || nextFileInfo.LastWriteTime < dateCheck)
                )
                    return nextFile;
            }
        }

        public void Change()
        {
            ChooseFiles();

            m_timer.Stop();
            Random rnd = new Random();

            try
            {
                if (m_timer.Interval == 120000)
                    m_timer.Interval = 1 * 30 * 60 * 1000;

                if (m_arrFiles.Count != 0)
                {

                    for (int i = 0; i < ImagesSetToScreens.Length; i++)
                    {
                        rnd = new Random(rnd.Next());
                        ImagesSetToScreens[i] = m_arrFiles[rnd.Next(0, m_arrFiles.Count - 1)];
                    }

                    rnd = null;

                    Wallpaper.SetDesktopWallpaper(ImagesSetToScreens);

                    /*var wallpaper = (IDesktopWallpaper)(new DesktopWallpaperClass());
                    for (uint i = 0; i < wallpaper.GetMonitorDevicePathCount(); i++)
                    {
                        rnd = new Random(rnd.Next());
                        var monitorId = wallpaper.GetMonitorDevicePathAt(i);
                        wallpaper.SetWallpaper(monitorId, m_arrFiles[rnd.Next(0, m_arrFiles.Count - 1)]);
                    }*/
                }

                if (NotifyIcon != null)
                    NotifyIcon.Text = DateTime.Now.ToString("HH:mm");
            }
            catch 
            {
                m_timer.Interval = 120000;
            }

            m_arrFiles = null;
            m_timer.Start();

            store.SaveData(ImagesSetToScreens);
        }
    }
}
