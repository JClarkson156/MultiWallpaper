using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.IsolatedStorage;

namespace MultiWallpaper
{
    public class Storage
    {
        public Storage()
        {
            m_strFolders = "";
        }

        private string m_strFolders;
        private Exception m_Exception;

        public string Folders
        {
            get { return m_strFolders; }
            set { m_strFolders += value; }
        }

        public Exception Exceptions
        {
            get { return m_Exception; }
        }

        public bool? Load()
        {
            try
            {
                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
                {
                    if (!isoStore.FileExists("Folders.txt"))
                    {
                        isoStore.CreateFile("Folders.txt");
                        return false;
                    }

                    using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("Folders.txt", FileMode.Open, isoStore))
                    {
                        using (StreamReader reader = new StreamReader(isoStream))
                        {
                            m_strFolders = reader.ReadToEnd().Replace(Environment.NewLine, "");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                m_Exception = e;
                return null;
            }

            return true;
        }

        public bool Save()
        {
            try
            {
                using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null))
                    using (IsolatedStorageFileStream isoStream = new IsolatedStorageFileStream("Folders.txt", FileMode.Truncate, isoStore))
                        using (StreamWriter writer = new StreamWriter(isoStream))
                            writer.WriteLine(m_strFolders);
            }
            catch (Exception e)
            {
                m_Exception = e;
                return false;
            }

            return true;
        }

        public void SaveLog(float Ratio1, float Ratio2, int imageWidth, int imageHeight, int ScreenWidth, int ScreenHeight, float newWidth, float newHeight)
        {
            var systemPath = System.Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData
            );
            var complete = Path.Combine(systemPath, "files");

            if (!File.Exists(complete))
                File.Create(complete);

            var data = new List<string>();

            data.Add("-----------------------------");
            data.Add($"Ratio 1 : {Ratio1}");
            data.Add($"Ratio 2 : {Ratio2}");
            data.Add($"image width : {imageWidth}");
            data.Add($"image height : {imageHeight}");
            data.Add($"screen width : {ScreenWidth}");
            data.Add($"screen height : {ScreenHeight}");
            data.Add($"new width : {newWidth}");
            data.Add($"new height : {newHeight}");
            data.Add("-----------------------------");

            File.WriteAllLines(complete, data);
        }
    }
}
