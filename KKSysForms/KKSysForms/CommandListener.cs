using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KKSysForms_DataTypes;

namespace KKSysForms
{
    class CommandListener
    {
        private TextBox line;
        private RichTextBox output;
        private ListBox  data;
        private List<Object> list;

        public CommandListener(TextBox line, RichTextBox output, ListBox data)
        {
            this.line = line;
            this.output = output;
            this.data = data;
            list = new List<object>();
        }
        
        //Hier die Reaktion der Commandline machen
        public String reactOnCommand(String command)
        {
            switch (command)
            {
                case "Create Datatype":

                    UpdateList(new KKSysForms_DataTypes.Text("Hello"));
                    return "Create a Text";
                default:
                    return "";
                    
            }

        }

        public String getSelectedData( int i)
        {
            if (i > list.Count)
            {
                return "Failure";
            }
            return "";
        }

        public void UpdateList(Object dataObj)
        {
            if (dataObj is KKSysForms_DataTypes.DataType)
            {
                if (dataObj is Text)
                {
                    data.Items.Add((Text)dataObj);
                    list.Add((Text)dataObj);

                }

            }
            
        }
    }
}
