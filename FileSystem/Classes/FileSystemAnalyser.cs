using FileSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FileSystem.Classes
{
    public static class FileSystemAnalyser
    {
        public const int MbSize = 1024 * 1024;

        public static FileSystemViewModel GetData(string path) {
            FileSystemViewModel result = new FileSystemViewModel();
            DirectoryInfo directory = new DirectoryInfo(path);
            List<FilesStatisticCalculator> statisticsGroups= new List<FilesStatisticCalculator>();

            if (!directory.Exists) {
                return null;
            }

            result.CurrentPath = path;
            try {
                result.Directories = directory.GetDirectories().Select(v => v.Name).ToList();
            } catch (UnauthorizedAccessException exception) {
                return result;
            }

            result.Files = directory.GetFiles().Select(v => v.Name).ToList();

            var statisticsGroup = new FilesStatisticCalculator() {
                Name = "<= 10mb",
                BelongingCheck = (long  v) => { return v <= 10 * MbSize; }
            };
            statisticsGroups.Add(statisticsGroup);

            statisticsGroup = new FilesStatisticCalculator() {
                Name = "> 10mb AND <= 50mb",
                BelongingCheck = ( long  v ) => { return v > 10 * MbSize && v <= 50 * MbSize; }
            };
            statisticsGroups.Add(statisticsGroup);

            statisticsGroup = new FilesStatisticCalculator() {
                Name = ">= 100mb",
                BelongingCheck = ( long  v ) => { return v >= 100 * MbSize; }
            };
            statisticsGroups.Add(statisticsGroup);



            



            Count(directory, statisticsGroups);
            
            result.Statistics = statisticsGroups.Select(v => new FilesStatisticGroup() { Name = v.Name, Count = v.Count} ).ToList();
            
            return result;
        }

        private static void Count(DirectoryInfo directory, List<FilesStatisticCalculator> statisticsGroups) {
            try { 
                foreach(var file in directory.GetFiles()) {
                    foreach(var statisticsGroup in statisticsGroups) {
                        statisticsGroup.Counter(file.Length);
                    }
                }
            } catch (UnauthorizedAccessException exception) {
                return;
            } 

            var subdirectories = directory.GetDirectories();
            foreach(var subdirectory in directory.GetDirectories()) {
                    Count(subdirectory, statisticsGroups); 
            }
            return;
        }
    }
}