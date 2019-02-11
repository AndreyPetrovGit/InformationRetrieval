using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace Dictionary
{
    class Tokenizer
    {
        public IEnumerable<string> ParsePdf(string fileFullPath)
        {
            PdfReader reader = new PdfReader(fileFullPath);
            string text = string.Empty;
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                text += PdfTextExtractor.GetTextFromPage(reader, page);
            }
            reader.Close();
            var lineWords = text.Split(' ');
            foreach (var word in lineWords)
            {
                yield return word;
            }
        }

        public IEnumerable<string> ParseTxt(string fileFullPath)
        {
            using (var mappedFile1 = MemoryMappedFile.CreateFromFile(fileFullPath))
            {
                using (Stream mmStream = mappedFile1.CreateViewStream())
                {
                    using (StreamReader sr = new StreamReader(mmStream, ASCIIEncoding.ASCII))
                    {
                        while (!sr.EndOfStream)
                        {
                            var line = sr.ReadLine();
                            var lineWords = line.Split();
                            foreach (var token in lineWords)
                            {
                                yield return token;
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<string> Parse(string filePath, string fileName, string fileExt)
        {
            string fullPath = $"{filePath}\\{fileName}.{fileExt}";
            IEnumerable<string> vs = null;
            switch (fileExt)
            {
                case "txt": vs = ParseTxt(fullPath); break;
                case "pdf": vs = ParsePdf(fullPath); break;
                default:
                    throw new NotImplementedException("Unknown file ext");
            }

            foreach (var item in vs)
            {
                yield return item;
            }
        }

    }
}
