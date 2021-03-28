using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.IsolatedStorage;

namespace MultiWallpaper
{
    class Storage
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
    }
}
