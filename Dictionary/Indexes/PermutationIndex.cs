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

        //лексикон перестановок
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
                foreach (var perm in LexicalPermutations(word))
                {
                    if (_trie.Prefix(perm) == null)
                    {
                        _trie.Insert(perm);
                    }
                }
            }
        }

        private string Shift()
        {
            throw new NotImplementedException();
        }

        public List<int> WildcardSearch(List<string> query)
        {
            query[query.Count - 1] += "$";



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
