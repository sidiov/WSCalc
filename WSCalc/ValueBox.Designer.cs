namespace WSCalc
{
    partial class ValueBox
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
            this.label_label = new System.Windows.Forms.Label();
            this.tb_Value = new System.Windows.Forms.TextBox();
            this.b_set = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label_label
            // 
            this.label_label.AutoSize = true;
            this.label_label.Location = new System.Drawing.Point(12, 9);
            this.label_label.Name = "label_label";
            this.label_label.Size = new System.Drawing.Size(50, 13);
            this.label_label.TabIndex = 0;
            this.label_label.Text = "Iterations";
            // 
            // tb_Value
            // 
            this.tb_Value.Location = new System.Drawing.Point(12, 25);
            this.tb_Value.Name = "tb_Value";
            this.tb_Value.Size = new System.Drawing.Size(100, 20);
            this.tb_Value.TabIndex = 1;
            this.tb_Value.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_Value_KeyPress);
            // 
            // b_set
            // 
            this.b_set.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.b_set.Location = new System.Drawing.Point(12, 51);
            this.b_set.Name = "b_set";
            this.b_set.Size = new System.Drawing.Size(55, 23);
            this.b_set.TabIndex = 2;
            this.b_set.Text = "Set";
            this.b_set.UseVisualStyleBackColor = true;
            this.b_set.Click += new System.EventHandler(this.b_set_Click);
            // 
            // ValueBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(126, 89);
            this.Controls.Add(this.b_set);
            this.Controls.Add(this.tb_Value);
            this.Controls.Add(this.label_label);
            this.Name = "ValueBox";
            this.Text = "Value Change";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_label;
        private System.Windows.Forms.TextBox tb_Value;
        private System.Windows.Forms.Button b_set;
    }
}