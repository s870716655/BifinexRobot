﻿
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
            this.m_CoinInfoTable.Location = new System.Drawing.Point(10, 10);
            this.m_CoinInfoTable.Name = "m_CoinInfoTable";
            this.m_CoinInfoTable.RowTemplate.Height = 25;
            this.m_CoinInfoTable.Size = new System.Drawing.Size(770, 200);
            this.m_CoinInfoTable.TabIndex = 1;
            // 
            // m_ActiveOrderTable
            // 
            this.m_ActiveOrderTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_ActiveOrderTable.Location = new System.Drawing.Point(10, 220);
            this.m_ActiveOrderTable.Name = "m_ActiveOrderTable";
            this.m_ActiveOrderTable.RowTemplate.Height = 25;
            this.m_ActiveOrderTable.Size = new System.Drawing.Size(770, 200);
            this.m_ActiveOrderTable.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 1500);
            this.Controls.Add(this.m_ActiveOrderTable);
            this.Controls.Add(this.m_CoinInfoTable);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.m_CoinInfoTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_ActiveOrderTable)).EndInit();
            this.ResumeLayout(false);

            //
            // ZoomChart
            //
            m_ZoomChart = new ZoomChart();
            m_ZoomChart.Location = new System.Drawing.Point(10, 430);
            m_ZoomChart.Size = new System.Drawing.Size(770, 350);
            this.Controls.Add(m_ZoomChart);
        }

        #endregion
        private System.Windows.Forms.DataGridView m_CoinInfoTable;
        private System.Windows.Forms.DataGridView m_ActiveOrderTable;
        private ZoomChart m_ZoomChart;
    }
}

