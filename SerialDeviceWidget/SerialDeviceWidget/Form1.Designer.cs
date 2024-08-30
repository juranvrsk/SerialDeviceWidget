namespace SerialDeviceWidget
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            buttonCheckAll = new System.Windows.Forms.Button();
            notifyIconMain = new System.Windows.Forms.NotifyIcon(components);
            contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(components);
            labelRefresh = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            dataGridViewSerialDevices = new System.Windows.Forms.DataGridView();
            numericUpDownRefreshRate = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSerialDevices).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRefreshRate).BeginInit();
            SuspendLayout();
            // 
            // buttonCheckAll
            // 
            buttonCheckAll.Location = new System.Drawing.Point(12, 477);
            buttonCheckAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonCheckAll.Name = "buttonCheckAll";
            buttonCheckAll.Size = new System.Drawing.Size(120, 25);
            buttonCheckAll.TabIndex = 1;
            buttonCheckAll.Text = "All";
            buttonCheckAll.UseVisualStyleBackColor = true;
            buttonCheckAll.Click += buttonCheckAll_Click;
            // 
            // notifyIconMain
            // 
            notifyIconMain.ContextMenuStrip = contextMenuStripMain;
            notifyIconMain.Icon = (System.Drawing.Icon)resources.GetObject("notifyIconMain.Icon");
            notifyIconMain.Text = "SerialDeviceWidget";
            notifyIconMain.Visible = true;
            notifyIconMain.DoubleClick += notifyIconMain_Click;
            // 
            // contextMenuStripMain
            // 
            contextMenuStripMain.Name = "contextMenuStrip1";
            contextMenuStripMain.Size = new System.Drawing.Size(61, 4);
            // 
            // labelRefresh
            // 
            labelRefresh.AutoSize = true;
            labelRefresh.Location = new System.Drawing.Point(142, 482);
            labelRefresh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelRefresh.Name = "labelRefresh";
            labelRefresh.Size = new System.Drawing.Size(95, 15);
            labelRefresh.TabIndex = 9;
            labelRefresh.Text = "Refresh rate, sec:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(149, 15);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(95, 15);
            label1.TabIndex = 11;
            label1.Text = "Serial devices list";
            // 
            // dataGridViewSerialDevices
            // 
            dataGridViewSerialDevices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSerialDevices.Location = new System.Drawing.Point(12, 33);
            dataGridViewSerialDevices.Name = "dataGridViewSerialDevices";
            dataGridViewSerialDevices.Size = new System.Drawing.Size(354, 440);
            dataGridViewSerialDevices.TabIndex = 12;
            dataGridViewSerialDevices.CellValueChanged += dataGridViewSerialDevicesCell_ValueChanged;
            dataGridViewSerialDevices.ColumnHeaderMouseClick += dataGridViewSerialDevices_ColumnHeaderMouseClick;
            dataGridViewSerialDevices.CurrentCellDirtyStateChanged += dataGridViewSerialDevices_CurrentCellDirtyStateChanged;
            // 
            // numericUpDownRefreshRate
            // 
            numericUpDownRefreshRate.Location = new System.Drawing.Point(246, 480);
            numericUpDownRefreshRate.Name = "numericUpDownRefreshRate";
            numericUpDownRefreshRate.Size = new System.Drawing.Size(120, 23);
            numericUpDownRefreshRate.TabIndex = 13;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(378, 601);
            Controls.Add(numericUpDownRefreshRate);
            Controls.Add(dataGridViewSerialDevices);
            Controls.Add(label1);
            Controls.Add(labelRefresh);
            Controls.Add(buttonCheckAll);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FormMain";
            Text = "SerialDevices";
            FormClosing += FormMain_FormClosing;
            ((System.ComponentModel.ISupportInitialize)dataGridViewSerialDevices).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownRefreshRate).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button buttonCheckAll;
        private System.Windows.Forms.NotifyIcon notifyIconMain;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMain;
        private System.Windows.Forms.Label labelRefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridViewSerialDevices;
        private System.Windows.Forms.NumericUpDown numericUpDownRefreshRate;
    }
}

