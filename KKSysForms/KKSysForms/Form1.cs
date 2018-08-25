using System;
using System.Windows.Forms;



namespace KKSysForms
{
    public partial class Form1 : Form
    {

     
        public Form1()
        {
            InitializeComponent();
            Controller.initSystem();

            foreach (KKSysForms_Event.EventLabel el in Controller.system.loadedLabel)
            {
                System.Collections.Generic.List<KKSysForms_CardModel.Theme> them = el.getThemeList();
                foreach (KKSysForms_CardModel.Theme th in them)
                {
                    this.ThemeBoxCreatorCB.Items.Add(th.ThemeName);
                }
                this.EvBoxCreatorCb.Items.Add(el.Name);
            }
           
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Controller.system.Stored)
            {
                Controller.system.Stored = true;
            }   
        }

        private void AddCardBut_Click(object sender, EventArgs e)
        {

        }

        private void AddEventBut_Click(object sender, EventArgs e)
        {

        }

        private void showAll_EventBut_Click(object sender, EventArgs e)
        {

        }

        private void AddEventLabelBut_Click(object sender, EventArgs e)
        {

        }

        private void eventLabelCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ShowAllCardsBut_Click(object sender, EventArgs e)
        {

        }

        private void AddThemeBut_Click(object sender, EventArgs e)
        {

        }

        private void ThemeCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EventListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CardListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RemEventBut_Click(object sender, EventArgs e)
        {

        }

        private void EditEventBut_Click(object sender, EventArgs e)
        {

        }

        private void RemCardBut_Click(object sender, EventArgs e)
        {

        }

        private void EditCardBut_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            Controller.system.CreatePDF();
        }

        private void AddCardCreatorBt_Click(object sender, EventArgs e)
        {
            if (this.QContentTB.Text == "" || this.QHeadTB.Text == "" || this.AHeadTB.Text == "" || this.AContentTB.Text == "")
            {
                return;
            }

            

            Controller.system.CreateCards(this.ThemeBoxCreatorCB.Text, this.QHeadTB.Text
                , this.AHeadTB.Text, this.QContentTB.Text, this.AContentTB.Text);
        }

        private void ELAndTheCreatorBt_Click(object sender, EventArgs e)
        {
            if (this.EventLabelCreatorTB.Text == "" && this.ThemeCreatorTB.Text == "")
            {
                return;
            }
            else
            {
                if (this.EventLabelCreatorTB.Text != "")
                {
                    Controller.system.CreateEventLabel(this.EventLabelCreatorTB.Text);
                }
                if (this.ThemeBoxCreatorCB.Text != "")
                {
                    Controller.system.CreateTheme(this.ThemeCreatorTB.Text);
                }

                this.EventLabelCreatorTB.Text = "";
                this.ThemeCreatorTB.Text = "";
            }
        }

        private void EvBoxCreatorCb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Controller.system.SetCurrentEventLabelTarget(this.EvBoxCreatorCb.Text);
        }
    }


}
