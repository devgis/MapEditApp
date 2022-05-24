namespace Devgis.MapApp
{
    partial class DataInfo
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataInfo));
            this.pTop = new System.Windows.Forms.Panel();
            this.pDataGrid = new System.Windows.Forms.Panel();
            this.dgvInfoList = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cbTables = new System.Windows.Forms.ComboBox();
            this.btnShow = new System.Windows.Forms.Button();
            this.pTop.SuspendLayout();
            this.pDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfoList)).BeginInit();
            this.SuspendLayout();
            // 
            // pTop
            // 
            this.pTop.Controls.Add(this.btnShow);
            this.pTop.Controls.Add(this.cbTables);
            this.pTop.Controls.Add(this.label1);
            this.pTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTop.Location = new System.Drawing.Point(0, 0);
            this.pTop.Name = "pTop";
            this.pTop.Size = new System.Drawing.Size(638, 30);
            this.pTop.TabIndex = 0;
            // 
            // pDataGrid
            // 
            this.pDataGrid.Controls.Add(this.dgvInfoList);
            this.pDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pDataGrid.Location = new System.Drawing.Point(0, 30);
            this.pDataGrid.Name = "pDataGrid";
            this.pDataGrid.Size = new System.Drawing.Size(638, 342);
            this.pDataGrid.TabIndex = 1;
            // 
            // dgvInfoList
            // 
            this.dgvInfoList.AllowUserToAddRows = false;
            this.dgvInfoList.AllowUserToDeleteRows = false;
            this.dgvInfoList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInfoList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInfoList.Location = new System.Drawing.Point(0, 0);
            this.dgvInfoList.Name = "dgvInfoList";
            this.dgvInfoList.ReadOnly = true;
            this.dgvInfoList.RowTemplate.Height = 23;
            this.dgvInfoList.Size = new System.Drawing.Size(638, 342);
            this.dgvInfoList.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "图层：";
            // 
            // cbTables
            // 
            this.cbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTables.FormattingEnabled = true;
            this.cbTables.Location = new System.Drawing.Point(53, 5);
            this.cbTables.Name = "cbTables";
            this.cbTables.Size = new System.Drawing.Size(152, 20);
            this.cbTables.TabIndex = 1;
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(212, 4);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 23);
            this.btnShow.TabIndex = 2;
            this.btnShow.Text = "查看(&V)";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // DataInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 372);
            this.Controls.Add(this.pDataGrid);
            this.Controls.Add(this.pTop);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "属性表";
            this.Load += new System.EventHandler(this.DataInfo_Load);
            this.pTop.ResumeLayout(false);
            this.pTop.PerformLayout();
            this.pDataGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInfoList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pTop;
        private System.Windows.Forms.Panel pDataGrid;
        private System.Windows.Forms.DataGridView dgvInfoList;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.ComboBox cbTables;
        private System.Windows.Forms.Label label1;
    }
}