namespace PadRoom
{
    partial class ConnectivityStatusForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lrStatusLabel = new System.Windows.Forms.Label();
            this.iPadStatusLabel = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(14, 30);
            this.label2.MinimumSize = new System.Drawing.Size(70, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Lightroom: ";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(14, 59);
            this.label1.MinimumSize = new System.Drawing.Size(70, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "iPad: ";
            // 
            // lrStatusLabel
            // 
            this.lrStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lrStatusLabel.AutoSize = true;
            this.lrStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lrStatusLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.lrStatusLabel.Location = new System.Drawing.Point(95, 30);
            this.lrStatusLabel.MinimumSize = new System.Drawing.Size(70, 20);
            this.lrStatusLabel.Name = "lrStatusLabel";
            this.lrStatusLabel.Size = new System.Drawing.Size(103, 20);
            this.lrStatusLabel.TabIndex = 3;
            this.lrStatusLabel.Text = "CONNECTING ...";
            // 
            // iPadStatusLabel
            // 
            this.iPadStatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.iPadStatusLabel.AutoSize = true;
            this.iPadStatusLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.iPadStatusLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.iPadStatusLabel.Location = new System.Drawing.Point(95, 59);
            this.iPadStatusLabel.MinimumSize = new System.Drawing.Size(70, 20);
            this.iPadStatusLabel.Name = "iPadStatusLabel";
            this.iPadStatusLabel.Size = new System.Drawing.Size(103, 20);
            this.iPadStatusLabel.TabIndex = 4;
            this.iPadStatusLabel.Text = "CONNECTING ...";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.textBox1.Location = new System.Drawing.Point(12, 102);
            this.textBox1.MinimumSize = new System.Drawing.Size(200, 60);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(200, 80);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "Note:  Open Lightroom first. Give some time for a plugin to load. Open iPad app a" +
    "nd connect.";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ConnectivityStatusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(226, 204);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.iPadStatusLabel);
            this.Controls.Add(this.lrStatusLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "ConnectivityStatusForm";
            this.Text = "PadRoom";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lrStatusLabel;
        private System.Windows.Forms.Label iPadStatusLabel;
        private System.Windows.Forms.TextBox textBox1;
    }
}