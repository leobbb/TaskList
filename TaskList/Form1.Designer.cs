namespace TaskList
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtContent = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cklShow = new System.Windows.Forms.CheckedListBox();
            this.btnDone = new System.Windows.Forms.Button();
            this.btnDoing = new System.Windows.Forms.Button();
            this.lblShow = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(111, 12);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(135, 20);
            this.txtContent.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "输入待办事项";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(263, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cklShow
            // 
            this.cklShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cklShow.FormattingEnabled = true;
            this.cklShow.Location = new System.Drawing.Point(29, 57);
            this.cklShow.Name = "cklShow";
            this.cklShow.Size = new System.Drawing.Size(309, 148);
            this.cklShow.TabIndex = 3;
            this.cklShow.SelectedIndexChanged += new System.EventHandler(this.cklShow_SelectedIndexChanged);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(240, 225);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 23);
            this.btnDone.TabIndex = 4;
            this.btnDone.Text = "查看已完成";
            this.btnDone.UseVisualStyleBackColor = true;
            // 
            // btnDoing
            // 
            this.btnDoing.Location = new System.Drawing.Point(50, 225);
            this.btnDoing.Name = "btnDoing";
            this.btnDoing.Size = new System.Drawing.Size(75, 23);
            this.btnDoing.TabIndex = 5;
            this.btnDoing.Text = "正在进行中的";
            this.btnDoing.UseVisualStyleBackColor = true;
            this.btnDoing.Click += new System.EventHandler(this.btnDoing_Click);
            // 
            // lblShow
            // 
            this.lblShow.AutoSize = true;
            this.lblShow.Location = new System.Drawing.Point(108, 41);
            this.lblShow.Name = "lblShow";
            this.lblShow.Size = new System.Drawing.Size(32, 13);
            this.lblShow.TabIndex = 6;
            this.lblShow.Text = "show";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 325);
            this.Controls.Add(this.lblShow);
            this.Controls.Add(this.btnDoing);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.cklShow);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtContent);
            this.Name = "Form1";
            this.Text = "待办事项";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckedListBox cklShow;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.Button btnDoing;
        private System.Windows.Forms.Label lblShow;
    }
}

