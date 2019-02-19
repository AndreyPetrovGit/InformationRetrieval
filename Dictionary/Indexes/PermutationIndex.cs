using System;
using System.Collections.Generic;
using System.Linq;

namespace Dictionary
{
    //4
    //перестановочный индекс (KWIC ?)
    class PermutationIndex
    {
        public string FilePath { get; set; }
        private HashSet<int> files = new HashSet<int>();
        private Trie _trie;
        private Tokenizer _parser;

        public PermutationIndex(string filePath)
        {
            _trie = new Trie();
            FilePath = filePath;
            _parser = new Tokenizer();
        }

        //лексиконом перестановок
        private List<string> LexicalPermutations(string s)
        {
            List<string> permutations = new List<string>();
            s += "$";
            for (int i = 0; i < s.Length -1; i++)
            {
                permutations.Add(s);
                s = leftRotateShift(s, 1);
            }
            return permutations;
        }

        private string leftRotateShift(string key, int shift)
        {
            shift %= key.Length;
            return key.Substring(shift) + key.Substring(0, shift);
        }

        public void EnreachIndex(string fileName, string fileExt)
        {
            files.Add(Int32.Parse(fileName));
            foreach (string word in _parser.Parse(FilePath, fileName, fileExt))
            {
                HashSet<int> list = null;
                if (_trie.Prefix(word) != null)
                {
                    list = _trie.Prefix(word).docIds;
                    list.Add(Int32.Parse(fileName));
                    //_trie[word] = list; update list
                }
                else
                {

                    list = new HashSet<int>();
                    list.Add(Int32.Parse(fileName));
                    _trie.Insert(word); // +list
                }

            }
        }

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

            return pretendents.SelectMany(p => _index.Search(p)).Distinct().ToList();
        }
    }
}
