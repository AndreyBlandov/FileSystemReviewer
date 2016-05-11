using FileSystem.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static FileSystem.Classes.FileSystemAnalyser;

namespace FileSystem.Models
{
    public class FileSystemViewModel
    {
        public string CurrentPath { get; set;}
        public List<string> Directories { get; set;}
        public List<string> Files { get; set;}
        public List<FilesStatisticGroup> Statistics { get; set;}
    }
}