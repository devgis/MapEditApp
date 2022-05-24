using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapInfo.Data;
using MapInfo.Engine;

namespace Devgis.MapApp
{
    public partial class DataInfo : Form
    {
        List<String> _listLayers;
        public DataInfo(List<String> listLayers)
        {
            InitializeComponent();
            this._listLayers=listLayers;
        }

        private void DataInfo_Load(object sender, EventArgs e)
        {
            foreach (String sTable in _listLayers)
            {
                this.cbTables.Items.Add(sTable);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cbTables.Text.Trim()))
            {
                PublicDim.ShowInfoMessage("请选择表进行查看！");
                return;
            }
            else
            {
                Table tInfoTable = Session.Current.Catalog.GetTable(cbTables.Text);
                if (tInfoTable == null)
                {
                    PublicDim.ShowErrorMessage("查找表信息失败，请检查！");
                }
                else
                {
                    //清除旧信息
                    dgvInfoList.Rows.Clear();
                    dgvInfoList.Columns.Clear();

                    //创建表格信息
                    foreach (Column c in tInfoTable.TableInfo.Columns)
                    {
                        dgvInfoList.Columns.Add(c.Alias, c.Alias);
                    }

                    //加载数据
                    foreach (Feature feature in (tInfoTable as MapInfo.Data.ITableFeatureCollection))
                    {
                        int index = dgvInfoList.Rows.Add();
                        foreach (Column c in tInfoTable.TableInfo.Columns)
                        {
                            dgvInfoList.Rows[index].Cells[c.Alias].Value = feature[c.Alias];
                        }
                    }

                }
            }
        }
    }
}