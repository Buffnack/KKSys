using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using KKSysForms_CardModel;
using KKSysForms_DataTypes;
using System.Reflection;

namespace KKSysForms_PDFCreate
{
    static class GeneratePDF
    {
        

        public static StreamWriter sw;

        public static String NameOfFile = "KKSys_";

        //Sollte Nutzung der Klasse, Page definition beinhalten
        private const String TEX_HEADER = "\\documentclass[a4paper,twoside,%DIV=60,%BCOR=0mm,headlineinclude=false,footinclude=false,numbers=noenddot,%headheight=40pt,landscape, 11pt]{scrartcl}\\usepackage{tabularx}\\usepackage[left = 0.2875cm, right = -0.575cm,top = 0cm, bottom = 0cm]{geometry} \\begin{document}  ";
        private const String TEX_DEF = "\\newcolumntype{C}[1]{>{\\centering\\arraybackslash}p{#1}}";
        private const String TEX_TABULAR_DEF = "\\begin{tabular}{C{6.9cm}| C{6.9cm}| C{6.9cm} | C{6.9cm}}";
        private const String TEX_TABULAR_DEF_NOLINE = "\\begin{tabular}{C{6.9cm} C{6.9cm} C{6.9cm}  C{6.9cm}}";
        private const String TEX_TABULAR_END = "\\end{tabular}";
        //Multicol for header and footer in cell?
        private const String TEX_CELL_START = "	\\parbox[c][5.2cm][c]{6.9cm}{\\centerline{";
        private const String TEX_CELL_END = "}}";
        private const String TEX_COL_SWITCH = "&";
        private const String TEX_ROW_SWITCH = "\\\\";
        private const String TEX_END = "\\end{document}";


        //Create possibility to switch between A8, A7, A6 and A5
        public static void GeneratePDFFile(List<QACard> printable, String outputName)
        {
            NameOfFile = NameOfFile + outputName + ".tex";
            GenerateTexOutput(printable);
        }

        private static void GenerateTexOutput(List<QACard> cards)
        {
            Queue<Datatype> questionQueue = new Queue<Datatype>();
            Queue<Datatype> answerQueue = new Queue<Datatype>();
            foreach (QACard card in cards)
            {
                questionQueue.Enqueue(card.questionContent);
                answerQueue.Enqueue(card.answerContent);
            }
            answerQueue.Reverse();


            String generalOutput = "";
            String frontPage = TEX_HEADER +TEX_DEF+TEX_TABULAR_DEF;
            String backPage = TEX_TABULAR_DEF_NOLINE;
            int cellCount = 0;
            String tmp = "";
            String tmpEnd = "";
            
            while (questionQueue.Count != 0)
            {
                if (cellCount % 4 == 0 && cellCount != 0)
                {
                    tmpEnd = TEX_ROW_SWITCH;
                }
                else
                {
                    tmpEnd = TEX_COL_SWITCH;
                }
                tmp = TEX_CELL_START + questionQueue.Dequeue().ToTex() + TEX_CELL_END + tmpEnd;
                frontPage = frontPage + tmp;

                tmp = TEX_CELL_START + answerQueue.Dequeue().ToTex() + TEX_CELL_END + tmpEnd;
                backPage = backPage + tmp;

                cellCount++;
                if (cellCount == 16)
                {
                    generalOutput = generalOutput + frontPage + TEX_TABULAR_END + backPage + TEX_TABULAR_END;
                    frontPage = TEX_TABULAR_DEF;
                    backPage = TEX_TABULAR_DEF_NOLINE;
                    cellCount = 0;
                   
                }
               
            }

            if (sw == null)
            {
                sw = new StreamWriter(new FileStream(NameOfFile, FileMode.OpenOrCreate));
            }
            else
            {
                sw.Close();
                sw.Dispose();
                sw = new StreamWriter(new FileStream(NameOfFile, FileMode.OpenOrCreate));
            }
            sw.Write(generalOutput +TEX_END);
        }

        private static void CompileTexToPdf()
        {

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C pdflatex -interaction=nonstopmode " + NameOfFile;
            startInfo.WorkingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ; 
            process.StartInfo = startInfo;
            process.Start();

            if (process.ExitCode == 0)
            {
                process.Dispose();
            }
            process.Dispose();

        }

        
        

        


    }
}
