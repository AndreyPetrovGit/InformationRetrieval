using System;
using System.Collections.Generic;

namespace Dictionary
{
    //4
    public class SuffixTree: Trie
    {
        public override void Insert(string s)
        {
            s = Reverse(s);
            base.Insert(s);
        }
        public override void Delete(string s)
        {
            s = Reverse(s);
            base.Delete(s);
        }
         public override List<string> WordsFrom(string s)
        {
            s = Reverse(s);
            
            return base.WordsFrom(s);
        }

        string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
