using System;
using System.IO;

namespace FootballEngine.Helper
{
    public class TryGetFilePath
    {
        private static string _AppDomainPath = AppDomain.CurrentDomain.BaseDirectory;
        private static string _AssemblyPath = new Uri(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath;
        private static string _DirectoryPath = Directory.GetCurrentDirectory();

        /// <summary>
        /// Tries to create a file path. Option: checks if the file exits.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool InProjectDirectory(string fileName, bool createFileIfFileDoesNotExist, out string path)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                path = null;
                return false;
            }

            path = _AppDomainPath;

            for (int i = 0; i < ((path == _DirectoryPath) ? 2 : 3); i++)
            {
                path = Path.GetDirectoryName(path);
            }

            path = Path.Combine(path, fileName);

            if (createFileIfFileDoesNotExist)
            {
                if (!File.Exists(path))
                {
                    try
                    {
                        File.Create(path).Close();
                    }
                    catch (Exception)
                    {
                        path = null;
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool InProjectDirectory(string fileName, string subDirectory, bool createFileIfFileDoesNotExist, out string path)
        {
            if (string.IsNullOrWhiteSpace(subDirectory))
            {
                path = null;
                return false;
            }

            if (!InProjectDirectory(Path.Combine(subDirectory, fileName), createFileIfFileDoesNotExist, out path))
            {
                path = null;
                return false;
            }

            return true;
        }

        public static bool InProjectDirectory(string fileName, string[] subDirectories, bool createFileIfFileDoesNotExist, out string path)
        {
            if (subDirectories == null || subDirectories.Length == 0)
            {
                path = null;
                return false;
            }
            foreach (string subDirectory in subDirectories)
            {
                if (string.IsNullOrWhiteSpace(subDirectory))
                {
                    path = null;
                    return false;
                }
            }

            if (!InProjectDirectory(Path.Combine(Path.Combine(subDirectories), fileName), createFileIfFileDoesNotExist, out path))
            {
                path = null;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Tries to create a file path. Option: checks if the file exits.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool InSolutionDirectory(string fileName, bool createFileIfFileDoesNotExist, out string path)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                path = null;
                return false;
            }

            path = _AppDomainPath;

            for (int i = 0; i < ((path == _DirectoryPath) ? 3 : 4); i++)
            {
                path = Path.GetDirectoryName(path);
            }

            path = Path.Combine(path, fileName);

            if (createFileIfFileDoesNotExist)
            {
                if (!File.Exists(path))
                {
                    try
                    {
                        File.Create(path).Close();
                    }
                    catch (Exception)
                    {
                        path = null;
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool InSolutionDirectory(string fileName, string subDirectory, bool createFileIfFileDoesNotExist, out string path)
        {
            if (string.IsNullOrWhiteSpace(subDirectory))
            {
                path = null;
                return false;
            }

            if (!InSolutionDirectory(Path.Combine(subDirectory, fileName), createFileIfFileDoesNotExist, out path))
            {
                path = null;
                return false;
            }

            return true;
        }

        public static bool InSolutionDirectory(string fileName, string[] subDirectories, bool createFileIfFileDoesNotExist, out string path)
        {
            if (subDirectories == null || subDirectories.Length == 0)
            {
                path = null;
                return false;
            }
            foreach (string subDirectory in subDirectories)
            {
                if (string.IsNullOrWhiteSpace(subDirectory))
                {
                    path = null;
                    return false;
                }
            }

            if (!InSolutionDirectory(Path.Combine(Path.Combine(subDirectories), fileName), createFileIfFileDoesNotExist, out path))
            {
                path = null;
                return false;
            }

            return true;
        }
    }
}  
