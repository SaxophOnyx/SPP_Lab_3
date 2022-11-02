using System.Collections.ObjectModel;

namespace Scanner.Results
{
    public partial class DirectoryResult
    {
        public string FullPath { get; }

        public long FilesSize
        {
            get { return _filesSize; }
            private set { _filesSize = value; }
        }

        public long DirsSize
        {
            get { return _dirsSize; }
            private set { _dirsSize = value; }
        }

        public long TotalSize
        {
            get { return FilesSize + DirsSize; }
        }

        public ReadOnlyCollection<DirectoryResult> NestedDirs { get; private set; }

        public ReadOnlyCollection<FileResult> NestedFiles { get; private set; }

        private long _filesSize;
        private long _dirsSize;
        private List<DirectoryResult> _nestedDirs;
        private List<FileResult> _nestedFiles;


        private DirectoryResult(string fullpath)
        {
            FullPath = fullpath;
        }

        public void PrintConsole()
        {
            PrintConsole(this, 0);
        }

        private void PrintConsole(DirectoryResult dirResult, int offsetCount)
        {
            PrintOffset(offsetCount);
            PrintWithColor("<DIR_BEGIN>", ConsoleColor.Green);
            PrintOffset(offsetCount);
            Console.WriteLine($"FullPath = {dirResult.FullPath}");
            PrintOffset(offsetCount);
            Console.WriteLine($"Files = {dirResult.FilesSize}, Dirs = {dirResult.DirsSize}, Total = {dirResult.TotalSize}");

            if (dirResult.NestedFiles.Count > 0)
            {
                PrintOffset(offsetCount + 1);
                PrintWithColor("<FILES_BEGIN>", ConsoleColor.Blue);

                int i = 0;
                foreach (var file in dirResult.NestedFiles)
                {
                    ++i;
                    PrintOffset(offsetCount + 2);
                    Console.WriteLine($"{i}. Name = {file.FileName}, Size = {file.Size}");
                }

                PrintOffset(offsetCount + 1);
                PrintWithColor("<FILES_END>", ConsoleColor.Blue);
            }

            if (dirResult.NestedDirs.Count > 0)
            {
                PrintOffset(offsetCount + 1);
                PrintWithColor("<NESTED_DIR_BEGIN>", ConsoleColor.DarkYellow);

                foreach (var dir in dirResult.NestedDirs)
                {
                    PrintConsole(dir, offsetCount + 2);
                }

                PrintOffset(offsetCount + 1);
                PrintWithColor("<NESTED_DIR_END>", ConsoleColor.DarkYellow);
            }

            PrintOffset(offsetCount);
            PrintWithColor("<DIR_END>", ConsoleColor.Green);
        }

        private void PrintOffset(int offsetCount)
        {
            for (int i = 0; i < offsetCount; ++i)
                Console.Write(".   ");
        }

        private void PrintWithColor(string str, ConsoleColor color)
        {
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(str);
            Console.ForegroundColor = prev;
        }
    }
}
