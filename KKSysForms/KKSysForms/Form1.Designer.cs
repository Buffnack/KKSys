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
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
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
            this.contextMenuStrip1.SuspendLayout();
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
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // eventLabelCB
            // 
            this.eventLabelCB.FormattingEnabled = true;
            this.eventLabelCB.Location = new System.Drawing.Point(320, 35);
            this.eventLabelCB.Name = "eventLabelCB";
            this.eventLabelCB.Size = new System.Drawing.Size(171, 33);
            this.eventLabelCB.TabIndex = 3;
            // 
            // ThemeCB
            // 
            this.ThemeCB.FormattingEnabled = true;
            this.ThemeCB.Location = new System.Drawing.Point(978, 34);
            this.ThemeCB.Name = "ThemeCB";
            this.ThemeCB.Size = new System.Drawing.Size(170, 33);
            this.ThemeCB.TabIndex = 4;
            // 
            // EventListBox
            // 
            this.EventListBox.FormattingEnabled = true;
            this.EventListBox.ItemHeight = 25;
            this.EventListBox.Location = new System.Drawing.Point(20, 101);
            this.EventListBox.Name = "EventListBox";
            this.EventListBox.Size = new System.Drawing.Size(473, 604);
            this.EventListBox.TabIndex = 6;
            // 
            // AddCardBut
            // 
            this.AddCardBut.Location = new System.Drawing.Point(683, 720);
            this.AddCardBut.Name = "AddCardBut";
            this.AddCardBut.Size = new System.Drawing.Size(131, 51);
            this.AddCardBut.TabIndex = 7;
            this.AddCardBut.Text = "Add Card";
            this.AddCardBut.UseVisualStyleBackColor = true;
            // 
            // RemCardBut
            // 
            this.RemCardBut.Location = new System.Drawing.Point(836, 720);
            this.RemCardBut.Name = "RemCardBut";
            this.RemCardBut.Size = new System.Drawing.Size(103, 51);
            this.RemCardBut.TabIndex = 8;
            this.RemCardBut.Text = "Remove Card";
            this.RemCardBut.UseVisualStyleBackColor = true;
            // 
            // EditCardBut
            // 
            this.EditCardBut.Location = new System.Drawing.Point(978, 720);
            this.EditCardBut.Name = "EditCardBut";
            this.EditCardBut.Size = new System.Drawing.Size(168, 51);
            this.EditCardBut.TabIndex = 9;
            this.EditCardBut.Text = "Edit Card";
            this.EditCardBut.UseVisualStyleBackColor = true;
            // 
            // EditEventBut
            // 
            this.EditEventBut.Location = new System.Drawing.Point(323, 720);
            this.EditEventBut.Name = "EditEventBut";
            this.EditEventBut.Size = new System.Drawing.Size(168, 51);
            this.EditEventBut.TabIndex = 12;
            this.EditEventBut.Text = "Edit Event";
            this.EditEventBut.UseVisualStyleBackColor = true;
            // 
            // RemEventBut
            // 
            this.RemEventBut.Location = new System.Drawing.Point(181, 720);
            this.RemEventBut.Name = "RemEventBut";
            this.RemEventBut.Size = new System.Drawing.Size(103, 51);
            this.RemEventBut.TabIndex = 11;
            this.RemEventBut.Text = "Remove Event";
            this.RemEventBut.UseVisualStyleBackColor = true;
            // 
            // AddEventBut
            // 
            this.AddEventBut.Location = new System.Drawing.Point(28, 720);
            this.AddEventBut.Name = "AddEventBut";
            this.AddEventBut.Size = new System.Drawing.Size(131, 51);
            this.AddEventBut.TabIndex = 10;
            this.AddEventBut.Text = "Add Event";
            this.AddEventBut.UseVisualStyleBackColor = true;
            // 
            // ShowAllCardsBut
            // 
            this.ShowAllCardsBut.Location = new System.Drawing.Point(683, 34);
            this.ShowAllCardsBut.Name = "ShowAllCardsBut";
            this.ShowAllCardsBut.Size = new System.Drawing.Size(146, 33);
            this.ShowAllCardsBut.TabIndex = 13;
            this.ShowAllCardsBut.Text = "Show All";
            this.ShowAllCardsBut.UseVisualStyleBackColor = true;
            // 
            // showAll_EventBut
            // 
            this.showAll_EventBut.Location = new System.Drawing.Point(20, 39);
            this.showAll_EventBut.Name = "showAll_EventBut";
            this.showAll_EventBut.Size = new System.Drawing.Size(139, 29);
            this.showAll_EventBut.TabIndex = 14;
            this.showAll_EventBut.Text = "Show All Events";
            this.showAll_EventBut.UseVisualStyleBackColor = true;
            // 
            // CardListBox
            // 
            this.CardListBox.FormattingEnabled = true;
            this.CardListBox.ItemHeight = 25;
            this.CardListBox.Location = new System.Drawing.Point(683, 101);
            this.CardListBox.Name = "CardListBox";
            this.CardListBox.Size = new System.Drawing.Size(485, 604);
            this.CardListBox.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 793);
            this.Controls.Add(this.CardListBox);
            this.Controls.Add(this.showAll_EventBut);
            this.Controls.Add(this.ShowAllCardsBut);
            this.Controls.Add(this.EditEventBut);
            this.Controls.Add(this.RemEventBut);
            this.Controls.Add(this.AddEventBut);
            this.Controls.Add(this.EditCardBut);
            this.Controls.Add(this.RemCardBut);
            this.Controls.Add(this.AddCardBut);
            this.Controls.Add(this.EventListBox);
            this.Controls.Add(this.ThemeCB);
            this.Controls.Add(this.eventLabelCB);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

       

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
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
    }
}

