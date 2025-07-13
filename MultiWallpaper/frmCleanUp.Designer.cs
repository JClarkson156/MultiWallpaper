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
            chkRemoveSmall = new System.Windows.Forms.CheckBox();
            chkRemoveDuplicate = new System.Windows.Forms.CheckBox();
            btnSave = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            chkMove = new System.Windows.Forms.CheckBox();
            chkMove2 = new System.Windows.Forms.CheckBox();
            checkBox1 = new System.Windows.Forms.CheckBox();
            SuspendLayout();
            // 
            // chkRemoveSmall
            // 
            chkRemoveSmall.AutoSize = true;
            chkRemoveSmall.Location = new System.Drawing.Point(503, 132);
            chkRemoveSmall.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chkRemoveSmall.Name = "chkRemoveSmall";
            chkRemoveSmall.Size = new System.Drawing.Size(101, 19);
            chkRemoveSmall.TabIndex = 0;
            chkRemoveSmall.Text = "Remove Small";
            chkRemoveSmall.UseVisualStyleBackColor = true;
            chkRemoveSmall.CheckedChanged += chkRemoveSmall_CheckedChanged;
            // 
            // chkRemoveDuplicate
            // 
            chkRemoveDuplicate.AutoSize = true;
            chkRemoveDuplicate.Location = new System.Drawing.Point(175, 196);
            chkRemoveDuplicate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chkRemoveDuplicate.Name = "chkRemoveDuplicate";
            chkRemoveDuplicate.Size = new System.Drawing.Size(122, 19);
            chkRemoveDuplicate.TabIndex = 1;
            chkRemoveDuplicate.Text = "Remove Duplicate";
            chkRemoveDuplicate.UseVisualStyleBackColor = true;
            chkRemoveDuplicate.CheckedChanged += chkRemoveDuplicate_CheckedChanged;
            // 
            // btnSave
            // 
            btnSave.Location = new System.Drawing.Point(379, 338);
            btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(88, 27);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            btnCancel.Location = new System.Drawing.Point(507, 338);
            btnCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(88, 27);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // chkMove
            // 
            chkMove.AutoSize = true;
            chkMove.Location = new System.Drawing.Point(479, 235);
            chkMove.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chkMove.Name = "chkMove";
            chkMove.Size = new System.Drawing.Size(56, 19);
            chkMove.TabIndex = 4;
            chkMove.Text = "Move";
            chkMove.UseVisualStyleBackColor = true;
            chkMove.CheckedChanged += chkMove_CheckedChanged;
            // 
            // chkMove2
            // 
            chkMove2.AutoSize = true;
            chkMove2.Location = new System.Drawing.Point(648, 235);
            chkMove2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            chkMove2.Name = "chkMove2";
            chkMove2.Size = new System.Drawing.Size(62, 19);
            chkMove2.TabIndex = 5;
            chkMove2.Text = "Move2";
            chkMove2.UseVisualStyleBackColor = true;
            chkMove2.CheckedChanged += chkMove2_CheckedChanged;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(275, 269);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(62, 19);
            checkBox1.TabIndex = 6;
            checkBox1.Text = "Move3";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // frmCleanUp
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(933, 519);
            Controls.Add(checkBox1);
            Controls.Add(chkMove2);
            Controls.Add(chkMove);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(chkRemoveDuplicate);
            Controls.Add(chkRemoveSmall);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "frmCleanUp";
            Text = "frmCleanUp";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.CheckBox chkRemoveSmall;
        private System.Windows.Forms.CheckBox chkRemoveDuplicate;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkMove;
        private System.Windows.Forms.CheckBox chkMove2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}