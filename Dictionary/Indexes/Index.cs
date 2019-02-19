using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dictionary
{
    class Index
    {
        public string FilePath { get; set; }
        private Dictionary<string/*termName*/, HashSet<int/*docId*/>> _index;
        private HashSet<int> files = new HashSet<int>();
        private Tokenizer _parser;

        public Index(string filePath)
        {
            FilePath = filePath;
            _index = new Dictionary<string, HashSet<int>>();
            _parser = new Tokenizer();
        }

        
        public void EnreachIndex(string fileName, string fileExt)
        {
            files.Add(Int32.Parse(fileName));
            foreach (string word in _parser.Parse(FilePath,  fileName,  fileExt))
            {
                HashSet<int> list = null;
                if (_index.ContainsKey(word))
                {
                    list = _index[word];
                    list.Add(Int32.Parse(fileName));
                    _index[word] = list;
                }
                else
                {

                    list = new HashSet<int>();
                    list.Add(Int32.Parse(fileName));
                    _index.Add(word, list);
                }
               
            }
        }

        public void ExportIndexToFs()
        {
            StreamWriter sw = new StreamWriter($@"{FilePath}\index.txt");
            foreach (var item in _index)
            {
                sw.Write(item.Key);
                foreach (var index  in item.Value)
                {
                    sw.Write($" {index}");
                }
                sw.WriteLine();
            }
            
        }

        
        public void ShowSize()
        {
            Console.WriteLine("Unique words count: " +_index.Count);
            Console.WriteLine("Size of raw data 2.5MB");
            Console.WriteLine("Size of index 588KB");
            Console.WriteLine();
        }

        public List<int> Or(string word1, string word2)
        {
            if (!_index.ContainsKey(word1) && !_index.ContainsKey(word2))
            {
                return new List<int>();
            } else
            if (!_index.ContainsKey(word1) && _index.ContainsKey(word2))
            {
                return _index[word2].ToList();
            }
            else
            if (_index.ContainsKey(word1) && !_index.ContainsKey(word2))
            {
                return _index[word1].ToList();
            }
            else
            {
                return _index[word1].Union(_index[word2]).ToList();
            }
        }

        public List<int> And(string word1, string word2)
        {

            if (!_index.ContainsKey(word1) || !_index.ContainsKey(word2))
            {
                return new List<int>();
            }
            else
            {
                return _index[word1].Intersect(_index[word2]).ToList();
            }
        }

        public List<int> Not(string word1)
        {
            if (!_index.ContainsKey(word1))
                return files.ToList();
            return files.Except(_index[word1]).ToList();
        }

        public List<int> Search(string word)
        {
            if (_index.ContainsKey(word))
            {
                return _index[word].ToList();
            }
            else
            {
                return new List<int>();
            }
        }

        public List<int> Search(Func<List<int>> func)
        {
            return func();
        }
    }
}
