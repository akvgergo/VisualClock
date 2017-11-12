namespace VisualClock {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.clock1 = new VisualClock.Clock();
            this.SuspendLayout();
            // 
            // clock1
            // 
            this.clock1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.clock1.ClockMode = VisualClock.ClockMode.Ticking;
            this.clock1.Location = new System.Drawing.Point(12, 12);
            this.clock1.Name = "clock1";
            this.clock1.Size = new System.Drawing.Size(198, 198);
            this.clock1.TabIndex = 0;
            this.clock1.Text = "clock1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lime;
            this.ClientSize = new System.Drawing.Size(226, 232);
            this.Controls.Add(this.clock1);
            this.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Form1";
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.ResumeLayout(false);

        }

        #endregion

        private Clock clock1;
    }
}

