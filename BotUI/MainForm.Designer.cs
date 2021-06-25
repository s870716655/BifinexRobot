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
            this.m_ZoomChart = new ZoomChart();
            this.m_CoinInfoTable = new System.Windows.Forms.DataGridView();
            this.m_ActiveOrderTable = new System.Windows.Forms.DataGridView();
            this.m_StartTimePicker = new System.Windows.Forms.DateTimePicker();
            this.m_EndTimePicker = new System.Windows.Forms.DateTimePicker();
            this.m_TimeUnitComboBox = new System.Windows.Forms.ComboBox();
            this.m_LabStartTime = new System.Windows.Forms.Label();
            this.m_LabEndTime = new System.Windows.Forms.Label();
            this.m_LabTimeUnit = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.m_CoinInfoTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_ActiveOrderTable)).BeginInit();
            this.SuspendLayout();
            // 
            // m_ZoomChart
            // 
            this.m_ZoomChart.Location = new System.Drawing.Point(12, 96);
            this.m_ZoomChart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_ZoomChart.Name = "m_ZoomChart";
            this.m_ZoomChart.Size = new System.Drawing.Size(659, 262);
            this.m_ZoomChart.TabIndex = 3;
            // 
            // m_CoinInfoTable
            // 
            this.m_CoinInfoTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_CoinInfoTable.Location = new System.Drawing.Point(38, 513);
            this.m_CoinInfoTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_CoinInfoTable.Name = "m_CoinInfoTable";
            this.m_CoinInfoTable.RowTemplate.Height = 25;
            this.m_CoinInfoTable.Size = new System.Drawing.Size(557, 150);
            this.m_CoinInfoTable.TabIndex = 1;
            // 
            // m_ActiveOrderTable
            // 
            this.m_ActiveOrderTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.m_ActiveOrderTable.Location = new System.Drawing.Point(680, 513);
            this.m_ActiveOrderTable.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.m_ActiveOrderTable.Name = "m_ActiveOrderTable";
            this.m_ActiveOrderTable.RowTemplate.Height = 25;
            this.m_ActiveOrderTable.Size = new System.Drawing.Size(557, 150);
            this.m_ActiveOrderTable.TabIndex = 2;
            // 
            // m_StartTimePicker
            // 
            this.m_StartTimePicker.Location = new System.Drawing.Point(12, 25);
            this.m_StartTimePicker.Name = "m_StartTimePicker";
            this.m_StartTimePicker.Size = new System.Drawing.Size(135, 22);
            this.m_StartTimePicker.TabIndex = 4;
            // 
            // m_EndTimePicker
            // 
            this.m_EndTimePicker.Location = new System.Drawing.Point(169, 25);
            this.m_EndTimePicker.Name = "m_EndTimePicker";
            this.m_EndTimePicker.Size = new System.Drawing.Size(135, 22);
            this.m_EndTimePicker.TabIndex = 5;
            // 
            // m_TimeUnitComboBox
            // 
            this.m_TimeUnitComboBox.FormattingEnabled = true;
            this.m_TimeUnitComboBox.Location = new System.Drawing.Point(320, 25);
            this.m_TimeUnitComboBox.Name = "m_TimeUnitComboBox";
            this.m_TimeUnitComboBox.Size = new System.Drawing.Size(121, 20);
            this.m_TimeUnitComboBox.TabIndex = 7;
            // 
            // m_LabStartTime
            // 
            this.m_LabStartTime.AutoSize = true;
            this.m_LabStartTime.Location = new System.Drawing.Point(10, 9);
            this.m_LabStartTime.Name = "m_LabStartTime";
            this.m_LabStartTime.Size = new System.Drawing.Size(53, 12);
            this.m_LabStartTime.TabIndex = 8;
            this.m_LabStartTime.Text = "起始日期";
            // 
            // m_LabEndTime
            // 
            this.m_LabEndTime.AutoSize = true;
            this.m_LabEndTime.Location = new System.Drawing.Point(167, 9);
            this.m_LabEndTime.Name = "m_LabEndTime";
            this.m_LabEndTime.Size = new System.Drawing.Size(53, 12);
            this.m_LabEndTime.TabIndex = 9;
            this.m_LabEndTime.Text = "結束日期";
            // 
            // m_LabTimeUnit
            // 
            this.m_LabTimeUnit.AutoSize = true;
            this.m_LabTimeUnit.Location = new System.Drawing.Point(318, 9);
            this.m_LabTimeUnit.Name = "m_LabTimeUnit";
            this.m_LabTimeUnit.Size = new System.Drawing.Size(53, 12);
            this.m_LabTimeUnit.TabIndex = 10;
            this.m_LabTimeUnit.Text = "時間單位";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 790);
            this.Controls.Add(this.m_LabTimeUnit);
            this.Controls.Add(this.m_LabEndTime);
            this.Controls.Add(this.m_LabStartTime);
            this.Controls.Add(this.m_TimeUnitComboBox);
            this.Controls.Add(this.m_EndTimePicker);
            this.Controls.Add(this.m_StartTimePicker);
            this.Controls.Add(this.m_ActiveOrderTable);
            this.Controls.Add(this.m_CoinInfoTable);
            this.Controls.Add(this.m_ZoomChart);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.m_CoinInfoTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_ActiveOrderTable)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView m_CoinInfoTable;
        private System.Windows.Forms.DataGridView m_ActiveOrderTable;
        private ZoomChart m_ZoomChart;
        private System.Windows.Forms.DateTimePicker m_StartTimePicker;
        private System.Windows.Forms.DateTimePicker m_EndTimePicker;
        private System.Windows.Forms.ComboBox m_TimeUnitComboBox;
        private System.Windows.Forms.Label m_LabStartTime;
        private System.Windows.Forms.Label m_LabEndTime;
        private System.Windows.Forms.Label m_LabTimeUnit;
    }
}

