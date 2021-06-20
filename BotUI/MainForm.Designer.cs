
namespace BotUI
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
            this.m_CoinInfoTable = new System.Windows.Forms.DataGridView();
            this.m_ActiveOrderTable = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.m_CoinInfoTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_ActiveOrderTable)).BeginInit();
            this.SuspendLayout();
            // 
            // m_CoinInfoTable
            // 
            this.m_CoinInfoTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_CoinInfoTable.Location = new System.Drawing.Point(12, 12);
            this.m_CoinInfoTable.Name = "m_CoinInfoTable";
            this.m_CoinInfoTable.RowTemplate.Height = 25;
            this.m_CoinInfoTable.Size = new System.Drawing.Size(776, 171);
            this.m_CoinInfoTable.TabIndex = 1;
            // 
            // m_ActiveOrderTable
            // 
            this.m_ActiveOrderTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_ActiveOrderTable.Location = new System.Drawing.Point(12, 201);
            this.m_ActiveOrderTable.Name = "m_ActiveOrderTable";
            this.m_ActiveOrderTable.RowTemplate.Height = 25;
            this.m_ActiveOrderTable.Size = new System.Drawing.Size(776, 187);
            this.m_ActiveOrderTable.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.m_ActiveOrderTable);
            this.Controls.Add(this.m_CoinInfoTable);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.m_CoinInfoTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_ActiveOrderTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView m_CoinInfoTable;
        private System.Windows.Forms.DataGridView m_ActiveOrderTable;
    }
}

