using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dictionary
{
    //4
    class NGramIndex
    {
        Dictionary<string/*n-gram*/, HashSet<string/*token*/>> _nGram = new Dictionary<string, HashSet<string>>();
        private HashSet<int> files = new HashSet<int>();//processed files
        public string FilePath { get; set; }
        private Tokenizer _parser;
        private Index _index;

        public NGramIndex(string filePath, Index index)
        {
            FilePath = filePath;
            _index = index;
            _parser = new Tokenizer();
        }

        //что если размер слова меньше k?
        public HashSet<string> TokenTo2GramList(string token)
        {
            HashSet<string> grams = new HashSet<string>();
            token = $"${token}$";
            int k = 2;
            
            for (int i = 0; i < token.Length - k; i++)
            {
                string nextGram = token.Substring(i, k);
                grams.Add(nextGram);
            }

            return grams;
        }

        public void EnreachIndex(string fileName, string fileExt)
        {
            files.Add(Int32.Parse(fileName));
            foreach (string token in _parser.Parse(FilePath, fileName, fileExt))
            {
                var list2Gram = TokenTo2GramList(token);
                foreach (var item2Gram in list2Gram)
                {
                    
                    if (_nGram.ContainsKey(item2Gram))
                    {
                        _nGram[item2Gram].Add(token);
                    }
                    else
                    {
                        HashSet<string> list = new HashSet<string>();
                        list.Add(token);
                        _nGram.Add(item2Gram, list);
                    }
                }
            }
        }

        // *f*f*f*f*? **?
        public List<int> WildcardSearch(List<string> query)
        {
            for (int i = 0; i < query.Count; i++)
            {
                if (query[i] != "*")
                {
                    query[i] = "$" + query[i];
                    break;
                } 
            }
            for (int j = query.Count - 1; j >= 0; j--)
            {
                if (query[j] != "*")
                {
                    query[j] += "$";
                    break;
                }
            }

            HashSet<string> grams = new HashSet<string>();

            foreach (var part in query)
            {
                if (part != "*")
                {
                    foreach (var gram in TokenTo2GramList(part))
                    {
                        grams.Add(gram);
                    }
                    
                }
            }

            List<string> pretendents = new List<string>();

            foreach (var gram in grams)
            {
                if (pretendents.Count == 0)
                {
                    pretendents = _nGram[gram].ToList();
                }
                else
                {
                    pretendents = pretendents.Intersect(_nGram[gram].ToList()).ToList();
                }
                
            }

            return pretendents.SelectMany(p => _index.Search(p)).Distinct().ToList(); //ToDo: постфільтрація
        }
    }
}
