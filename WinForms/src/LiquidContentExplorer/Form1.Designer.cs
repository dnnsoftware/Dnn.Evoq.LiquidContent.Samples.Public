namespace LiquidContentExplorer
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textApiEndPoint = new System.Windows.Forms.TextBox();
            this.btnLoadContent = new System.Windows.Forms.Button();
            this.btnClearContent = new System.Windows.Forms.Button();
            this.textApiToken = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textpiVersion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeViewLC = new System.Windows.Forms.TreeView();
            this.richTextNotes = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "API &Endpoint";
            // 
            // textApiEndPoint
            // 
            this.textApiEndPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textApiEndPoint.Location = new System.Drawing.Point(83, 9);
            this.textApiEndPoint.Name = "textApiEndPoint";
            this.textApiEndPoint.Size = new System.Drawing.Size(413, 21);
            this.textApiEndPoint.TabIndex = 1;
            // 
            // btnLoadContent
            // 
            this.btnLoadContent.Location = new System.Drawing.Point(546, 9);
            this.btnLoadContent.Name = "btnLoadContent";
            this.btnLoadContent.Size = new System.Drawing.Size(112, 20);
            this.btnLoadContent.TabIndex = 2;
            this.btnLoadContent.Text = "&Load Content";
            this.btnLoadContent.UseVisualStyleBackColor = true;
            this.btnLoadContent.Click += new System.EventHandler(this.btnLoadContent_Click);
            // 
            // btnClearContent
            // 
            this.btnClearContent.Location = new System.Drawing.Point(546, 42);
            this.btnClearContent.Name = "btnClearContent";
            this.btnClearContent.Size = new System.Drawing.Size(112, 20);
            this.btnClearContent.TabIndex = 3;
            this.btnClearContent.Text = "&Clear Content";
            this.btnClearContent.UseVisualStyleBackColor = true;
            this.btnClearContent.Click += new System.EventHandler(this.btnClearContent_Click);
            // 
            // textApiToken
            // 
            this.textApiToken.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textApiToken.Location = new System.Drawing.Point(83, 39);
            this.textApiToken.Name = "textApiToken";
            this.textApiToken.Size = new System.Drawing.Size(413, 21);
            this.textApiToken.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "API &Token";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.textpiVersion);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.textApiEndPoint);
            this.panel1.Controls.Add(this.textApiToken);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnLoadContent);
            this.panel1.Controls.Add(this.btnClearContent);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(669, 97);
            this.panel1.TabIndex = 6;
            // 
            // textpiVersion
            // 
            this.textpiVersion.BackColor = System.Drawing.SystemColors.Window;
            this.textpiVersion.Enabled = false;
            this.textpiVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textpiVersion.Location = new System.Drawing.Point(83, 69);
            this.textpiVersion.Name = "textpiVersion";
            this.textpiVersion.Size = new System.Drawing.Size(108, 21);
            this.textpiVersion.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "API Version";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Location = new System.Drawing.Point(12, 142);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(670, 522);
            this.panel2.TabIndex = 7;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeViewLC);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextNotes);
            this.splitContainer1.Size = new System.Drawing.Size(670, 522);
            this.splitContainer1.SplitterDistance = 223;
            this.splitContainer1.TabIndex = 0;
            // 
            // treeViewLC
            // 
            this.treeViewLC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewLC.Location = new System.Drawing.Point(0, 0);
            this.treeViewLC.Name = "treeViewLC";
            this.treeViewLC.Size = new System.Drawing.Size(189, 332);
            this.treeViewLC.TabIndex = 0;
            this.treeViewLC.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewLC_NodeMouseClick);
            // 
            // richTextNotes
            // 
            this.richTextNotes.Font = new System.Drawing.Font("mononoki", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextNotes.Location = new System.Drawing.Point(0, 0);
            this.richTextNotes.Name = "richTextNotes";
            this.richTextNotes.Size = new System.Drawing.Size(237, 332);
            this.richTextNotes.TabIndex = 0;
            this.richTextNotes.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "C&ontent Types and Items:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(703, 700);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textApiEndPoint;
        private System.Windows.Forms.Button btnLoadContent;
        private System.Windows.Forms.Button btnClearContent;
        private System.Windows.Forms.TextBox textApiToken;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView treeViewLC;
        private System.Windows.Forms.RichTextBox richTextNotes;
        private System.Windows.Forms.TextBox textpiVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

