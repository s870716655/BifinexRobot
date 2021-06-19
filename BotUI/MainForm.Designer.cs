
namespace BTCTrandBot
{
    partial class MainForm
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
            if (disposing && (components != null)) {
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
            this.Lab_PriceBTCUSD = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Lab_PriceBTCUSD
            // 
            this.Lab_PriceBTCUSD.BackColor = System.Drawing.Color.White;
            this.Lab_PriceBTCUSD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Lab_PriceBTCUSD.ForeColor = System.Drawing.Color.Black;
            this.Lab_PriceBTCUSD.Location = new System.Drawing.Point(41, 27);
            this.Lab_PriceBTCUSD.Name = "Lab_PriceBTCUSD";
            this.Lab_PriceBTCUSD.Size = new System.Drawing.Size(100, 20);
            this.Lab_PriceBTCUSD.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Lab_PriceBTCUSD);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Lab_PriceBTCUSD;
    }
}

