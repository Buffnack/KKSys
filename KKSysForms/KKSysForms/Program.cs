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
           // KKSysDatabase.DatabaseConnector test = new KKSysDatabase.DatabaseConnector();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

           
            //Friedrich darf die Gui machen uns intressierts ab hier
            //Einstiegspunkt der Programmlogic
            
        }
    }
}
