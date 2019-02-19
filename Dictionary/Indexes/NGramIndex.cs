using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary
{
    //4
    class NGramIndex
    {
        Dictionary<string/*n-gram*/, HashSet<int/*docId*/>> _nGram = new Dictionary<string, HashSet<int>>();
        private HashSet<int> files = new HashSet<int>();//processed files
        public string FilePath { get; set; }
        private Tokenizer _parser;

        public NGramIndex(string filePath)
        {
            FilePath = filePath;
            _parser = new Tokenizer();
        }

        public HashSet<string> TokenTo2GramList(string token)
        {
            throw new NotImplementedException();
        }

        public void EnreachIndex(string fileName, string fileExt)
        {
            files.Add(Int32.Parse(fileName));
            foreach (string token in _parser.Parse(FilePath, fileName, fileExt))
            {
                var list2Gram = TokenTo2GramList(token);
                foreach (var item2Gram in list2Gram)
                {
                    HashSet<int> list = null;
                    if (_nGram.ContainsKey(item2Gram))
                    {
                        list = _nGram[item2Gram];
                        list.Add(Int32.Parse(fileName));
                        _nGram[item2Gram] = list;
                    }
                    else
                    {

                        list = new HashSet<int>();
                        list.Add(Int32.Parse(fileName));
                        _nGram.Add(item2Gram, list);
                    }
                }
            }
        }

        public List<int> WildcardSearch(List<string> query)
        {
            List<int> res = new List<int>();

            return res;
        }
    }
}
