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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.buttonCheckAll = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonApply = new System.Windows.Forms.Button();
            this.notifyIconMain = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.labelRefreshMin = new System.Windows.Forms.Label();
            this.trackBarRefreshRate = new System.Windows.Forms.TrackBar();
            this.labelRefreshMax = new System.Windows.Forms.Label();
            this.labelRefresh = new System.Windows.Forms.Label();
            this.checkedListBoxSerialDevices = new System.Windows.Forms.CheckedListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRefreshRate)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCheckAll
            // 
            this.buttonCheckAll.Location = new System.Drawing.Point(18, 415);
            this.buttonCheckAll.Name = "buttonCheckAll";
            this.buttonCheckAll.Size = new System.Drawing.Size(60, 22);
            this.buttonCheckAll.TabIndex = 1;
            this.buttonCheckAll.Text = "All";
            this.buttonCheckAll.UseVisualStyleBackColor = true;
            this.buttonCheckAll.Click += new System.EventHandler(this.buttonCheckAll_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(122, 415);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(80, 22);
            this.buttonRefresh.TabIndex = 3;
            this.buttonRefresh.Text = "Refresh now";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(252, 415);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(60, 22);
            this.buttonApply.TabIndex = 4;
            this.buttonApply.Text = "Hide";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // notifyIconMain
            // 
            this.notifyIconMain.ContextMenuStrip = this.contextMenuStripMain;
            this.notifyIconMain.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconMain.Icon")));
            this.notifyIconMain.Text = "SerialDeviceWidget";
            this.notifyIconMain.Visible = true;
            // 
            // contextMenuStripMain
            // 
            this.contextMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemRefresh,
            this.toolStripSeparator2,
            this.toolStripSeparator1,
            this.toolStripMenuItemExit});
            this.contextMenuStripMain.Name = "contextMenuStrip1";
            this.contextMenuStripMain.Size = new System.Drawing.Size(114, 60);
            // 
            // toolStripMenuItemRefresh
            // 
            this.toolStripMenuItemRefresh.Name = "toolStripMenuItemRefresh";
            this.toolStripMenuItemRefresh.Size = new System.Drawing.Size(113, 22);
            this.toolStripMenuItemRefresh.Text = "Refresh";
            this.toolStripMenuItemRefresh.Click += new System.EventHandler(this.toolStripMenuItemRefresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(110, 6);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(110, 6);
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(113, 22);
            this.toolStripMenuItemExit.Text = "Exit";
            this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 60000;
            // 
            // labelRefreshMin
            // 
            this.labelRefreshMin.AutoSize = true;
            this.labelRefreshMin.Location = new System.Drawing.Point(15, 448);
            this.labelRefreshMin.Name = "labelRefreshMin";
            this.labelRefreshMin.Size = new System.Drawing.Size(41, 13);
            this.labelRefreshMin.TabIndex = 6;
            this.labelRefreshMin.Text = "100 ms";
            // 
            // trackBarRefreshRate
            // 
            this.trackBarRefreshRate.Location = new System.Drawing.Point(18, 464);
            this.trackBarRefreshRate.Minimum = 1;
            this.trackBarRefreshRate.Name = "trackBarRefreshRate";
            this.trackBarRefreshRate.Size = new System.Drawing.Size(291, 45);
            this.trackBarRefreshRate.TabIndex = 7;
            this.trackBarRefreshRate.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarRefreshRate.Value = 10;
            // 
            // labelRefreshMax
            // 
            this.labelRefreshMax.AutoSize = true;
            this.labelRefreshMax.Location = new System.Drawing.Point(271, 448);
            this.labelRefreshMax.Name = "labelRefreshMax";
            this.labelRefreshMax.Size = new System.Drawing.Size(38, 13);
            this.labelRefreshMax.TabIndex = 8;
            this.labelRefreshMax.Text = "10 min";
            // 
            // labelRefresh
            // 
            this.labelRefresh.AutoSize = true;
            this.labelRefresh.Location = new System.Drawing.Point(93, 448);
            this.labelRefresh.Name = "labelRefresh";
            this.labelRefresh.Size = new System.Drawing.Size(71, 13);
            this.labelRefresh.TabIndex = 9;
            this.labelRefresh.Text = "Refresh rate: ";
            // 
            // checkedListBoxSerialDevices
            // 
            this.checkedListBoxSerialDevices.FormattingEnabled = true;
            this.checkedListBoxSerialDevices.Location = new System.Drawing.Point(12, 30);
            this.checkedListBoxSerialDevices.Name = "checkedListBoxSerialDevices";
            this.checkedListBoxSerialDevices.Size = new System.Drawing.Size(300, 379);
            this.checkedListBoxSerialDevices.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Serial devices list";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 521);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkedListBoxSerialDevices);
            this.Controls.Add(this.labelRefresh);
            this.Controls.Add(this.labelRefreshMax);
            this.Controls.Add(this.trackBarRefreshRate);
            this.Controls.Add(this.labelRefreshMin);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.buttonCheckAll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormMain";
            this.Text = "SerialDevices";
            this.contextMenuStripMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRefreshRate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonCheckAll;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.NotifyIcon notifyIconMain;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMain;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.Label labelRefreshMin;
        private System.Windows.Forms.TrackBar trackBarRefreshRate;
        private System.Windows.Forms.Label labelRefreshMax;
        private System.Windows.Forms.Label labelRefresh;
        private System.Windows.Forms.CheckedListBox checkedListBoxSerialDevices;
        private System.Windows.Forms.Label label1;
    }
}

