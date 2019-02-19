using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Dictionary
{
    class IncidenceMatrix
    {
        public string FilePath { get; set; }
        private Tokenizer _parser;
        List<string> tokens = new List<string>();
        int _allDocs = 0;
        private List<int> _incidenceMatrix = new List<int>(); // list: (word -> bitmap)

        public IncidenceMatrix(string filePath)
        {
            FilePath = filePath;
            _parser = new Tokenizer();
        }

        public int DoucumentNameToCode(string fileName)
        {
            return (int)Math.Pow(2, Int32.Parse(fileName));
        }

        private int PowerOf2(int num)
        {
            int i = 0;
            for (;  num != 0; i++)
            {
                num /= 2;
                
            }
            return i -1;
        }
        public List<int> DoucumentsCodesToNames(int mask)
        {
            List<int> result = new List<int>();
            for (int i = 0; mask !=0; i++)
            {
                if (mask %2 == 1)
                {
                    result.Add((int)Math.Pow(2, i)); 
                }
                mask /= 2;
            }
            return result.Select(ix => PowerOf2(ix)).ToList();
        }

        public int MaskOfDoucumentsContainingWord(string word)
        {
            int indexOf = tokens.IndexOf(word);
            if (indexOf == -1)
            {
                return 0;
            }
            else
            {
                return _incidenceMatrix[indexOf];
            }
        }

        public void EnreachIncidenceMatrix(string fileName, string fileExt)
        {
            foreach (string word in _parser.Parse(FilePath, fileName, fileExt))
            {
                int indexOf = tokens.IndexOf(word);
                int doucumentWhitchContainWord = DoucumentNameToCode(fileName);
                _allDocs |= doucumentWhitchContainWord;
                if (indexOf == -1)
                {
                    tokens.Add(word);
                    indexOf = tokens.Count -1;
                    _incidenceMatrix.Add(doucumentWhitchContainWord);
                }
                else
                {
                    _incidenceMatrix[indexOf] = _incidenceMatrix[indexOf] | doucumentWhitchContainWord;
                }
            }
        }

        public int Or(string word1, string word2)
        {
           return MaskOfDoucumentsContainingWord(word1) | MaskOfDoucumentsContainingWord(word2);
        }

        public int And(string word1, string word2)
        {
            return MaskOfDoucumentsContainingWord(word1) & MaskOfDoucumentsContainingWord(word2);
        }

        public int Not(string word1)
        {
            return _allDocs ^ MaskOfDoucumentsContainingWord(word1);
        }

        public List<int> Search(Func<int> func)
        {   
            return DoucumentsCodesToNames(func());
        }

    }
}
