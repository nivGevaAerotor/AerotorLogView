using ScottPlot;

namespace AerotorViewLog_3._0
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.tBoxGetFile = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.vScrollBarOffset = new System.Windows.Forms.VScrollBar();
            this.dataGraph = new ScottPlot.FormsPlot();
            this.panelRight = new System.Windows.Forms.Panel();
            this.treeViewDataVals = new System.Windows.Forms.TreeView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panelTop.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.tBoxGetFile);
            this.panelTop.Controls.Add(this.btnBrowse);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1281, 54);
            this.panelTop.TabIndex = 0;
            // 
            // tBoxGetFile
            // 
            this.tBoxGetFile.Location = new System.Drawing.Point(109, 14);
            this.tBoxGetFile.Name = "tBoxGetFile";
            this.tBoxGetFile.Size = new System.Drawing.Size(526, 26);
            this.tBoxGetFile.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(13, 10);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(89, 33);
            this.btnBrowse.TabIndex = 0;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.vScrollBarOffset);
            this.panelLeft.Controls.Add(this.dataGraph);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeft.Location = new System.Drawing.Point(0, 54);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(949, 540);
            this.panelLeft.TabIndex = 1;
            // 
            // vScrollBarOffset
            // 
            this.vScrollBarOffset.Dock = System.Windows.Forms.DockStyle.Left;
            this.vScrollBarOffset.Location = new System.Drawing.Point(0, 0);
            this.vScrollBarOffset.Name = "vScrollBarOffset";
            this.vScrollBarOffset.Size = new System.Drawing.Size(26, 540);
            this.vScrollBarOffset.TabIndex = 1;
            this.vScrollBarOffset.Value = 45;
            this.vScrollBarOffset.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBarOffset_Scroll);
            // 
            // dataGraph
            // 
            this.dataGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGraph.Location = new System.Drawing.Point(0, 0);
            this.dataGraph.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.dataGraph.Name = "dataGraph";
            this.dataGraph.Size = new System.Drawing.Size(949, 540);
            this.dataGraph.TabIndex = 0;
            this.dataGraph.LeftClicked += new System.EventHandler(this.dataGraph_LeftClicked);
            // 
            // panelRight
            // 
            this.panelRight.Controls.Add(this.treeViewDataVals);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.Location = new System.Drawing.Point(949, 54);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(332, 540);
            this.panelRight.TabIndex = 2;
            // 
            // treeViewDataVals
            // 
            this.treeViewDataVals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDataVals.Location = new System.Drawing.Point(0, 0);
            this.treeViewDataVals.Name = "treeViewDataVals";
            this.treeViewDataVals.Size = new System.Drawing.Size(332, 540);
            this.treeViewDataVals.TabIndex = 1;
            this.treeViewDataVals.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewDataVals_NodeMouseDoubleClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1281, 594);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelTop);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.TextBox tBoxGetFile;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.VScrollBar vScrollBarOffset;
        private ScottPlot.FormsPlot dataGraph;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TreeView treeViewDataVals;
    }
}

