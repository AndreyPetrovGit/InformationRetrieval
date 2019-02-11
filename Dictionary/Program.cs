using System;

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
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine("Start " +i);
               // ix.EnreachIndex(i.ToString(), "txt");
                //im.EnreachIncidenceMatrix(i.ToString(), "txt");
                //ix2.EnreachIndex(i.ToString(), "txt");
                pii.EnreachIndex(i.ToString(), "txt");
                Console.WriteLine("End " + i);
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





            Console.ReadLine();

        }
    }
}
