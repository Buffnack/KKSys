using System;
using System.Windows.Forms;



namespace KKSysForms
{
    public partial class MainFrameHandle : Form
    {
        static int cards = 0;
     
        public MainFrameHandle()
        {
            // InitializeComponent();
            this.Controls.Add(new CardCreatorPanel());
            Controller.initSystem();

          
           
            
        }

        
    }


}
