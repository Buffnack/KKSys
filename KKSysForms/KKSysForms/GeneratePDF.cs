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
        private const String TEX_HEADER = "\\documentclass[a4paper,twoside,footinclude=false,numbers=noenddot,landscape, 11pt]{scrartcl}\\usepackage{tabularx}\\usepackage[left = 0.2875cm, right = -0.575cm,top = 0cm, bottom = 0cm]{geometry} \\begin{document}  ";
        private const String TEX_DEF = "\\newcolumntype{C}[1]{>{\\centering\\arraybackslash}p{#1}}";
        private const String TEX_TABULAR_DEF = "\\begin{tabular}{C{6.9cm}| C{6.9cm}| C{6.9cm} | C{6.9cm}}";
        private const String TEX_TABULAR_DEF_NOLINE = "\\begin{tabular}{C{6.9cm} C{6.9cm} C{6.9cm}  C{6.9cm}}";
        private const String TEX_TABULAR_END = "\\end{tabular} \\";
        //Multicol for header and footer in cell?
        private const String TEX_CELL_START = "	\\parbox[c][5.2cm][c]{6.9cm}{\\centerline{";
        private const String TEX_CELL_END = "}}";
        private const String TEX_COL_SWITCH = "&";
        private const String TEX_ROW_SWITCH = "\\\\ ";
        private const String TEX_HLINE = "\\hline";
        private const String TEX_END = "\\end{document}";


        //Create possibility to switch between A8, A7, A6 and A5
        public static void GeneratePDFFile(List<QACard> printable, String outputName)
        {
            NameOfFile = NameOfFile + outputName + ".tex";
            GenerateTexOutput(printable);
            CompileTexToPdf();
            
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
           answerQueue =  (Queue<Datatype>)answerQueue.Reverse();


            String generalOutput = "";
            String frontPage = TEX_HEADER +TEX_DEF+TEX_TABULAR_DEF;
            String backPage = TEX_TABULAR_DEF_NOLINE;
            int cellCount = 0;
            int entryCount = 0;
            String tmp = "";
            String tmpEnd = "";
            
            while (questionQueue.Count != 0)
            {
             
                //Erstellen der Frontpage Zelle
                tmp = TEX_CELL_START + questionQueue.Dequeue().ToTex() + TEX_CELL_END;
                frontPage = frontPage + tmp;

                tmp = TEX_CELL_START + answerQueue.Dequeue().ToTex() + TEX_CELL_END;
                backPage = backPage + tmp;

                //Abhier sind beide Queues lenght-1
                //Erhoehen anzahl der Zellen
                //1 = 1 Eintrag, 2 = 2 Eintrag, 3 = 3 Eintrag, 4 = 4 Eintrag
                cellCount++;
                entryCount++;
                //Abfrage ob schon die letzte Spalte erreicht ist
                if (entryCount == 4)
                {
                    frontPage = frontPage + TEX_ROW_SWITCH;
                    backPage = backPage + TEX_ROW_SWITCH;
                    if (cellCount != 16)
                    {
                        frontPage = frontPage + TEX_HLINE;
                        backPage = backPage + TEX_HLINE;
                    }
                    entryCount = 0;
                }
                //Sonst wird ein Spaltenwechsel ausgeloest
                else
                {   frontPage = frontPage + TEX_COL_SWITCH;
                    backPage = backPage + TEX_COL_SWITCH;
                    tmpEnd = TEX_COL_SWITCH;
                }


                if (cellCount == 16)
                {
                    generalOutput = generalOutput + frontPage + TEX_TABULAR_END + "\\\\ "+ backPage + TEX_TABULAR_END;
                    frontPage = TEX_TABULAR_DEF;
                    backPage = TEX_TABULAR_DEF_NOLINE;
                    cellCount = 0;
                    entryCount = 0;
                   
                }
               
            }


            File.WriteAllText(NameOfFile,generalOutput +" "+TEX_END);
           
            

        }

        private static void CompileTexToPdf()
        {

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            startInfo.FileName = "pdflatex.exe";
            startInfo.Arguments = String.Concat("--interaction=nonstopmode --synctex=0 ", NameOfFile);
            // startInfo.WorkingDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ; 
            startInfo.CreateNoWindow = false;
            process.StartInfo = startInfo;
            process.Start();
            
            

            

        }

        
        

        


    }
}
