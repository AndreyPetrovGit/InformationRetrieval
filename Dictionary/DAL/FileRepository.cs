using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary
{
    class FileRepository
    {
        private List<int> files = new List<int>()
        {
            1,2,3,4,5,6,7,8,9,10
        };

        public List<int> GetFiles()
        {
            return files;
        } 
    }
}
