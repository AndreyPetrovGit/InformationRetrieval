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

            NGramIndex ng = new NGramIndex(filePath, ix);
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
            //pi.WildcardSearch(new List<string> { "colo", "*", "r" }).ForEach(Console.WriteLine);
            //pi.WildcardSearch(new List<string> { "*", "ur" }).ForEach(Console.WriteLine);
            //pi.WildcardSearch(new List<string> { "colo", "*" }).ForEach(Console.WriteLine);


            Console.ReadLine();

            //The EF Core tools version '2.1.4-rtm-31024' is older than that of the runtime '3.0.0-preview.19074.3'.Update the tools for the latest features and bug fixes.
            //Unable to identify the primary key for table 'public.movies_actors'.
            //Unable to generate entity type for table 'public.movies_actors'.
            //Unable to identify the primary key for table 'public.genres'.
            //Unable to generate entity type for table 'public.genres'.
            //Could not find type mapping for column 'public.movies.genre' with data type 'cube'.Skipping column.
            //Unable to scaffold the index 'movies_genres_cube'.The following columns could not be scaffolded: genre.

//            The EF Core tools version '2.1.4-rtm-31024' is older than that of the runtime '3.0.0-preview.19074.3'.Update the tools for the latest features and bug fixes.
//Could not find type mapping for column 'public.movies.genre' with data type 'cube'.Skipping column.
//Unable to scaffold the index 'movies_genres_cube'.The following columns could not be scaffolded: genre.

        }
    }
}
