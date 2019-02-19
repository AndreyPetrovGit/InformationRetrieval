using System;
using System.Collections.Generic;

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
            List<int> res = new List<int>();

            return res;
        }
    }
}
