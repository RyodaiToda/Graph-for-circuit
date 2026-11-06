namespace Graph
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNodeNToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addEdgeEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addRegisterRToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuMToolStripMenuItem
            // 
            this.menuMToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addNodeNToolStripMenuItem,
            this.addEdgeEToolStripMenuItem,
            this.addRegisterRToolStripMenuItem});
            this.menuMToolStripMenuItem.Name = "menuMToolStripMenuItem";
            this.menuMToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.menuMToolStripMenuItem.Text = "Menu(&M)";
            this.menuMToolStripMenuItem.Click += new System.EventHandler(this.menuMToolStripMenuItem_Click);
            // 
            // addNodeNToolStripMenuItem
            // 
            this.addNodeNToolStripMenuItem.Name = "addNodeNToolStripMenuItem";
            this.addNodeNToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addNodeNToolStripMenuItem.Text = "AddNode(&N)";
            this.addNodeNToolStripMenuItem.Click += new System.EventHandler(this.addNodeNToolStripMenuItem_Click);
            // 
            // addEdgeEToolStripMenuItem
            // 
            this.addEdgeEToolStripMenuItem.Name = "addEdgeEToolStripMenuItem";
            this.addEdgeEToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addEdgeEToolStripMenuItem.Text = "AddEdge(&E)";
            this.addEdgeEToolStripMenuItem.Click += new System.EventHandler(this.addEdgeEToolStripMenuItem_Click);
            // 
            // addRegisterRToolStripMenuItem
            // 
            this.addRegisterRToolStripMenuItem.Name = "addRegisterRToolStripMenuItem";
            this.addRegisterRToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addRegisterRToolStripMenuItem.Text = "AddRegister(&R)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 961);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuMToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addNodeNToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addEdgeEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRegisterRToolStripMenuItem;
    }
}

