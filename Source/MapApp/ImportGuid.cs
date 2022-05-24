using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Devgis.MapApp
{
    public partial class ImportGuid : Form
    {
        public ImportGuid()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {

        }

        private void btEntity_Click(object sender, EventArgs e)
        {

        }

        private void btSelectExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "老版Excel文件(.xls)|*.xls|新版Excel文件(.xlsx)|*.xlsx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbExcelFilePath.Text = ofd.FileName;
            }
        }
    }
}