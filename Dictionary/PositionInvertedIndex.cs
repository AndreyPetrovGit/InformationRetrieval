using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dictionary
{
    class PositionInvertedIndex
    {
        public string FilePath { get; set; }
        private Dictionary<string/*лексема*/, Dictionary<int/*id doc*/, List<int/*position in doc*/>>> _index;
        private HashSet<int> files = new HashSet<int>();
        private Tokenizer _parser;

        public PositionInvertedIndex(string filePath)
        {
            FilePath = filePath;
            _index = new Dictionary<string, Dictionary<int, List<int>>>();
            _parser = new Tokenizer();
        }

        public void EnreachIndex(string fileName, string fileExt)
        {
            files.Add(Int32.Parse(fileName));
            Int32 tokenPos = 0;
            foreach (string word in _parser.Parse(FilePath, fileName, fileExt))
            {
                Dictionary<int, List<int>> docIdDic = null;
                if (_index.ContainsKey(word))
                {
                    docIdDic = _index[word];
                    if (docIdDic.ContainsKey(Int32.Parse(fileName)))
                    {
                        var positions = docIdDic[Int32.Parse(fileName)];
                        positions.Add(tokenPos);
                        docIdDic[Int32.Parse(fileName)] = positions;// need?
                        
                    }
                    else
                    {
                        var positions = new List<int>();
                        positions.Add(tokenPos);
                        docIdDic.Add(Int32.Parse(fileName), positions);
                    }
                    _index[word] = docIdDic;// need?
                }
                else
                {

                    docIdDic = new Dictionary<int, List<int>>();
                    var positions = new List<int>();
                    positions.Add(tokenPos);
                    docIdDic.Add(Int32.Parse(fileName), positions);
                    _index.Add(word, docIdDic);
                }
                ++tokenPos;
            }
        }

        public void ExportIndexToFs()
        {
            StreamWriter sw = new StreamWriter($@"{FilePath}\PositionInvertedIndex.txt");
            foreach (var item in _index)
            {
                sw.Write($"Word:<{item.Key}>");
                
                foreach (var index in item.Value)
                {
                    sw.WriteLine();
                    sw.Write($"<{index.Key}:");
                    foreach (var pos in index.Value)
                    {
                        sw.Write($"{pos};");
                    }
                    sw.Write(">");
                }
                sw.WriteLine();
                sw.WriteLine();
                sw.WriteLine();
            }

        }


        public void ShowSize()
        {
            Console.WriteLine("Unique words count: " + _index.Count);
            Console.WriteLine("Size of raw data 2.5MB");
            //Console.WriteLine("Size of index 588KB");
            Console.WriteLine();
        }

        //public List<int> Or(string word1, string word2)
        //{
        //    if (!_index.ContainsKey(word1) && !_index.ContainsKey(word2))
        //    {
        //        return new List<int>();
        //    }
        //    else
        //    if (!_index.ContainsKey(word1) && _index.ContainsKey(word2))
        //    {
        //        return _index[word2].ToList();
        //    }
        //    else
        //    if (_index.ContainsKey(word1) && !_index.ContainsKey(word2))
        //    {
        //        return _index[word1].ToList();
        //    }
        //    else
        //    {
        //        return _index[word1].Union(_index[word2]).ToList();
        //    }
        //}

        //public List<int> And(string word1, string word2)
        //{

        //    if (!_index.ContainsKey(word1) || !_index.ContainsKey(word2))
        //    {
        //        return new List<int>();
        //    }
        //    else
        //    {
        //        return _index[word1].Intersect(_index[word2]).ToList();
        //    }
        //}

        //public List<int> Not(string word1)
        //{
        //    if (!_index.ContainsKey(word1))
        //        return files.ToList();
        //    return files.Except(_index[word1]).ToList();
        //}

        //public List<int> Search(Func<List<int>> func)
        //{
        //    return func();
        //}
    }
}
