using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapInfo.Data;

namespace Devgis.MapApp
{
    public partial class InfoBox : Form
    {
        Feature _feature;
        public InfoBox(Feature feature)
        {
            InitializeComponent();
            this._feature = feature;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InfoBox_Load(object sender, EventArgs e)
        {
            if (_feature != null)
            {
                int index= dgvFeatureInfoList.Rows.Add();
                dgvFeatureInfoList.Rows[index].Cells["CName"].Value = "所属图层";
                dgvFeatureInfoList.Rows[index].Cells["CValue"].Value = _feature.Table.TableInfo.Alias;

                //index = dgvFeatureInfoList.Rows.Add();
                //dgvFeatureInfoList.Rows[index].Cells["CName"].Value = "图元类型";
                //dgvFeatureInfoList.Rows[index].Cells["CValue"].Value = _feature.Geometry.TypeName;

                index = dgvFeatureInfoList.Rows.Add();
                dgvFeatureInfoList.Rows[index].Cells["CName"].Value = "Key";
                dgvFeatureInfoList.Rows[index].Cells["CValue"].Value = _feature.Key.Value;

                //index = dgvFeatureInfoList.Rows.Add();
                //dgvFeatureInfoList.Rows[index].Cells["CName"].Value = "Style";
                //dgvFeatureInfoList.Rows[index].Cells["CValue"].Value = _feature.Style.ToString();


                index = dgvFeatureInfoList.Rows.Add();
                dgvFeatureInfoList.Rows[index].Cells["CName"].Value = "中点X";
                dgvFeatureInfoList.Rows[index].Cells["CValue"].Value = _feature.Geometry.Centroid.x;

                index = dgvFeatureInfoList.Rows.Add();
                dgvFeatureInfoList.Rows[index].Cells["CName"].Value = "中点Y";
                dgvFeatureInfoList.Rows[index].Cells["CValue"].Value = _feature.Geometry.Centroid.y;

                foreach (Column c in _feature.Columns)
                {
                    index = dgvFeatureInfoList.Rows.Add();
                    dgvFeatureInfoList.Rows[index].Cells["CName"].Value = c.Alias;
                    dgvFeatureInfoList.Rows[index].Cells["CValue"].Value = _feature[c.Alias];
                }
            }
        }
    }
}