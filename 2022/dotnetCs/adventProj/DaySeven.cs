namespace adventProj
{
    using System.Collections;
    using System.Collections.Generic;

    internal class DaySeven : DayTemplate
    {

        internal override object GetAnswer(string testInput)
        {
            uint retVal = 0;

            //if (String.IsNullOrEmpty(testInput))
            {
                testInput = "$ cd / \n$ ls \ndir a\n14848514 b.txt \n8504156 c.dat \ndir d\n" +
                    "$ cd a \n$ ls \ndir e \n29116 f \n2557 g\n62596 h.lst\n" +
                    "$ cd e \n$ls \n584 i \n$ cd .. \n$ cd .. \n$ cd d \n$ls \n" +
                    "4060174 j \n8033020 d.log  \n5626152 d.ext  \n7214296 k";
            }

            FileSystem fs = FileSystem.BuildFileSystem(testInput);

            var filteredDirs = fs.FilterDirectories(100000);

            foreach(var dir in filteredDirs)
            {
                retVal += dir.Size;
            }

            return retVal;
        }
    }

    internal class FileSystem
    {
        private FileSystemNode Root
        { get; set; }

        public FileSystem()
        {
            Root = FileSystemNode.CreateRootNode();
            CurrentDirectory = Root;
        }

        public FileSystemNode CurrentDirectory
        { get; private set; }

        public void MoveToRoot()
        {
            MoveToDirectory("/");
        }

        public IEnumerable<FileSystemNode> FilterDirectories(uint maxSize)
        {
            List<FileSystemNode> matchingDirectories = new List<FileSystemNode>();

            if (this.CurrentDirectory.Size <= maxSize)
            {
                matchingDirectories.Add(this.CurrentDirectory);
            }

            // Doesn't currently include subdir size in parent dir size
            foreach (string childName in this.CurrentDirectory.GetChildrenNames())
            {
                if (this.CurrentDirectory.ChildDirectoryExists(childName))
                {
                    this.MoveToDirectory(childName);
                    var matchingSubDirs = this.FilterDirectories(maxSize);
                    matchingDirectories.AddRange(matchingSubDirs);
                    this.MoveToDirectory("..");
                }
            }

            return matchingDirectories;
        }

        public void MoveToDirectory(string name)
        {
            // If name not null, and is a special character
            // special chars = / . ..
            // current
            // if name not null, and is in the children names, and is directory
            // 
            // child = GetChildNode(name)
            // if child.IsDirectory 
            // CurrentDirectory = child;

            if (!string.IsNullOrWhiteSpace(name))
            {
                switch (name)
                {
                    case "/":
                        CurrentDirectory = Root;
                        break;
                    case ".":
                        // no op; keep current directory the same
                        break;
                    case "..":
                        if (CurrentDirectory.Parent != null)
                        {
                            CurrentDirectory = CurrentDirectory.Parent;
                        }
                        break;
                    default:
                        // is there a child with that name that is a directory?
                        var child = CurrentDirectory.GetChild(name);
                        if (child != null && child.IsDirectory)
                        {
                            CurrentDirectory = child;
                        }
                        else
                        {
                            Console.WriteLine($"No such child directory: {name}");
                        }
                        break;
                }
            }

        }

        public static FileSystem BuildFileSystem(string shellCommands)
        {
            string commandPrompt = "$";
            FileSystem fs = new FileSystem();

            string[] terminalInput = shellCommands.Split("\n");
            //bool processingListOutput = false;
            foreach (string input in terminalInput)
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    // Not a lot of sanity checking here.
                    // You could have a list of files after cd \
                    // but command processing isn't the point (for now)
                    string[] commandAndParams = input.Split(" ");

                    // All the input has either two or three tokens separated by spaces
                    if (commandAndParams.Any() &&
                        !string.IsNullOrWhiteSpace(commandAndParams[0]) &&
                        commandAndParams[0].Any() &&
                        commandAndParams.Count() > 1 &&
                        !string.IsNullOrWhiteSpace(commandAndParams[1]))
                    {
                        if (commandAndParams[0].StartsWith(commandPrompt))
                        {

                            switch (commandAndParams[1])
                            {
                                case "cd":
                                    if (commandAndParams.Count() > 2)
                                    {
                                        // could be /  . .. or <child directory name>
                                        string directoryName = commandAndParams[2];
                                        fs.MoveToDirectory(directoryName);
                                        Console.WriteLine($"Changed to directory {fs.CurrentDirectory.Name}");


                                    }
                                    break;

                                case "ls":
                                    //processingListOutput = true;

                                    // list current directory contents
                                    Console.WriteLine($"Listing contents for directory {fs.CurrentDirectory.Name}");
                                    var children = fs.CurrentDirectory.GetChildrenNames();
                                    foreach (string childName in children)
                                    {
                                        Console.WriteLine("\t{childName}");
                                    }
                                    break;
                            }

                        }
                        else
                        {
                            // We can build a tree from two types of ls input
                            // <size> <filename>
                            // dir <dirname>
                            if (commandAndParams[0].StartsWith("dir"))
                            {
                                // add a child directory
                                fs.CurrentDirectory.AddChildDir(commandAndParams[1]);
                            }
                            else
                            {
                                // Try to add a file
                                uint size = 0;
                                if (uint.TryParse(commandAndParams[0], out size))
                                {

                                    fs.CurrentDirectory.AddFile(commandAndParams[1], size);
                                }
                            }
                        }
                    }
                }
            }

            fs.MoveToRoot();
            return fs;
        }
    }

    internal class FileSystemNode
    {
        private FileSystemNode(string name, uint size, FileSystemNode parent)
        {
            // Files have to have a parent, and it must be a directory
            if ((parent != null) && (parent.IsDirectory))
            {
                Parent = parent;
            }
            else
            {
                throw new ArgumentException("Invalid parent for file constructor");
            }
            Name = name;
            Size = size;
            IsDirectory = false;
            Children = new List<FileSystemNode>();
        }

        private FileSystemNode(string name = "", FileSystemNode? parent = null)
        {
            if ((parent != null) && (!parent.IsDirectory))
            {
                throw new ArgumentException("Invalid parent for directory constructor");
            }

            Parent = parent;  // can be null for a root dir node
            Name = name;
            Size = 0;       // will be used as file size total
            IsDirectory = true;
            Children = new List<FileSystemNode>();
        }

        public static FileSystemNode CreateRootNode()
        {
            return new FileSystemNode();
        }

        public override string ToString()
        {
            if (this.IsRoot())
            {
                return "/";
            }
            else {
                return $"./{this.Name}";
            }
        }

        public uint Size
        {
            get; private set;
        }

        public string Name
        {
            get; set;
        }

        public FileSystemNode? Parent
        {
            get; private set;
        }

        private List<FileSystemNode> Children // IEnumerable
        { get; set; }

        public IEnumerable<string> GetChildrenNames()
        {   
            return Children.Select(s => s.Name);
        }

        public bool ChildDirectoryExists(string name)
        {
            var childDirectory = Children.Where(s => (s.Name == name));
            if (childDirectory.Any() && childDirectory.First().IsDirectory)
            {
                return true;
            }

            return false;
        }

        public FileSystemNode? GetChild(string Name)
        {
            if (!this.IsDirectory)
            {
                // Files don't have children.
                return null;
            }

            var results = Children.Where(s => s.Name == Name);

            if (results.Any())
            {
                return results.First();
            }
            else
            {
                return null;
            }
        }

        public bool IsRoot()
        {
            return IsDirectory && Parent == null;
        }

        public bool IsDirectory
        {
            get; private set;
        }

        public void AddFile(string name, uint size)
        {
            if (this.IsDirectory)
            {

                FileSystemNode childFile = new FileSystemNode(name, size, this);
                Children.Add(childFile);
                this.Size += size;  // directory size is just direct file sizes

            }
        }

        public void AddChildDir(string name)
        {
            if (this.IsDirectory)
            {
                FileSystemNode childDir = new FileSystemNode(name, this);
                Children.Add(childDir);
            }

        }
    }
}