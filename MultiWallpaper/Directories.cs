using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.IO;
using System.Drawing;

namespace MultiWallpaper
{
    class Directories
    {
        public Directories(string[] arrFolders, System.Windows.Forms.NotifyIcon notifyIcon)
        {
            m_arrFolders = arrFolders;

            ImagesSetToScreens = new string[System.Windows.Forms.Screen.AllScreens.Length];

            m_timer = new Timer();

            m_timer.Interval = 1 * 30 * 60 * 1000; //1 Hour
            m_timer.Elapsed += M_timer_Elapsed;

            NotifyIcon = notifyIcon;

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
        private string[] Paths = new string[5]{ ".bmp", ".jpg", ".jpeg", ".png", ".jfif" };

        public string[] ImagesSetToScreens { get; set; }

        private List<string> m_arrFiles;

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
            for (int i = 0; i < infos.Length; i++)
            {
                if (infos[i] is DirectoryInfo)
                {
                    files.AddRange(CountFiles(((DirectoryInfo)infos[i]).GetFileSystemInfos()));
                }
                else if (infos[i] is FileInfo)
                {
                    if(Paths.Contains((((FileInfo)infos[i]).Extension).ToLower()))
                    {
                        files.Add(infos[i].FullName);
                    }
                }
            }
            return files;
        }

        public void Change()
        {
            ChooseFiles();

            m_timer.Stop();
            Random rnd = new Random();

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

            NotifyIcon.Text = DateTime.Now.ToString("HH:mm");

            m_arrFiles = null;
            m_timer.Start();
        }
    }
}
