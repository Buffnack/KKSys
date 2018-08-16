using System;

using System.Windows.Forms;

namespace KKSysForms
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
           
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 tmp = new Form1();
            
            KKSys run = new KKSys(tmp);
            Application.Run(tmp);


            //Friedrich darf die Gui machen uns intressierts ab hier
            //Einstiegspunkt der Programmlogic

        }
    }
}
