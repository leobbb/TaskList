namespace TaskList
{
    partial class xmlForm
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
            this.lbl2 = new System.Windows.Forms.Label();
            this.lblShow = new System.Windows.Forms.Label();
            this.btnDoing = new System.Windows.Forms.Button();
            this.btnDone = new System.Windows.Forms.Button();
            this.cklShow = new System.Windows.Forms.CheckedListBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbl2
            // 
            this.lbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl2.Location = new System.Drawing.Point(40, 207);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(309, 23);
            this.lbl2.TabIndex = 16;
            this.lbl2.Text = "双击任务可以改变任务的状态";
            this.lbl2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShow
            // 
            this.lblShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblShow.Location = new System.Drawing.Point(40, 53);
            this.lblShow.Name = "lblShow";
            this.lblShow.Size = new System.Drawing.Size(309, 18);
            this.lblShow.TabIndex = 15;
            this.lblShow.Text = "show";
            this.lblShow.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDoing
            // 
            this.btnDoing.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnDoing.Location = new System.Drawing.Point(61, 242);
            this.btnDoing.Name = "btnDoing";
            this.btnDoing.Size = new System.Drawing.Size(75, 23);
            this.btnDoing.TabIndex = 12;
            this.btnDoing.Text = "正在进行中的";
            this.btnDoing.UseVisualStyleBackColor = false;
            this.btnDoing.Click += new System.EventHandler(this.btnDoing_Click);
            // 
            // btnDone
            // 
            this.btnDone.Location = new System.Drawing.Point(228, 242);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(75, 23);
            this.btnDone.TabIndex = 13;
            this.btnDone.Text = "查看已完成";
            this.btnDone.UseVisualStyleBackColor = true;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // cklShow
            // 
            this.cklShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cklShow.FormattingEnabled = true;
            this.cklShow.HorizontalScrollbar = true;
            this.cklShow.Location = new System.Drawing.Point(40, 74);
            this.cklShow.Name = "cklShow";
            this.cklShow.Size = new System.Drawing.Size(309, 130);
            this.cklShow.TabIndex = 14;
            this.cklShow.TabStop = false;
            this.cklShow.DoubleClick += new System.EventHandler(this.cklShow_DoubleClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(274, 27);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "输入待办事项";
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(122, 29);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(135, 20);
            this.txtContent.TabIndex = 9;
            // 
            // xmlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 283);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lblShow);
            this.Controls.Add(this.btnDoing);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.cklShow);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtContent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "xmlForm";
            this.Text = "xmlForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.xmlForm_FormClosing);
            this.Load += new System.EventHandler(this.xmlForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lblShow;
        private System.Windows.Forms.Button btnDoing;
        private System.Windows.Forms.Button btnDone;
        private System.Windows.Forms.CheckedListBox cklShow;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtContent;
    }
}