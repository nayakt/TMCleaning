using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TMCleaningSln
{
    class Program
    {
        static void Main(string[] args)
        {
            //Analysis of Spanish by Tapas
            string inputFile = @"D:\MCSE\Thesis\NLP4TM2016\SharedTask\TestData\test-English-Spanish-NoCategory.txt";
            string outputFile = @"D:\MCSE\Thesis\NLP4TM2016\SharedTask\TestData\test-English-Spanish-Category.txt";
            TMSentenceClassifier classifier = new TMSentenceClassifier();
            StreamReader sr = new StreamReader(inputFile);
            string line = sr.ReadLine();
            bool res = true;
            string srcSentence, targetSentence, classLabel;
            StreamWriter sw = new StreamWriter(outputFile);
            while (line != null)
            {
                line = line.Trim();
                res = true;
                string[] sen = line.Split('\t');
                srcSentence = sen[0]; 
                targetSentence = sen[1]; 
                string[] sourceWords = srcSentence.Split(' ');
                string[] targetWords = targetSentence.Split(' ');

                res &= classifier.basis_length(sourceWords, targetWords); // on length
                res &= classifier.basis_letter(sourceWords, targetWords); //case of 1st letter
                res &= classifier.basis_capital(sourceWords, targetWords); //all capital
                res &= classifier.basis_number(sourceWords, targetWords);  // all number

                res &= classifier.basis_max_word(sourceWords, targetWords); //max length word
                res &= classifier.basis_eod_attached(sourceWords, targetWords); //eod
                res &= classifier.basis_hyphen_as_sep(sourceWords, targetWords); // -
                res &= classifier.basis_comma_as_sep(sourceWords, targetWords);  //,
                res &= classifier.basis_singlequote_as_sep(sourceWords, targetWords); // '
                res &= classifier.basis_percent_as_sep(sourceWords, targetWords);  // %
                res &= classifier.basis_colon_as_sep(sourceWords, targetWords);   // :
                res &= classifier.basis_semicolon_as_sep(sourceWords, targetWords);  // ;
                res &= classifier.basis_dash_as_first_char(sourceWords, targetWords); //-

                if (res)
                    classLabel = "1";
                else
                    classLabel = "3";

                sw.WriteLine(srcSentence + "\t" + targetSentence + "\t" + classLabel);
                line = sr.ReadLine();
            } 
            sw.Close();
        }
    }
}
