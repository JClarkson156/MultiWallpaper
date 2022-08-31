namespace MultiWallpaper
{
    partial class frmCleanUp
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
            this.chkRemoveSmall = new System.Windows.Forms.CheckBox();
            this.chkRemoveDuplicate = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.chkMove = new System.Windows.Forms.CheckBox();
            this.chkMove2 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkRemoveSmall
            // 
            this.chkRemoveSmall.AutoSize = true;
            this.chkRemoveSmall.Location = new System.Drawing.Point(503, 132);
            this.chkRemoveSmall.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkRemoveSmall.Name = "chkRemoveSmall";
            this.chkRemoveSmall.Size = new System.Drawing.Size(101, 19);
            this.chkRemoveSmall.TabIndex = 0;
            this.chkRemoveSmall.Text = "Remove Small";
            this.chkRemoveSmall.UseVisualStyleBackColor = true;
            this.chkRemoveSmall.CheckedChanged += new System.EventHandler(this.chkRemoveSmall_CheckedChanged);
            // 
            // chkRemoveDuplicate
            // 
            this.chkRemoveDuplicate.AutoSize = true;
            this.chkRemoveDuplicate.Location = new System.Drawing.Point(175, 196);
            this.chkRemoveDuplicate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkRemoveDuplicate.Name = "chkRemoveDuplicate";
            this.chkRemoveDuplicate.Size = new System.Drawing.Size(122, 19);
            this.chkRemoveDuplicate.TabIndex = 1;
            this.chkRemoveDuplicate.Text = "Remove Duplicate";
            this.chkRemoveDuplicate.UseVisualStyleBackColor = true;
            this.chkRemoveDuplicate.CheckedChanged += new System.EventHandler(this.chkRemoveDuplicate_CheckedChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(379, 338);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 27);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(507, 338);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 27);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // chkMove
            // 
            this.chkMove.AutoSize = true;
            this.chkMove.Location = new System.Drawing.Point(479, 235);
            this.chkMove.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkMove.Name = "chkMove";
            this.chkMove.Size = new System.Drawing.Size(56, 19);
            this.chkMove.TabIndex = 4;
            this.chkMove.Text = "Move";
            this.chkMove.UseVisualStyleBackColor = true;
            this.chkMove.CheckedChanged += new System.EventHandler(this.chkMove_CheckedChanged);
            // 
            // chkMove2
            // 
            this.chkMove2.AutoSize = true;
            this.chkMove2.Location = new System.Drawing.Point(648, 235);
            this.chkMove2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chkMove2.Name = "chkMove2";
            this.chkMove2.Size = new System.Drawing.Size(62, 19);
            this.chkMove2.TabIndex = 5;
            this.chkMove2.Text = "Move2";
            this.chkMove2.UseVisualStyleBackColor = true;
            this.chkMove2.CheckedChanged += new System.EventHandler(this.chkMove2_CheckedChanged);
            // 
            // frmCleanUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 519);
            this.Controls.Add(this.chkMove2);
            this.Controls.Add(this.chkMove);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkRemoveDuplicate);
            this.Controls.Add(this.chkRemoveSmall);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmCleanUp";
            this.Text = "frmCleanUp";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkRemoveSmall;
        private System.Windows.Forms.CheckBox chkRemoveDuplicate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkMove;
        private System.Windows.Forms.CheckBox chkMove2;
    }
}