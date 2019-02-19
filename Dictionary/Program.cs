using System;
using System.Collections.Generic;

namespace Dictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\TheAndrey\Source\Repos\Dictionary\Dictionary\files";
            //Source of files http://textfiles.com/etext/AUTHORS/SHAKESPEARE/
            Index ix = new Index(filePath);
            IncidenceMatrix im = new IncidenceMatrix(filePath);
            InvertedIndex2Word ix2 = new InvertedIndex2Word(filePath);
            PositionInvertedIndex pii = new PositionInvertedIndex(filePath);

            NGramIndex ng = new NGramIndex(filePath);
            PermutationIndex pi = new PermutationIndex(filePath);


            var fileRepositry = new FileRepository();
            foreach (int fileId in fileRepositry.GetFiles())
            {
                Console.WriteLine("Start " + fileId);
               // ix.EnreachIndex(i.ToString(), "txt");
                //im.EnreachIncidenceMatrix(i.ToString(), "txt");
                //ix2.EnreachIndex(i.ToString(), "txt");
                ng.EnreachIndex(fileId.ToString(), "txt");
                pi.EnreachIndex(fileId.ToString(), "txt");
                Console.WriteLine("End " + fileId);
            }


            //ix.ExportIndexToFs();
            //ix2.ExportIndexToFs();
            pii.ExportIndexToFs();

            //5. Оцінити розмір колекції, загальну кількість слів в колекції та розмір словника.
            //ix.ShowSize();

            ////-- incidence matrix search
            //Console.WriteLine("im");
            //im.Search(() => im.And("DRAMATIS", "Caesar.")).ForEach(Console.WriteLine);
            //Console.WriteLine("ix");
            //ix.Search(() => ix.And("DRAMATIS", "Caesar.")).ForEach(Console.WriteLine);

            //Console.WriteLine("im");
            //im.Search(() => im.Not( "Caesar.")).ForEach(Console.WriteLine);
            //Console.WriteLine("ix");
            //ix.Search(() => ix.Not("Caesar.")).ForEach(Console.WriteLine);

            //Console.WriteLine("ix2");
            //ix2.Search(() => ix2.And("OCTAVIUS CAESAR", "MARK ANTONY")).ForEach(Console.WriteLine);

            Console.WriteLine("pii");


            Console.WriteLine("NG");
            ng.WildcardSearch(new List<string> { "colo", "*", "r" }).ForEach(Console.WriteLine);
            ng.WildcardSearch(new List<string> { "*", "ur" }).ForEach(Console.WriteLine);
            ng.WildcardSearch(new List<string> { "colo", "*"}).ForEach(Console.WriteLine);


            Console.WriteLine("pi");
            pi.WildcardSearch(new List<string> { "colo", "*", "r" }).ForEach(Console.WriteLine);
            pi.WildcardSearch(new List<string> { "*", "ur" }).ForEach(Console.WriteLine);
            pi.WildcardSearch(new List<string> { "colo", "*" }).ForEach(Console.WriteLine);


            Console.ReadLine();

        }
    }
}
