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
            buttonApply = new System.Windows.Forms.Button();
            notifyIconMain = new System.Windows.Forms.NotifyIcon(components);
            contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(components);
            labelRefreshMin = new System.Windows.Forms.Label();
            trackBarRefreshRate = new System.Windows.Forms.TrackBar();
            labelRefreshMax = new System.Windows.Forms.Label();
            labelRefresh = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            dataGridViewSerialDevices = new System.Windows.Forms.DataGridView();
            formMainBindingSource = new System.Windows.Forms.BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)trackBarRefreshRate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSerialDevices).BeginInit();
            ((System.ComponentModel.ISupportInitialize)formMainBindingSource).BeginInit();
            SuspendLayout();
            // 
            // buttonCheckAll
            // 
            buttonCheckAll.Location = new System.Drawing.Point(14, 479);
            buttonCheckAll.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonCheckAll.Name = "buttonCheckAll";
            buttonCheckAll.Size = new System.Drawing.Size(93, 25);
            buttonCheckAll.TabIndex = 1;
            buttonCheckAll.Text = "All";
            buttonCheckAll.UseVisualStyleBackColor = true;
            buttonCheckAll.Click += buttonCheckAll_Click;
            // 
            // buttonApply
            // 
            buttonApply.Location = new System.Drawing.Point(271, 479);
            buttonApply.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new System.Drawing.Size(93, 25);
            buttonApply.TabIndex = 4;
            buttonApply.Text = "Hide";
            buttonApply.UseVisualStyleBackColor = true;
            buttonApply.Click += buttonApply_Click;
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
            // labelRefreshMin
            // 
            labelRefreshMin.AutoSize = true;
            labelRefreshMin.Location = new System.Drawing.Point(18, 517);
            labelRefreshMin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelRefreshMin.Name = "labelRefreshMin";
            labelRefreshMin.Size = new System.Drawing.Size(21, 15);
            labelRefreshMin.TabIndex = 6;
            labelRefreshMin.Text = "1 s";
            // 
            // trackBarRefreshRate
            // 
            trackBarRefreshRate.Location = new System.Drawing.Point(21, 535);
            trackBarRefreshRate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trackBarRefreshRate.Minimum = 1;
            trackBarRefreshRate.Name = "trackBarRefreshRate";
            trackBarRefreshRate.Size = new System.Drawing.Size(340, 45);
            trackBarRefreshRate.TabIndex = 7;
            trackBarRefreshRate.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            trackBarRefreshRate.Value = 10;
            trackBarRefreshRate.ValueChanged += trackBarRefreshRate_ValueChanged;
            // 
            // labelRefreshMax
            // 
            labelRefreshMax.AutoSize = true;
            labelRefreshMax.Location = new System.Drawing.Point(316, 517);
            labelRefreshMax.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelRefreshMax.Name = "labelRefreshMax";
            labelRefreshMax.Size = new System.Drawing.Size(43, 15);
            labelRefreshMax.TabIndex = 8;
            labelRefreshMax.Text = "30 min";
            // 
            // labelRefresh
            // 
            labelRefresh.AutoSize = true;
            labelRefresh.Location = new System.Drawing.Point(108, 517);
            labelRefresh.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            labelRefresh.Name = "labelRefresh";
            labelRefresh.Size = new System.Drawing.Size(75, 15);
            labelRefresh.TabIndex = 9;
            labelRefresh.Text = "Refresh rate: ";
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
            // 
            // formMainBindingSource
            // 
            formMainBindingSource.DataSource = typeof(FormMain);
            // 
            // FormMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(378, 601);
            Controls.Add(dataGridViewSerialDevices);
            Controls.Add(label1);
            Controls.Add(labelRefresh);
            Controls.Add(labelRefreshMax);
            Controls.Add(trackBarRefreshRate);
            Controls.Add(labelRefreshMin);
            Controls.Add(buttonApply);
            Controls.Add(buttonCheckAll);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FormMain";
            Text = "SerialDevices";
            FormClosing += FormMain_FormClosing;
            ((System.ComponentModel.ISupportInitialize)trackBarRefreshRate).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSerialDevices).EndInit();
            ((System.ComponentModel.ISupportInitialize)formMainBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Button buttonCheckAll;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.NotifyIcon notifyIconMain;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMain;
        private System.Windows.Forms.Label labelRefreshMin;
        private System.Windows.Forms.TrackBar trackBarRefreshRate;
        private System.Windows.Forms.Label labelRefreshMax;
        private System.Windows.Forms.Label labelRefresh;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView dataGridViewSerialDevices;
        private System.Windows.Forms.BindingSource formMainBindingSource;
    }
}

