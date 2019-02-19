using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Dictionary
{
    //4 PrefixTree
    public class Trie
    {
        private readonly Node _root;

        public Trie()
        {
            _root = new Node('^', 0, null);
        }

        public Node Prefix(string s)
        {
            var currentNode = _root;
            var result = currentNode;

            foreach (var c in s)
            {
                currentNode = currentNode.FindChildNode(c);
                if (currentNode == null)
                    break;
                result = currentNode;
            }

            return result;
        }

        virtual public List<string> WordsFrom(string s)
        {
            Node fromNode = Prefix(s);
            List<string> words = new List<string>();
            WordsFrom(fromNode, s, ref words);

            return words;
        }

        public void WordsFrom(Node node, string prefix, ref List<string> words)
        {
            if (!node.IsLeaf())
            {
                foreach (var chNode in node.Children)
                {
                    WordsFrom(chNode, prefix + node.ToString(), ref words);
                }

            } else
            {
                words.Add(prefix + node.Value.ToString());
            }  
        }

        public List<int> DocIdsWithPrifix(string s)
        {
            Node fromNode = Prefix(s);
            List<int> ids = new List<int>();
            WordsFrom(fromNode, s, ref ids);
            
            return ids;
        }

        public void WordsFrom(Node node, string prefix, ref List<int> ids)
        {
            if (!node.IsLeaf())
            {
                foreach (var chNode in node.Children)
                {
                    WordsFrom(chNode, prefix + node.ToString(), ref ids);
                }

            }
            else
            {
                ids.AddRange(node.docIds);
                ids.Distinct();
            }
        }


        public bool Search(string s)
        {
            var prefix = Prefix(s);
            return prefix.Depth == s.Length && prefix.FindChildNode('$') != null;
        }

        public void InsertRange(List<string> items)
        {
            for (int i = 0; i < items.Count; i++)
                Insert(items[i]);
        }

        virtual public void Insert(string s)
        {
            var commonPrefix = Prefix(s);
            var current = commonPrefix;

            for (var i = current.Depth; i < s.Length; i++)
            {
                var newNode = new Node(s[i], current.Depth + 1, current);
                current.Children.Add(newNode);
                current = newNode;
            }

            current.Children.Add(new Node('$', current.Depth + 1, current));
        }

        virtual public void Delete(string s)
        {
            if (Search(s))
            {
                var node = Prefix(s).FindChildNode('$');

                while (node.IsLeaf())
                {
                    var parent = node.Parent;
                    parent.DeleteChildNode(node.Value);
                    node = parent;
                }
            }
        }
    }
}
