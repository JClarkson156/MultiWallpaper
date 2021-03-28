using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.IO;

namespace MultiWallpaper
{
    class Directories
    {
        public Directories(string[] arrFolders)
        {
            m_arrFolders = arrFolders;

            arrScreens = new string[System.Windows.Forms.Screen.AllScreens.Length];

            m_timer = new Timer();

            m_timer.Interval = 1 * 60 * 60 * 1000; //1 Hour
            m_timer.Elapsed += M_timer_Elapsed;

            Change();
            m_timer.Start();
        }

        private void M_timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Change();
        }

        private string[] m_arrFolders;
        private Timer m_timer;
        private string[] Paths = new string[4]{ ".bmp", ".jpg", ".jpeg", ".png" };
        private string[] arrScreens;
        private List<string> m_arrFiles;

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
                FileSystemInfo[] infos = new DirectoryInfo(m_arrFolders[i]).GetFileSystemInfos();
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
            
            for (int i = 0; i < arrScreens.Length; i++)
            {
                rnd = new Random(rnd.Next());
                arrScreens[i] = m_arrFiles[rnd.Next(0, m_arrFiles.Count - 1)];
            }

            rnd = null;

            Wallpaper.SetDesktopWallpaper(arrScreens);

            m_arrFiles = null;
            m_timer.Start();
        }
    }
}
