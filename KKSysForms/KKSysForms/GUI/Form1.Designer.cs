using System;
using System.ComponentModel;

namespace KKSysForms
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.eventLabelCB = new System.Windows.Forms.ComboBox();
            this.ThemeCB = new System.Windows.Forms.ComboBox();
            this.EventListBox = new System.Windows.Forms.ListBox();
            this.AddCardBut = new System.Windows.Forms.Button();
            this.RemCardBut = new System.Windows.Forms.Button();
            this.EditCardBut = new System.Windows.Forms.Button();
            this.EditEventBut = new System.Windows.Forms.Button();
            this.RemEventBut = new System.Windows.Forms.Button();
            this.AddEventBut = new System.Windows.Forms.Button();
            this.ShowAllCardsBut = new System.Windows.Forms.Button();
            this.showAll_EventBut = new System.Windows.Forms.Button();
            this.CardListBox = new System.Windows.Forms.ListBox();
            this.AddEventLabelBut = new System.Windows.Forms.Button();
            this.AddThemeBut = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CreatePDFBt = new System.Windows.Forms.Button();
            this.ELAndTheCreatorBt = new System.Windows.Forms.Button();
            this.AddCardCreatorBt = new System.Windows.Forms.Button();
            this.ThemeBoxCreatorCB = new System.Windows.Forms.ComboBox();
            this.EvBoxCreatorCb = new System.Windows.Forms.ComboBox();
            this.ThemeCreatorTB = new System.Windows.Forms.TextBox();
            this.EventLabelCreatorTB = new System.Windows.Forms.TextBox();
            this.InsertLabel = new System.Windows.Forms.Label();
            this.AContentTB = new System.Windows.Forms.TextBox();
            this.QContentTB = new System.Windows.Forms.TextBox();
            this.AHeadTB = new System.Windows.Forms.TextBox();
            this.QHeadTB = new System.Windows.Forms.TextBox();
            this.QContentCreatedList = new System.Windows.Forms.ListBox();
            this.AContentCreatedList = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(305, 148);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(304, 36);
            this.toolStripMenuItem1.Text = "toolStripMenuItem1";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(304, 36);
            this.toolStripMenuItem2.Text = "toolStripMenuItem2";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(304, 36);
            this.toolStripMenuItem3.Text = "toolStripMenuItem3";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(304, 36);
            this.toolStripMenuItem4.Text = "toolStripMenuItem4";
            // 
            // eventLabelCB
            // 
            this.eventLabelCB.FormattingEnabled = true;
            this.eventLabelCB.Location = new System.Drawing.Point(334, 31);
            this.eventLabelCB.Name = "eventLabelCB";
            this.eventLabelCB.Size = new System.Drawing.Size(171, 33);
            this.eventLabelCB.TabIndex = 3;
            this.eventLabelCB.SelectedIndexChanged += new System.EventHandler(this.eventLabelCB_SelectedIndexChanged);
            // 
            // ThemeCB
            // 
            this.ThemeCB.FormattingEnabled = true;
            this.ThemeCB.Location = new System.Drawing.Point(992, 30);
            this.ThemeCB.Name = "ThemeCB";
            this.ThemeCB.Size = new System.Drawing.Size(170, 33);
            this.ThemeCB.TabIndex = 4;
            this.ThemeCB.SelectedIndexChanged += new System.EventHandler(this.ThemeCB_SelectedIndexChanged);
            // 
            // EventListBox
            // 
            this.EventListBox.FormattingEnabled = true;
            this.EventListBox.ItemHeight = 25;
            this.EventListBox.Location = new System.Drawing.Point(34, 97);
            this.EventListBox.Name = "EventListBox";
            this.EventListBox.Size = new System.Drawing.Size(473, 604);
            this.EventListBox.TabIndex = 6;
            this.EventListBox.SelectedIndexChanged += new System.EventHandler(this.EventListBox_SelectedIndexChanged);
            // 
            // AddCardBut
            // 
            this.AddCardBut.Location = new System.Drawing.Point(697, 716);
            this.AddCardBut.Name = "AddCardBut";
            this.AddCardBut.Size = new System.Drawing.Size(131, 51);
            this.AddCardBut.TabIndex = 7;
            this.AddCardBut.Text = "Add Card";
            this.AddCardBut.UseVisualStyleBackColor = true;
            this.AddCardBut.Click += new System.EventHandler(this.AddCardBut_Click);
            // 
            // RemCardBut
            // 
            this.RemCardBut.Location = new System.Drawing.Point(850, 716);
            this.RemCardBut.Name = "RemCardBut";
            this.RemCardBut.Size = new System.Drawing.Size(103, 51);
            this.RemCardBut.TabIndex = 8;
            this.RemCardBut.Text = "Remove Card";
            this.RemCardBut.UseVisualStyleBackColor = true;
            this.RemCardBut.Click += new System.EventHandler(this.RemCardBut_Click);
            // 
            // EditCardBut
            // 
            this.EditCardBut.Location = new System.Drawing.Point(992, 716);
            this.EditCardBut.Name = "EditCardBut";
            this.EditCardBut.Size = new System.Drawing.Size(168, 51);
            this.EditCardBut.TabIndex = 9;
            this.EditCardBut.Text = "Edit Card";
            this.EditCardBut.UseVisualStyleBackColor = true;
            this.EditCardBut.Click += new System.EventHandler(this.EditCardBut_Click);
            // 
            // EditEventBut
            // 
            this.EditEventBut.Location = new System.Drawing.Point(337, 716);
            this.EditEventBut.Name = "EditEventBut";
            this.EditEventBut.Size = new System.Drawing.Size(168, 51);
            this.EditEventBut.TabIndex = 12;
            this.EditEventBut.Text = "Edit Event";
            this.EditEventBut.UseVisualStyleBackColor = true;
            this.EditEventBut.Click += new System.EventHandler(this.EditEventBut_Click);
            // 
            // RemEventBut
            // 
            this.RemEventBut.Location = new System.Drawing.Point(195, 716);
            this.RemEventBut.Name = "RemEventBut";
            this.RemEventBut.Size = new System.Drawing.Size(103, 51);
            this.RemEventBut.TabIndex = 11;
            this.RemEventBut.Text = "Remove Event";
            this.RemEventBut.UseVisualStyleBackColor = true;
            this.RemEventBut.Click += new System.EventHandler(this.RemEventBut_Click);
            // 
            // AddEventBut
            // 
            this.AddEventBut.Location = new System.Drawing.Point(42, 716);
            this.AddEventBut.Name = "AddEventBut";
            this.AddEventBut.Size = new System.Drawing.Size(131, 51);
            this.AddEventBut.TabIndex = 10;
            this.AddEventBut.Text = "Add Event";
            this.AddEventBut.UseVisualStyleBackColor = true;
            this.AddEventBut.Click += new System.EventHandler(this.AddEventBut_Click);
            // 
            // ShowAllCardsBut
            // 
            this.ShowAllCardsBut.Location = new System.Drawing.Point(697, 30);
            this.ShowAllCardsBut.Name = "ShowAllCardsBut";
            this.ShowAllCardsBut.Size = new System.Drawing.Size(146, 33);
            this.ShowAllCardsBut.TabIndex = 13;
            this.ShowAllCardsBut.Text = "Show All";
            this.ShowAllCardsBut.UseVisualStyleBackColor = true;
            this.ShowAllCardsBut.Click += new System.EventHandler(this.ShowAllCardsBut_Click);
            // 
            // showAll_EventBut
            // 
            this.showAll_EventBut.Location = new System.Drawing.Point(34, 35);
            this.showAll_EventBut.Name = "showAll_EventBut";
            this.showAll_EventBut.Size = new System.Drawing.Size(139, 29);
            this.showAll_EventBut.TabIndex = 14;
            this.showAll_EventBut.Text = "Show All Events";
            this.showAll_EventBut.UseVisualStyleBackColor = true;
            this.showAll_EventBut.Click += new System.EventHandler(this.showAll_EventBut_Click);
            // 
            // CardListBox
            // 
            this.CardListBox.FormattingEnabled = true;
            this.CardListBox.ItemHeight = 25;
            this.CardListBox.Location = new System.Drawing.Point(697, 97);
            this.CardListBox.Name = "CardListBox";
            this.CardListBox.Size = new System.Drawing.Size(485, 604);
            this.CardListBox.TabIndex = 15;
            this.CardListBox.SelectedIndexChanged += new System.EventHandler(this.CardListBox_SelectedIndexChanged);
            // 
            // AddEventLabelBut
            // 
            this.AddEventLabelBut.Location = new System.Drawing.Point(195, 34);
            this.AddEventLabelBut.Name = "AddEventLabelBut";
            this.AddEventLabelBut.Size = new System.Drawing.Size(119, 29);
            this.AddEventLabelBut.TabIndex = 16;
            this.AddEventLabelBut.Text = "New EventLabel";
            this.AddEventLabelBut.UseVisualStyleBackColor = true;
            this.AddEventLabelBut.Click += new System.EventHandler(this.AddEventLabelBut_Click);
            // 
            // AddThemeBut
            // 
            this.AddThemeBut.Location = new System.Drawing.Point(863, 31);
            this.AddThemeBut.Name = "AddThemeBut";
            this.AddThemeBut.Size = new System.Drawing.Size(111, 32);
            this.AddThemeBut.TabIndex = 17;
            this.AddThemeBut.Text = "New";
            this.AddThemeBut.UseVisualStyleBackColor = true;
            this.AddThemeBut.Click += new System.EventHandler(this.AddThemeBut_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CardListBox);
            this.panel1.Controls.Add(this.eventLabelCB);
            this.panel1.Controls.Add(this.AddThemeBut);
            this.panel1.Controls.Add(this.ThemeCB);
            this.panel1.Controls.Add(this.AddEventLabelBut);
            this.panel1.Controls.Add(this.EventListBox);
            this.panel1.Controls.Add(this.AddCardBut);
            this.panel1.Controls.Add(this.showAll_EventBut);
            this.panel1.Controls.Add(this.RemCardBut);
            this.panel1.Controls.Add(this.ShowAllCardsBut);
            this.panel1.Controls.Add(this.EditCardBut);
            this.panel1.Controls.Add(this.EditEventBut);
            this.panel1.Controls.Add(this.AddEventBut);
            this.panel1.Controls.Add(this.RemEventBut);
            this.panel1.Location = new System.Drawing.Point(46, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1295, 788);
            this.panel1.TabIndex = 18;
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.tabPage1);
            this.MainTabControl.Controls.Add(this.tabPage2);
            this.MainTabControl.Location = new System.Drawing.Point(12, 12);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(1384, 858);
            this.MainTabControl.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1368, 811);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.AContentCreatedList);
            this.tabPage2.Controls.Add(this.QContentCreatedList);
            this.tabPage2.Controls.Add(this.CreatePDFBt);
            this.tabPage2.Controls.Add(this.ELAndTheCreatorBt);
            this.tabPage2.Controls.Add(this.AddCardCreatorBt);
            this.tabPage2.Controls.Add(this.ThemeBoxCreatorCB);
            this.tabPage2.Controls.Add(this.EvBoxCreatorCb);
            this.tabPage2.Controls.Add(this.ThemeCreatorTB);
            this.tabPage2.Controls.Add(this.EventLabelCreatorTB);
            this.tabPage2.Controls.Add(this.InsertLabel);
            this.tabPage2.Controls.Add(this.AContentTB);
            this.tabPage2.Controls.Add(this.QContentTB);
            this.tabPage2.Controls.Add(this.AHeadTB);
            this.tabPage2.Controls.Add(this.QHeadTB);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1368, 811);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // CreatePDFBt
            // 
            this.CreatePDFBt.Location = new System.Drawing.Point(459, 363);
            this.CreatePDFBt.Name = "CreatePDFBt";
            this.CreatePDFBt.Size = new System.Drawing.Size(280, 63);
            this.CreatePDFBt.TabIndex = 12;
            this.CreatePDFBt.Text = "Erstelle PDF";
            this.CreatePDFBt.UseVisualStyleBackColor = true;
            this.CreatePDFBt.Click += new System.EventHandler(this.CreatePDFBt_Click);
            // 
            // ELAndTheCreatorBt
            // 
            this.ELAndTheCreatorBt.Location = new System.Drawing.Point(89, 311);
            this.ELAndTheCreatorBt.Name = "ELAndTheCreatorBt";
            this.ELAndTheCreatorBt.Size = new System.Drawing.Size(148, 51);
            this.ELAndTheCreatorBt.TabIndex = 10;
            this.ELAndTheCreatorBt.Text = "Erstellen";
            this.ELAndTheCreatorBt.UseVisualStyleBackColor = true;
            this.ELAndTheCreatorBt.Click += new System.EventHandler(this.ELAndTheCreatorBt_Click);
            // 
            // AddCardCreatorBt
            // 
            this.AddCardCreatorBt.Location = new System.Drawing.Point(459, 278);
            this.AddCardCreatorBt.Name = "AddCardCreatorBt";
            this.AddCardCreatorBt.Size = new System.Drawing.Size(280, 52);
            this.AddCardCreatorBt.TabIndex = 9;
            this.AddCardCreatorBt.Text = "Karte Erstellen";
            this.AddCardCreatorBt.UseVisualStyleBackColor = true;
            this.AddCardCreatorBt.Click += new System.EventHandler(this.AddCardCreatorBt_Click);
            // 
            // ThemeBoxCreatorCB
            // 
            this.ThemeBoxCreatorCB.FormattingEnabled = true;
            this.ThemeBoxCreatorCB.Location = new System.Drawing.Point(618, 68);
            this.ThemeBoxCreatorCB.Name = "ThemeBoxCreatorCB";
            this.ThemeBoxCreatorCB.Size = new System.Drawing.Size(121, 33);
            this.ThemeBoxCreatorCB.TabIndex = 8;
            this.ThemeBoxCreatorCB.SelectedIndexChanged += new System.EventHandler(this.ThemeBoxCreatorCB_SelectedIndexChanged);
            // 
            // EvBoxCreatorCb
            // 
            this.EvBoxCreatorCb.FormattingEnabled = true;
            this.EvBoxCreatorCb.Location = new System.Drawing.Point(466, 68);
            this.EvBoxCreatorCb.Name = "EvBoxCreatorCb";
            this.EvBoxCreatorCb.Size = new System.Drawing.Size(121, 33);
            this.EvBoxCreatorCb.TabIndex = 7;
            this.EvBoxCreatorCb.SelectedIndexChanged += new System.EventHandler(this.EvBoxCreatorCb_SelectedIndexChanged);
            // 
            // ThemeCreatorTB
            // 
            this.ThemeCreatorTB.Location = new System.Drawing.Point(89, 260);
            this.ThemeCreatorTB.Name = "ThemeCreatorTB";
            this.ThemeCreatorTB.Size = new System.Drawing.Size(148, 31);
            this.ThemeCreatorTB.TabIndex = 6;
            // 
            // EventLabelCreatorTB
            // 
            this.EventLabelCreatorTB.Location = new System.Drawing.Point(89, 207);
            this.EventLabelCreatorTB.Name = "EventLabelCreatorTB";
            this.EventLabelCreatorTB.Size = new System.Drawing.Size(148, 31);
            this.EventLabelCreatorTB.TabIndex = 5;
            // 
            // InsertLabel
            // 
            this.InsertLabel.AutoSize = true;
            this.InsertLabel.Location = new System.Drawing.Point(529, 39);
            this.InsertLabel.Name = "InsertLabel";
            this.InsertLabel.Size = new System.Drawing.Size(158, 25);
            this.InsertLabel.TabIndex = 4;
            this.InsertLabel.Text = "Currently Insert";
            // 
            // AContentTB
            // 
            this.AContentTB.Location = new System.Drawing.Point(618, 217);
            this.AContentTB.Name = "AContentTB";
            this.AContentTB.Size = new System.Drawing.Size(121, 31);
            this.AContentTB.TabIndex = 3;
            // 
            // QContentTB
            // 
            this.QContentTB.Location = new System.Drawing.Point(466, 217);
            this.QContentTB.Name = "QContentTB";
            this.QContentTB.Size = new System.Drawing.Size(121, 31);
            this.QContentTB.TabIndex = 2;
            // 
            // AHeadTB
            // 
            this.AHeadTB.Location = new System.Drawing.Point(618, 158);
            this.AHeadTB.Name = "AHeadTB";
            this.AHeadTB.Size = new System.Drawing.Size(121, 31);
            this.AHeadTB.TabIndex = 1;
            // 
            // QHeadTB
            // 
            this.QHeadTB.Location = new System.Drawing.Point(466, 158);
            this.QHeadTB.Name = "QHeadTB";
            this.QHeadTB.Size = new System.Drawing.Size(121, 31);
            this.QHeadTB.TabIndex = 0;
            // 
            // QContentCreatedList
            // 
            this.QContentCreatedList.FormattingEnabled = true;
            this.QContentCreatedList.ItemHeight = 25;
            this.QContentCreatedList.Location = new System.Drawing.Point(858, 63);
            this.QContentCreatedList.Name = "QContentCreatedList";
            this.QContentCreatedList.Size = new System.Drawing.Size(178, 354);
            this.QContentCreatedList.TabIndex = 13;
            // 
            // AContentCreatedList
            // 
            this.AContentCreatedList.FormattingEnabled = true;
            this.AContentCreatedList.ItemHeight = 25;
            this.AContentCreatedList.Location = new System.Drawing.Point(1101, 63);
            this.AContentCreatedList.Name = "AContentCreatedList";
            this.AContentCreatedList.Size = new System.Drawing.Size(178, 354);
            this.AContentCreatedList.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1489, 935);
            this.Controls.Add(this.MainTabControl);
            this.Name = "Form1";
            this.Text = "KKSys";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.MainTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

       

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ComboBox eventLabelCB;
        private System.Windows.Forms.ComboBox ThemeCB;
        private System.Windows.Forms.ListBox EventListBox;
        private System.Windows.Forms.Button AddCardBut;
        private System.Windows.Forms.Button RemCardBut;
        private System.Windows.Forms.Button EditCardBut;
        private System.Windows.Forms.Button EditEventBut;
        private System.Windows.Forms.Button RemEventBut;
        private System.Windows.Forms.Button AddEventBut;
        private System.Windows.Forms.Button ShowAllCardsBut;
        private System.Windows.Forms.Button showAll_EventBut;
        private System.Windows.Forms.ListBox CardListBox;
        private System.Windows.Forms.Button AddEventLabelBut;
        private System.Windows.Forms.Button AddThemeBut;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox ThemeBoxCreatorCB;
        private System.Windows.Forms.ComboBox EvBoxCreatorCb;
        private System.Windows.Forms.TextBox ThemeCreatorTB;
        private System.Windows.Forms.TextBox EventLabelCreatorTB;
        private System.Windows.Forms.Label InsertLabel;
        private System.Windows.Forms.TextBox AContentTB;
        private System.Windows.Forms.TextBox QContentTB;
        private System.Windows.Forms.TextBox AHeadTB;
        private System.Windows.Forms.TextBox QHeadTB;
        private System.Windows.Forms.Button ELAndTheCreatorBt;
        private System.Windows.Forms.Button AddCardCreatorBt;
        private System.Windows.Forms.Button CreatePDFBt;
        private System.Windows.Forms.ListBox AContentCreatedList;
        private System.Windows.Forms.ListBox QContentCreatedList;
    }
}

