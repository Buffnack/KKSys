using System;
using System.ComponentModel;

namespace KKSysForms
{
    partial class MainFrameHandle
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.eventPaneToday = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.eventPaneToday);
            this.panel1.Location = new System.Drawing.Point(27, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1679, 877);
            this.panel1.TabIndex = 0;
            // 
            // eventPaneToday
            // 
            this.eventPaneToday.Location = new System.Drawing.Point(43, 67);
            this.eventPaneToday.Name = "eventPaneToday";
            this.eventPaneToday.Size = new System.Drawing.Size(293, 787);
            this.eventPaneToday.TabIndex = 0;
            // 
            // MainFrameHandle
            // 
            this.ClientSize = new System.Drawing.Size(1728, 936);
            this.Controls.Add(this.panel1);
            this.Name = "MainFrameHandle";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

            this.Controls.Remove(this.panel1);
            this.Controls.Add(new CardCreatorPanel());

        }

       

        #endregion
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel eventPaneToday;
    }
}

