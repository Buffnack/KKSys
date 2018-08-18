using System;

using System.Windows.Forms;

namespace KKSysForms
{
    static class Program
    {
        
        static Controller executeManager;
        
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Die Form muss den Controller kennen, der Controller kennt das KKSys
            //Controller laeuft in einem Thread, da wir sonst mit den Systemnachrichten 
            //kollidieren - oder geht das auch so? Mal sehen
            //TODO: Was ist das mit dem STATThread
            Form1 tmp = new Form1();
            
            
            KKSys run = new KKSys(tmp);
            
            Application.Run(tmp);
            


            //Friedrich darf die Gui machen uns intressierts ab hier
            //Einstiegspunkt der Programmlogic

        }
    }
}
