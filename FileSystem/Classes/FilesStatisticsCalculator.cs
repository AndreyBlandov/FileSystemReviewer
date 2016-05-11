using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileSystem.Classes
{

    public class FilesStatisticCalculator : FilesStatisticGroup {
        public Func<long , bool> BelongingCheck { get; set;}

        public void Counter(long fileSize) {
            if(BelongingCheck(fileSize)) Count++ ;
        }
    }
}