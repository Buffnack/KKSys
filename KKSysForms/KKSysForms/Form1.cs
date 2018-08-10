using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KKSysForms
{
    public partial class Form1 : Form
    {

        private RichTextBox commandLog;
        private TextBox commandLine;
        private ListBox objects;

        private bool objectView;
        private String log;

        private CommandListener listener; 
        


        public Form1()
        {
            InitializeComponent();

           
            
        }

     
     



        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          

        }

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {
            Console.WriteLine();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }


}
