namespace BroncoIDE
{
    partial class IDE
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IDE));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.inputPane = new FastColoredTextBoxNS.FastColoredTextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.generateButton = new System.Windows.Forms.ToolStripMenuItem();
            this.outputPane = new System.Windows.Forms.TextBox();
            this.BackgroundParser = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputPane)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.inputPane);
            this.splitContainer1.Panel1.Controls.Add(this.menuStrip);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.outputPane);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 212;
            this.splitContainer1.TabIndex = 0;
            // 
            // inputPane
            // 
            this.inputPane.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.inputPane.AutoScrollMinSize = new System.Drawing.Size(121, 18);
            this.inputPane.BackBrush = null;
            this.inputPane.CharHeight = 18;
            this.inputPane.CharWidth = 10;
            this.inputPane.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.inputPane.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputPane.IsReplaceMode = false;
            this.inputPane.Location = new System.Drawing.Point(0, 28);
            this.inputPane.Name = "inputPane";
            this.inputPane.Paddings = new System.Windows.Forms.Padding(0);
            this.inputPane.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.inputPane.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("inputPane.ServiceColors")));
            this.inputPane.Size = new System.Drawing.Size(800, 184);
            this.inputPane.TabIndex = 1;
            this.inputPane.Text = "inputPane";
            this.inputPane.Zoom = 100;
            this.inputPane.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.inputPane_TextChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateButton});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(800, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // generateButton
            // 
            this.generateButton.Name = "generateButton";
            this.generateButton.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.generateButton.Size = new System.Drawing.Size(83, 24);
            this.generateButton.Text = "Generate";
            this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
            // 
            // outputPane
            // 
            this.outputPane.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.outputPane.BackColor = System.Drawing.SystemColors.Window;
            this.outputPane.Location = new System.Drawing.Point(3, 3);
            this.outputPane.Multiline = true;
            this.outputPane.Name = "outputPane";
            this.outputPane.ReadOnly = true;
            this.outputPane.Size = new System.Drawing.Size(794, 228);
            this.outputPane.TabIndex = 0;
            this.outputPane.Text = "test";
            // 
            // BackgroundParser
            // 
            this.BackgroundParser.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackgroundParser_DoWork);
            this.BackgroundParser.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackgroundParser_RunWorkerCompleted);
            // 
            // IDE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "IDE";
            this.Text = "Bronco Editor";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputPane)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainer1;
        private TextBox outputPane;
        private FastColoredTextBoxNS.FastColoredTextBox inputPane;
        private MenuStrip menuStrip;
        private ToolStripMenuItem generateButton;
        private System.ComponentModel.BackgroundWorker BackgroundParser;
    }
}