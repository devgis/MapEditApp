using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapInfo.Mapping;
using MapInfo.Data;
using MapInfo.Engine;
using MapInfo.Geometry;
using MapInfo.Styles;
using System.IO;
using MapInfo.Tools;
using System.Collections;
using MapInfo.Windows.Controls;
using MapInfo.Printing;
using MapInfo.Windows.Dialogs;
using MapInfo.Mapping.Thematics;
using MapInfo.Data;
using MapInfo.Engine;
using MapInfo.Geometry;
using MapInfo.Mapping;
using MapInfo.Windows.Controls;
using MapInfo.Tools;
using MapInfo.Styles;
using Devgis.Common;
using MapInfo.Mapping.Legends;
using System.Drawing.Printing;

namespace Devgis.MapApp
{
    /// <summary>
    /// 地图程序主窗体
    /// </summary>
    public partial class MainMap : Form
    {
        #region 变量定义
        CoordSys mapCS;//地图坐标系

        #region 鹰眼相关变量
        FeatureLayer eagleEye;
        Feature fRec;
        #endregion

        #region 测距变量
        double dblDistance = 0;
        DPoint dptStart;
        #endregion

        #region 测面积变量
        DPoint dptFirstPoint;
        ArrayList arrlstPoints = new ArrayList();
        #endregion

        #region 信息工具
        bool isInfoTool = false;
        #endregion

        #region 加载全图
        DPoint dpCenter;
        Double mapScale;
        #endregion

        #region 专题图
        private ITheme _thm;
        private FeatureLayer _themedFeatureLayer = null;
        private LabelLayer _themedLabelLayer = null;
        #endregion

        #region 添加线 多面体
        List<DPoint> listDP = new List<DPoint>();
        #endregion

        #region 添加圆 椭圆 矩形
        DPoint dpDrawStart;
        DPoint dpDrawEnd;
        #endregion
        #endregion

        #region 构造函数
        public MainMap()
        {
            InitializeComponent();
            mainMapControl.Map.ViewChangedEvent += new MapInfo.Mapping.ViewChangedEventHandler(Map_ViewChangedEvent);
            Map_ViewChangedEvent(this, null);
            //this.eagleMap.MouseWheelSupport = new MouseWheelSupport(MouseWheelBehavior.None, 10, 5);

            //添加自定义测量距离
            mainMapControl.Tools.Add(MapTool.DistanceTool, new CustomPolylineMapTool(true, true, true, mainMapControl.Viewer, mainMapControl.Handle.ToInt32(), mainMapControl.Tools, mainMapControl.Tools.MouseToolProperties, mainMapControl.Tools.MapToolProperties));

            //添加自定义测量面积
            mainMapControl.Tools.Add(MapTool.AreaTool, new CustomPolygonMapTool(true, true, true, mainMapControl.Viewer, mainMapControl.Handle.ToInt32(), mainMapControl.Tools, mainMapControl.Tools.MouseToolProperties, mainMapControl.Tools.MapToolProperties));

            //信息工具
            mainMapControl.Tools.Add(MapTool.InfoTool, new CustomPointMapTool(true, mainMapControl.Tools.FeatureViewer, mainMapControl.Handle.ToInt32(), mainMapControl.Tools, mainMapControl.Tools.MouseToolProperties, mainMapControl.Tools.MapToolProperties)); //信息工具
            
            mainMapControl.Tools.Used += new ToolUsedEventHandler(Tools_Used);

            mainMapControl.Tools.FeatureSelected += new FeatureSelectedEventHandler(Tools_FeatureSelected);
        }
        #endregion

        #region 窗体事件
        void Map_ViewChangedEvent(object sender, MapInfo.Mapping.ViewChangedEventArgs e)
        {
            // Display the zoom level
            Double dblZoom = System.Convert.ToDouble(String.Format("{0:E2}", mainMapControl.Map.Zoom.Value));
            if (mainStatusStrip.Items.Count > 0)
            {
                tsslZoomLabel.Text = "缩放: " + dblZoom.ToString() + " " + MapInfo.Geometry.CoordSys.DistanceUnitAbbreviation(mainMapControl.Map.Zoom.Unit);
            }

            if (mainMapControl.Map.Zoom.Value < 100)
            {
                mainMapControl.Map.Zoom = new Distance(100, DistanceUnit.Meter); 
            }
            else if (mainMapControl.Map.Zoom.Value > 140000)
            {
                mainMapControl.Map.Zoom = new Distance(140000, DistanceUnit.Meter); 
            }
        }

        private void eagleMap_MouseClick(object sender, MouseEventArgs e)
        {
            ////鹰眼地图点击时切换主地图到该点中兴
            //DPoint pt = new DPoint();
            //eagleMap.Map.DisplayTransform.FromDisplay(e.Location, out pt);
            //mainMapControl.Map.Center = pt;

        }

        private void MainMap_Load(object sender, EventArgs e)
        {
            //加载地图
            this.LoadMap();

            mapCS = mainMapControl.Map.GetDisplayCoordSys();//获取并保存地图坐标系

            this.mainMapControl.Tools.LeftButtonTool = MapTool.Pan; //默认使用平移工具

            Distance mapDistance = new Distance(this.mainMapControl.Map.Zoom.Value * 1609.344 - 0.0001, DistanceUnit.Meter); //地图单位为米
            mainMapControl.Map.Zoom = mapDistance;//设置显示比例
            
            //加载地图比例尺
            LoadScaleBar();

            //加载地图绘图图层
            LoadDrawLayer();

            //获取地图参数
            dpCenter = mainMapControl.Map.Center;
            mapScale = mainMapControl.Map.Scale;

            //InitTables();//加载地图业务数据
            
        }

        private void MainMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PublicDim.ShowQuestion("确认退出系统？") != DialogResult.OK)
            {
                e.Cancel = true;
                return;
            }
        }

        /// <summary>
        /// 开启导入向导
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiImportGuid_Click(object sender, EventArgs e)
        {
            ImportGuid frmImportGuid = new ImportGuid();
            if (frmImportGuid.ShowDialog() == DialogResult.OK)
            {
                //执行导入操作
                PublicDim.ShowInfoMessage("执行导入操作！");
            }
        }
        #endregion

        #region 菜单工具栏事件
        #region 地图工具栏点击
        private void mapToolButton_Click(object sender, EventArgs e)
        {
            //处理工具栏Button事件
            if (sender is ToolStripButton)
            {
                String ToolButtonName = (sender as ToolStripButton).Name;
                DoButtonMethod(ToolButtonName);

            }
        }
        #endregion

        #region 关于点击
        private void tsmiHelpAbout_Click(object sender, EventArgs e)
        {
            AboutBox frmAboutBox = new AboutBox();
            frmAboutBox.ShowDialog();
        }
        #endregion

        #region 图元工具使用事件
        void Tools_Used(object sender, ToolUsedEventArgs e)
        {
            try
            {
                switch (e.ToolName)
                {
                    //测量距离
                    case "DistanceTool":
                        #region 测量距离工具
                        switch (e.ToolStatus)
                        {
                            case ToolStatus.Start:
                                dblDistance = 0;
                                dptStart = e.MapCoordinate;
                                break;
                            case ToolStatus.InProgress:
                                dblDistance += CoordSys.Distance(DistanceType.Spherical, mainMapControl.Map.Zoom.Unit, mainMapControl.Map.GetDisplayCoordSys(), dptStart, e.MapCoordinate);
                                //PublicDim.ShowInfoMessage("总长度为:" + string.Format("{0:F3}", dblDistance) + " " + CoordSys.DistanceUnitAbbreviation(mainMapControl.Map.Zoom.Unit).ToString());
                                //dptStart = e.MapCoordinate;
                                break;
                            case ToolStatus.End:
                                dblDistance += CoordSys.Distance(DistanceType.Spherical, mainMapControl.Map.Zoom.Unit, mainMapControl.Map.GetDisplayCoordSys(), dptStart, e.MapCoordinate);
                                PublicDim.ShowInfoMessage("总长度为:" + string.Format("{0:F3}", dblDistance) + " " + CoordSys.DistanceUnitAbbreviation(mainMapControl.Map.Zoom.Unit).ToString());
                                mainMapControl.Map.Invalidate(true);
                                break;
                            default:
                                break;
                        }
                        #endregion
                        break;
                    //测量面积
                    case "AreaTool":
                        #region 计算面积工具
                        switch (e.ToolStatus)
                        {
                            case ToolStatus.Start:
                                arrlstPoints.Clear();
                                dptFirstPoint = e.MapCoordinate;
                                arrlstPoints.Add(e.MapCoordinate);
                                break;
                            case ToolStatus.InProgress:
                                arrlstPoints.Add(e.MapCoordinate);
                                break;
                            case ToolStatus.End:
                                //构造一个闭合环
                                arrlstPoints.Add(e.MapCoordinate);
                                int intCount = arrlstPoints.Count;
                                if (intCount <= 3)
                                {
                                    PublicDim.ShowInfoMessage("请画3个以上的点形成面来测量你所要的面积");
                                    return;
                                }
                                DPoint[] dptPoints = new DPoint[intCount];
                                for (int i = 0; i <= intCount - 1; i++)
                                {
                                    dptPoints[i] = (DPoint)arrlstPoints[i];

                                }
                                dptPoints[intCount - 1] = dptFirstPoint;

                                //用闭合的环构造一个面        
                                AreaUnit costAreaUnit;
                                costAreaUnit = CoordSys.GetAreaUnitCounterpart(mainMapControl.Map.Zoom.Unit);
                                CoordSys objCoordSys = this.mainMapControl.Map.GetDisplayCoordSys();
                                Polygon objPolygon = new Polygon(objCoordSys, CurveSegmentType.Linear, dptPoints);
                                //if (objPolygon == null)
                                //    return;
                                PublicDim.ShowInfoMessage("总面积为:" + string.Format("{0:F3}", objPolygon.Area(costAreaUnit)) + "  " + CoordSys.AreaUnitAbbreviation(costAreaUnit));
                                mainMapControl.Map.Invalidate(true);
                                break;
                            default:
                                break;
                        }
                        #endregion
                        break;
                    case "InfoTool":
                        #region 信息工具
                        this.mainMapControl.Tools.LeftButtonTool = MapTool.Select;
                        isInfoTool = true;
                        #endregion
                        break;
                    case "AddPoint":
                        #region 绘制点
                        switch (e.ToolStatus)
                        {
                            case ToolStatus.Start:
                                break;
                            case ToolStatus.InProgress:
                                break;
                            case ToolStatus.End:
                                AddFeature frmAddFeature = new AddFeature(StyleType.Point);
                                if (frmAddFeature.ShowDialog() == DialogResult.OK)
                                {
                                    Feature f=new Feature(tDarwTalbe.TableInfo.Columns);
                                    f.Geometry = new MapInfo.Geometry.Point(mapCS, e.MapCoordinate);
                                    f.Style=frmAddFeature.FeatureStyle;
                                    f["ID"]=Guid.NewGuid().ToString();
                                    f["NAME"]=frmAddFeature.Name;
                                    tDarwTalbe.InsertFeature(f);
                                }
                                break;
                        }
                        #endregion
                        break;
                    case "AddPolyline":
                        #region 绘制折现
                        switch (e.ToolStatus)
                        {
                            case ToolStatus.Start:
                                listDP.Clear();
                                listDP.Add(e.MapCoordinate);
                                break;
                            case ToolStatus.InProgress:
                                listDP.Add(e.MapCoordinate);
                                break;
                            case ToolStatus.End:
                                AddFeature frmAddFeature = new AddFeature(StyleType.Line);
                                if (frmAddFeature.ShowDialog() == DialogResult.OK)
                                {
                                    DPoint[] dp=new DPoint[listDP.Count];
                                    for (int i = 0; i < listDP.Count; i++)
                                    {
                                        dp[i] = listDP[i];
                                    }
                                    Feature f=new Feature(tDarwTalbe.TableInfo.Columns);
                                    f.Geometry = new MapInfo.Geometry.MultiCurve(mapCS, CurveSegmentType.Linear, dp);
                                    f.Style=frmAddFeature.FeatureStyle;
                                    f["ID"]=Guid.NewGuid().ToString();
                                    f["NAME"]=frmAddFeature.Name;
                                    tDarwTalbe.InsertFeature(f);
                                }
                                break;
                        }
                        #endregion
                        break;
                    case "AddCircle":
                        #region 绘制圆
                        switch (e.ToolStatus)
                        {
                            case ToolStatus.Start:
                                dpDrawStart=e.MapCoordinate;
                                break;
                            case ToolStatus.InProgress:
                                break;
                            case ToolStatus.End:
                                dpDrawEnd=e.MapCoordinate;
                                AddFeature frmAddFeature = new AddFeature(StyleType.Shape);
                                if (frmAddFeature.ShowDialog() == DialogResult.OK)
                                {
                                    double Radius = (double)(Math.Sqrt(Math.Pow(dpDrawStart.y - dpDrawEnd.y, 2) + Math.Pow(dpDrawStart.x - dpDrawEnd.x, 2)));
                                    Ellipse ellipse = new Ellipse(mapCS, dpDrawStart, Radius, Radius, mainMapControl.Map.Zoom.Unit, DistanceType.Spherical);
                                    Feature f = new Feature(tDarwTalbe.TableInfo.Columns);
                                    f.Geometry = ellipse;
                                    f.Style = frmAddFeature.FeatureStyle;
                                    f["ID"] = Guid.NewGuid().ToString();
                                    f["NAME"] = frmAddFeature.Name;
                                    tDarwTalbe.InsertFeature(f);
                                }
                                break;
                        }
                        #endregion
                        break;
                    case "AddEllipse":
                        #region 绘制椭圆
                        switch (e.ToolStatus)
                        {
                            case ToolStatus.Start:
                                dpDrawStart=e.MapCoordinate;
                                break;
                            case ToolStatus.InProgress:
                                break;
                            case ToolStatus.End:
                                dpDrawEnd=e.MapCoordinate;
                                AddFeature frmAddFeature = new AddFeature(StyleType.Shape);
                                if (frmAddFeature.ShowDialog() == DialogResult.OK)
                                {
                                    Ellipse ellipse = new Ellipse(mapCS,new DRect(dpDrawStart,dpDrawEnd));
                                    Feature f = new Feature(tDarwTalbe.TableInfo.Columns);
                                    f.Geometry = ellipse;
                                    f.Style = frmAddFeature.FeatureStyle;
                                    f["ID"] = Guid.NewGuid().ToString();
                                    f["NAME"] = frmAddFeature.Name;
                                    tDarwTalbe.InsertFeature(f);
                                }
                                break;
                        }
                        #endregion
                        break;
                    case "AddRectangle":
                        #region 绘制矩形
                        switch (e.ToolStatus)
                        {
                            case ToolStatus.Start:
                                dpDrawStart = e.MapCoordinate;
                                break;
                            case ToolStatus.InProgress:
                                break;
                            case ToolStatus.End:
                                dpDrawEnd = e.MapCoordinate;
                                AddFeature frmAddFeature = new AddFeature(StyleType.Shape);
                                if (frmAddFeature.ShowDialog() == DialogResult.OK)
                                {
                                    MapInfo.Geometry.Rectangle rectangle = new MapInfo.Geometry.Rectangle(mapCS, new DRect(dpDrawStart, dpDrawEnd));
                                    Feature f = new Feature(tDarwTalbe.TableInfo.Columns);
                                    f.Geometry = rectangle;
                                    f.Style = frmAddFeature.FeatureStyle;
                                    f["ID"] = Guid.NewGuid().ToString();
                                    f["NAME"] = frmAddFeature.Name;
                                    tDarwTalbe.InsertFeature(f);
                                }
                                break;
                        }
                        #endregion
                        break;
                    case "AddPolygon":
                        #region 绘制多边形
                        switch (e.ToolStatus)
                        {
                            case ToolStatus.Start:
                                listDP.Clear();
                                listDP.Add(e.MapCoordinate);
                                break;
                            case ToolStatus.InProgress:
                                listDP.Add(e.MapCoordinate);
                                break;
                            case ToolStatus.End:
                                AddFeature frmAddFeature = new AddFeature(StyleType.Shape);
                                if (frmAddFeature.ShowDialog() == DialogResult.OK)
                                {
                                    DPoint[] dp = new DPoint[listDP.Count];
                                    for (int i = 0; i < listDP.Count; i++)
                                    {
                                        dp[i] = listDP[i];
                                    }
                                    MultiPolygon multiPolygon = new MultiPolygon(mapCS, CurveSegmentType.Linear, dp);
                                    Feature f = new Feature(tDarwTalbe.TableInfo.Columns);
                                    f.Geometry = multiPolygon;
                                    f.Style = frmAddFeature.FeatureStyle;
                                    f["ID"] = Guid.NewGuid().ToString();
                                    f["NAME"] = frmAddFeature.Name;
                                    tDarwTalbe.InsertFeature(f);
                                }
                                break;
                        }
                        #endregion
                        break;
                    //可以添加其他的用户自定义Tool
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {
                PublicDim.ShowErrorMessage("测量错误:" + Ex.Message.ToString());
            }
        }
        #endregion

        #region 图元工具选择事件
        void Tools_FeatureSelected(object sender, FeatureSelectedEventArgs e)
        {
            if (isInfoTool)
            {
                isInfoTool = false;
                //处理信息工具相关
                if (Session.Current.Selections.DefaultSelection.Count > 0)
                {
                    ShowFeatureInfo((Session.Current.Selections.DefaultSelection[0] as IResultSetFeatureCollection)[0]);
                    Session.Current.Selections.DefaultSelection.Clear();
                    this.mainMapControl.Tools.LeftButtonTool = MapTool.InfoTool;
                }
            }

        }
        #endregion
        #endregion

        #region 私有方法
        Table tDarwTalbe;
        //构建绘图图层
        private void LoadDrawLayer()
        {
            ////创建临时图层用于显示连线
            TableInfoMemTable ti = new TableInfoMemTable("pDrawLayer");
            ti.Temporary = true;

            //添加字段
            Column column;
            column = new GeometryColumn(mapCS);
            column.Alias = "MI_Geometry ";
            column.DataType = MIDbType.FeatureGeometry;
            ti.Columns.Add(column);

            column = new Column();
            column.Alias = "MI_Style ";
            column.DataType = MIDbType.Style;
            ti.Columns.Add(column);

            column = new Column();
            column.Alias = "ID";
            column.DataType = MIDbType.String;
            ti.Columns.Add(column);

            column = new Column();
            column.Alias = "Name";
            column.DataType = MIDbType.String;
            ti.Columns.Add(column);
            tDarwTalbe = Session.Current.Catalog.CreateTable(ti);

            FeatureLayer flDrawLayer = new FeatureLayer(tDarwTalbe);
            this.mainMapControl.Map.Layers.Add(flDrawLayer);

            LayerHelper.SetInsertable(flDrawLayer, true);
        }

        //处理工具栏事件
        private void DoButtonMethod(String ToolButtonName)
        {
            switch (ToolButtonName)
            {
                //退出
                case "sysTooBtnExit":
                    if (PublicDim.ShowQuestion("确认退出系统？") == DialogResult.OK)
                    {
                        Application.ExitThread();
                    }
                    break;
                //查询
                case "mapToolBtnSearch":
                    //加载查找对话框获取查找条件
                    SearcgWorksForm frmSearcgWorksForm = new SearcgWorksForm();
                    if (frmSearcgWorksForm.ShowDialog() == DialogResult.OK)
                    {
                        //执行查找
                        Search(frmSearcgWorksForm.SearchWords);
                    }
                    break;
                case "mapToolBtnLayer":
                    MapInfo.Windows.Dialogs.LayerControlDlg frmLayerControlDlg = new MapInfo.Windows.Dialogs.LayerControlDlg();
                    frmLayerControlDlg.Map = mainMapControl.Map;
                    frmLayerControlDlg.ShowDialog();
                    break;
                case "mapToolBtnSelect":
                    this.mainMapControl.Tools.LeftButtonTool = MapTool.Select;
                    break;
                case "mapToolBtnZoomIn":
                    this.mainMapControl.Tools.LeftButtonTool = MapTool.ZoomIn;
                    break;
                case "mapToolBtnZoomOut":
                    this.mainMapControl.Tools.LeftButtonTool = MapTool.ZoomOut;
                    break;
                case "mapToolBtnPan":
                    this.mainMapControl.Tools.LeftButtonTool = MapTool.Pan;
                    break;
                case "mapToolBtnCenter":
                    this.mainMapControl.Tools.LeftButtonTool = MapTool.Center;
                    break;
                case "mapToolBtnDistance":
                    this.mainMapControl.Tools.LeftButtonTool = MapTool.DistanceTool;
                    break;
                case "mapToolBtnArea":
                    this.mainMapControl.Tools.LeftButtonTool = MapTool.AreaTool;
                    break;
                case "mapToolBtnIdentify":
                    Session.Current.Selections.DefaultSelection.Clear();
                    this.mainMapControl.Tools.LeftButtonTool = MapTool.InfoTool;
                    break;
                case "mapToolBtnImage":
                    this.SaveImage();
                    break;
                case "mapToolBtnPrint":
                    this.PrintMap();
                    break;
                case "MapToolBtnReset":
                    this.ResetMap();
                    break;
                case "MapToolBtnData":
                    this.ShowDataGrid();
                    break;
            }

        }

        //显示图层的属性表
        private void ShowDataGrid()
        {
            List<String> listLayers = new List<string>();
            foreach(IMapLayer layer in mainMapControl.Map.Layers)
            {
                if(layer is FeatureLayer)
                {
                    listLayers.Add((layer as FeatureLayer).Table.Alias);
                }
            }
            DataInfo frmDataInfo = new DataInfo(listLayers);
            frmDataInfo.ShowDialog();
        }

        //查找
        private void Search(string SearchWords)
        {
            //按照关键字查找图元
            Session.Current.Selections.DefaultSelection.Clear();
            bool bFind = false;

            //遍历图层
            foreach (IMapLayer layer in this.mainMapControl.Map.Layers)
            {
                if (layer is FeatureLayer)
                {
                    Table tLayer = (layer as FeatureLayer).Table;
                    if (tLayer != null)
                    {
                        //生成查询条件 表中所有字段匹配即可
                        string Where = "0=1";
                        foreach (Column c in (layer as FeatureLayer).Table.TableInfo.Columns)
                        {
                            if ("Obj".Equals(c.Alias) || "MI_Style".Equals(c.Alias))
                                continue;
                            if (c.DataType == MIDbType.String)
                            {
                                Where += " or " + c.Alias + " like '%" + SearchWords + "%'";
                            }
                            else
                            {
                                //Where += " or " + c.Alias + " = " + SearchWords;
                            }
                        }
                        //查找
                        SearchInfo si = MapInfo.Data.SearchInfoFactory.SearchWhere(Where);
                        IResultSetFeatureCollection ifs = MapInfo.Engine.Session.Current.Catalog.Search(tLayer, si);
                        if (ifs.Count > 0)
                        {
                            //查询结果选中并定位到地图中兴
                            Session.Current.Selections.DefaultSelection.Add(ifs);
                            this.mainMapControl.Map.Center = ifs[0].Geometry.Centroid;
                            bFind = true;
                        }
                    }
                }
            }
            if (!bFind)
            {
                PublicDim.ShowInfoMessage("未找到查找的内容！");
            }
        }

        //加载地图
        private void LoadMap()
        {
            string MapPath = Path.Combine(Application.StartupPath, @"Map\Map.mws");
            MapWorkSpaceLoader mwsLoader = new MapWorkSpaceLoader(MapPath);
            mainMapControl.Map.Load(mwsLoader);
        }

        private void ShowFeatureInfo(Feature SelectedFeature)
        {
            InfoBox frmInfoBox = new InfoBox(SelectedFeature);
            frmInfoBox.ShowDialog();
        }

        //加载地图比例尺
        ScaleBarAdornmentControl sbac = null;
        private void LoadScaleBar()
        {
            return;
            //创建一个scalebar
            ScaleBarAdornment sba = new ScaleBarAdornment(mainMapControl.Map);
            int x = mainMapControl.Map.Size.Width - sba.Size.Width - 5;
            int y = mainMapControl.Map.Size.Height - sba.Size.Height - 5;
            sba.Location = new System.Drawing.Point(x, y);

            //加载到地图
            sbac = new ScaleBarAdornmentControl(sba, mainMapControl.Map);
            mainMapControl.AddAdornment(sba, sbac);
        }

        //导出地图到图片
        private void SaveImage()
        {
            MapInfo.Mapping.Map mapnew = mainMapControl.Map.Clone() as MapInfo.Mapping.Map;
            MapInfo.Mapping.MapExport ME = new MapInfo.Mapping.MapExport(mapnew);
            
            ME.PixelFormat = MapInfo.Mapping.ExportPixelFormat.Format24bpp;
            ME.ExportSize = new MapInfo.Mapping.ExportSize(mainMapControl.Width, mainMapControl.Height);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = ".wmf|*.wmf|.jpg|*.jpg|.bmp|*.bmp|.gif|*.gif|.png|*.png";
            if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
            {
                switch (saveFileDialog.FilterIndex)
                {
                    case 0:
                        ME.Format = MapInfo.Mapping.ExportFormat.Wmf;
                        break;
                    case 1:
                        ME.Format = MapInfo.Mapping.ExportFormat.Jpeg;
                        break;
                    case 2:
                        ME.Format = MapInfo.Mapping.ExportFormat.Bmp;
                        break;
                    case 3:
                        ME.Format = MapInfo.Mapping.ExportFormat.Gif;
                        break;
                    case 4:
                        ME.Format = MapInfo.Mapping.ExportFormat.Png;
                        break;
                }
                ME.Export(saveFileDialog.FileName);
                PublicDim.ShowInfoMessage("保存成功！");
            }

        }

        //打印地图
        private void PrintMap()
        {
            PageSetupDialog frmSetDialog = new PageSetupDialog();
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += new PrintPageEventHandler(mapPrintD_PrintPage);
            frmSetDialog.Document = printDoc;
            if (frmSetDialog.ShowDialog() == DialogResult.OK)
            {

                PrintPreviewDialog frmPrintPreviewDialog = new PrintPreviewDialog();
                frmPrintPreviewDialog.Document = printDoc;
                printDoc.OriginAtMargins = true;
                (frmPrintPreviewDialog as Form).WindowState = FormWindowState.Maximized;
                frmPrintPreviewDialog.ShowDialog();
            }

            #region 打印地图调用Mapinfo
            //try
            //{
            //    mainMapControl.Map.Name = "电力GIS地图";
            //    MapInfo.Mapping.Map mapnew = mainMapControl.Map.Clone() as MapInfo.Mapping.Map;
            //    MapInfo.Printing.MapPrintDocument mapPrintD = new MapPrintDocument(mapnew);
            //    mapPrintD.MapPrintSize = MapPrintSize.FillPage;//已打印地图的大小信息。
            //    mapPrintD.Map.Name = "地图";
            //    //mapPrintD.Border = PrintBorder.On;
            //    //mapPrintD.DefaultPageSettings.Landscape = true; 启用横向打印
            //    PrintPreviewDialog pd = new PrintPreviewDialog();
            //    mapPrintD.DocumentName = "地图名称";
            //    //mapPrintD.DefaultPageSettings.Margins = new System.Drawing.Printing.Margins(5, 5, 5, 5);
            //    pd.Document = mapPrintD;
            //    mapPrintD.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(mapPrintD_PrintPage);
            //    pd.ShowDialog();
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show("未安装打印机！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            #endregion

        }

        void mapPrintD_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //获取打印参数
            System.Drawing.Rectangle margins = e.PageSettings.Bounds;
            int Height = e.MarginBounds.Height-e.PageSettings.Margins.Bottom; 
            int Width = e.MarginBounds.Width-e.PageSettings.Margins.Right;

            MapInfo.Mapping.Map mapnew = mainMapControl.Map.Clone() as MapInfo.Mapping.Map;
            MapInfo.Mapping.MapExport ME = new MapInfo.Mapping.MapExport(mapnew);

            double mapScale = mapnew.Scale;


            ME.PixelFormat = MapInfo.Mapping.ExportPixelFormat.Format24bpp;
            ME.ExportSize = new MapInfo.Mapping.ExportSize(Width , Height);//(margins.Right - margins.Left-margins.Right, margins.Bottom - margins.Top-margins.Bottom);
            Image image = ME.Export();
            e.Graphics.DrawImageUnscaled(image, new System.Drawing.Rectangle(margins.Left,margins.Top,Width-e.PageSettings.Margins.Right,Height-e.PageSettings.Margins.Bottom));//(image,new System.Drawing.Point(margins.Left,margins.Top),);
            e.Graphics.DrawString("国家电网GIS系统",new System.Drawing.Font("宋体",50),new SolidBrush(Color.Black), 50 + margins.Left, margins.Top);

            //绘制指北针
            e.Graphics.DrawImage(Devgis.MapApp.Properties.Resources.north, 50 + margins.Left, 80 + margins.Top);
            e.Graphics.DrawImage(Devgis.MapApp.Properties.Resources.Lengend, 30 + margins.Left, e.MarginBounds.Height - Devgis.MapApp.Properties.Resources.Lengend.Height - 20);

            //绘制地图比例尺
            e.Graphics.DrawString(string.Format("地图比例尺1:{0}米", mapScale), new System.Drawing.Font("宋体", 10), new SolidBrush(Color.Black), Width-200,Height-30);



        }

        //恢复地图到默认
        private void ResetMap()
        {
            //获取地图参数
            mainMapControl.Map.Center = dpCenter;
            mainMapControl.Map.Scale = mapScale;
        }
        #endregion


        #region 初始化Table 加载数据
        //全局table用于调用
        Table tableGANTA = null;
        Table tableXIANLU = null;
        Table tableBanYaQi = null;
        Table tableDuanLuQi = null;
        Table tableGeLiKaiGuan = null;

        /// <summary>
        /// 全局调用
        /// </summary>
        private void InitTables()
        {
            //InitLengend();

            //初始化临时图层
            InitBanYaQi();
            InitDuanLuQi();
            InitGeLiKaiGuan();
            InitGANTA();
            InitXIANLU();

            //读取数据库绘图
            LoadGANTAData();
            LoadXIANLUData();
            LoadBanYaQiData();
            LoadDuanLuQiData();
            LoadGeLiKaiGuanData();
        }

        Legend legend = null;
        private void InitLengend()
        {
            legend = mainMapControl.Map.Legends.CreateLegend(new Size(90, 160));
            legend.Border = true;

           
            mainMapControl.Map.Adornments.Append(legend);
            System.Drawing.Point pt = new System.Drawing.Point(0, 0);
            pt.X = mainMapControl.Size.Width - legend.Size.Width-80;
            pt.Y = 5;// mainMapControl.Size.Height - legend.Size.Height;
            legend.Location = pt;

            CustomLegendFrameRow[] rows = new CustomLegendFrameRow[6];
            rows[0] = new CustomLegendFrameRow(new CompositeStyle(PublicDim.StyleMainLine), "主干线路");
            rows[1] = new CustomLegendFrameRow(new CompositeStyle(PublicDim.StyleSubLine), "分支线路");
            rows[2] = new CustomLegendFrameRow(new CompositeStyle(PublicDim.StyleBanYaQi), "变压器");
            rows[3] = new CustomLegendFrameRow(new CompositeStyle(PublicDim.StyleDuanLuQi), "断路器");
            rows[4] = new CustomLegendFrameRow(new CompositeStyle(PublicDim.StyleGeLiKaiGuan), "隔离开关");
            rows[5] = new CustomLegendFrameRow(new CompositeStyle(PublicDim.StyleGANTA), "杆塔");

            LegendFrame frame = LegendFrameFactory.CreateCustomLegendFrame("图例", "图例", rows);
            legend.Frames.Append(frame);
            
        }
        
        /// <summary>
        /// 初始化杆塔图层
        /// </summary>
        private void InitGANTA()
        {
            tableGANTA = MapInfo.Engine.Session.Current.Catalog.GetTable("tableGANTA");//确保当前目录下不存在同名表
            if (tableGANTA == null)
            {
                MapInfo.Data.TableInfoMemTable tblInfo = new MapInfo.Data.TableInfoMemTable("tableGANTA");
                tblInfo.Columns.Add(MapInfo.Data.ColumnFactory.CreateFeatureGeometryColumn(mainMapControl.Map.GetDisplayCoordSys()));//向表信息中添加必备的可绘图列
                tblInfo.Columns.Add(MapInfo.Data.ColumnFactory.CreateStyleColumn());
                tblInfo.Columns.Add(MapInfo.Data.ColumnFactory.CreateStringColumn("name", 255));  //创建字符串型的列,用于标注

                tableGANTA = MapInfo.Engine.Session.Current.Catalog.CreateTable(tblInfo);//根据表信息创建临时表
                FeatureLayer layer = new FeatureLayer(tableGANTA, "layerGANTA", "layerGANTA");//创建图层(并关联表)

                //CartographicLegendFrame frame = LegendFrameFactory.CreateCartographicLegendFrame("杆塔", "杆塔", layer);
                //legend.Frames.Append(frame);
                //frame.Title = "杆塔";


                LabelSource source = new LabelSource(tableGANTA);//绑定Table
                source.DefaultLabelProperties.Caption = "name";//指定哪个字段作为显示标注
                source.DefaultLabelProperties.Style.Font.ForeColor = Color.Red;
                source.DefaultLabelProperties.CalloutLine.Use = false;  //是否使用标注线  
                source.DefaultLabelProperties.Layout.Offset = 5;//标注偏移   
                LabelLayer labelLayer = new LabelLayer();
                labelLayer.Sources.Append(source);//加载指定数据
                mainMapControl.Map.Layers.Add(labelLayer);
                mainMapControl.Map.Layers.Add(layer);
                mainMapControl.Tools["Select"].UseDefaultInfoTipLayerExpressions = false;
                MapInfo.Tools.MapTool.SetInfoTipExpression(mainMapControl.Tools["Select"], layer, "name");
            }
        }

        /// <summary>
        /// 初始化线路图层
        /// </summary>
        private void InitXIANLU()
        {
            tableXIANLU = MapInfo.Engine.Session.Current.Catalog.GetTable("tableXianLu");//确保当前目录下不存在同名表
            if (tableXIANLU == null)
            {
                MapInfo.Data.TableInfoMemTable tblInfo = new MapInfo.Data.TableInfoMemTable("tableXianLu");
                tblInfo.Columns.Add(MapInfo.Data.ColumnFactory.CreateFeatureGeometryColumn(mainMapControl.Map.GetDisplayCoordSys()));//向表信息中添加必备的可绘图列
                tblInfo.Columns.Add(MapInfo.Data.ColumnFactory.CreateStyleColumn());
                tblInfo.Columns.Add(MapInfo.Data.ColumnFactory.CreateStringColumn("name", 255));  //创建字符串型的列,用于标注

                tableXIANLU = MapInfo.Engine.Session.Current.Catalog.CreateTable(tblInfo);//根据表信息创建临时表
                FeatureLayer layer = new FeatureLayer(tableXIANLU, "layerXianLu", "layerXianLu");//创建图层(并关联表)
                LabelSource source = new LabelSource(tableXIANLU);//绑定Table
                source.DefaultLabelProperties.Caption = "name";//指定哪个字段作为显示标注
                source.DefaultLabelProperties.Style.Font.ForeColor = Color.Red;
                source.DefaultLabelProperties.CalloutLine.Use = false;  //是否使用标注线  
                source.DefaultLabelProperties.Layout.Offset = 5;//标注偏移   
                LabelLayer labelLayer = new LabelLayer();
                labelLayer.Sources.Append(source);//加载指定数据
                mainMapControl.Map.Layers.Add(labelLayer);
                mainMapControl.Map.Layers.Add(layer);
                mainMapControl.Tools["Select"].UseDefaultInfoTipLayerExpressions = false;
                MapInfo.Tools.MapTool.SetInfoTipExpression(mainMapControl.Tools["Select"], layer, "name");
            }
        }

        /// <summary>
        /// 初始化变压器图层
        /// </summary>
        private void InitBanYaQi()
        {
            tableBanYaQi = MapInfo.Engine.Session.Current.Catalog.GetTable("tableBanYaQi");//确保当前目录下不存在同名表
            if (tableBanYaQi == null)
            {
                MapInfo.Data.TableInfoMemTable tblInfo = new MapInfo.Data.TableInfoMemTable("tableBanYaQi");
                tblInfo.Columns.Add(MapInfo.Data.ColumnFactory.CreateFeatureGeometryColumn(mainMapControl.Map.GetDisplayCoordSys()));//向表信息中添加必备的可绘图列
                tblInfo.Columns.Add(MapInfo.Data.ColumnFactory.CreateStyleColumn());
                tblInfo.Columns.Add(MapInfo.Data.ColumnFactory.CreateStringColumn("name", 255));  //创建字符串型的列,用于标注

                tableBanYaQi = MapInfo.Engine.Session.Current.Catalog.CreateTable(tblInfo);//根据表信息创建临时表
                FeatureLayer layer = new FeatureLayer(tableBanYaQi, "layerBanYaQi", "layerBanYaQi");//创建图层(并关联表)
                LabelSource source = new LabelSource(tableBanYaQi);//绑定Table
                source.DefaultLabelProperties.Caption = "name";//指定哪个字段作为显示标注
                source.DefaultLabelProperties.Style.Font.ForeColor = Color.Red;
                source.DefaultLabelProperties.CalloutLine.Use = false;  //是否使用标注线  
                source.DefaultLabelProperties.Layout.Offset = 5;//标注偏移   
                LabelLayer labelLayer = new LabelLayer();
                labelLayer.Sources.Append(source);//加载指定数据
                mainMapControl.Map.Layers.Add(labelLayer);
                mainMapControl.Map.Layers.Add(layer);
                mainMapControl.Tools["Select"].UseDefaultInfoTipLayerExpressions = false;
                MapInfo.Tools.MapTool.SetInfoTipExpression(mainMapControl.Tools["Select"], layer, "name");
            }
        }
        /// <summary>
        /// 初始化断路器图层
        /// </summary>
        private void InitDuanLuQi()
        {
            tableDuanLuQi = MapInfo.Engine.Session.Current.Catalog.GetTable("tableDuanLuQi");//确保当前目录下不存在同名表
            if (tableDuanLuQi == null)
            {
                MapInfo.Data.TableInfoMemTable tblInfo = new MapInfo.Data.TableInfoMemTable("tableDuanLuQi");
                tblInfo.Columns.Add(MapInfo.Data.ColumnFactory.CreateFeatureGeometryColumn(mainMapControl.Map.GetDisplayCoordSys()));//向表信息中添加必备的可绘图列
                tblInfo.Columns.Add(MapInfo.Data.ColumnFactory.CreateStyleColumn());
                tblInfo.Columns.Add(MapInfo.Data.ColumnFactory.CreateStringColumn("name", 255));  //创建字符串型的列,用于标注

                tableDuanLuQi = MapInfo.Engine.Session.Current.Catalog.CreateTable(tblInfo);//根据表信息创建临时表
                FeatureLayer layer = new FeatureLayer(tableDuanLuQi, "layerDuanLuQi", "layerDuanLuQi");//创建图层(并关联表)
                LabelSource source = new LabelSource(tableDuanLuQi);//绑定Table
                source.DefaultLabelProperties.Caption = "name";//指定哪个字段作为显示标注
                source.DefaultLabelProperties.Style.Font.ForeColor = Color.Red;
                source.DefaultLabelProperties.CalloutLine.Use = false;  //是否使用标注线  
                source.DefaultLabelProperties.Layout.Offset = 5;//标注偏移   
                LabelLayer labelLayer = new LabelLayer();
                labelLayer.Sources.Append(source);//加载指定数据
                mainMapControl.Map.Layers.Add(labelLayer);
                mainMapControl.Map.Layers.Add(layer);
                mainMapControl.Tools["Select"].UseDefaultInfoTipLayerExpressions = false;
                MapInfo.Tools.MapTool.SetInfoTipExpression(mainMapControl.Tools["Select"], layer, "name");
            }
        }

        /// <summary>
        /// 初始化断路器图层
        /// </summary>
        private void InitGeLiKaiGuan()
        {
            tableGeLiKaiGuan = MapInfo.Engine.Session.Current.Catalog.GetTable("tableGeLiKaiGuan");//确保当前目录下不存在同名表
            if (tableGeLiKaiGuan == null)
            {
                MapInfo.Data.TableInfoMemTable tblInfo = new MapInfo.Data.TableInfoMemTable("tableGeLiKaiGuan");
                tblInfo.Columns.Add(MapInfo.Data.ColumnFactory.CreateFeatureGeometryColumn(mainMapControl.Map.GetDisplayCoordSys()));//向表信息中添加必备的可绘图列
                tblInfo.Columns.Add(MapInfo.Data.ColumnFactory.CreateStyleColumn());
                tblInfo.Columns.Add(MapInfo.Data.ColumnFactory.CreateStringColumn("name", 255));  //创建字符串型的列,用于标注

                tableGeLiKaiGuan = MapInfo.Engine.Session.Current.Catalog.CreateTable(tblInfo);//根据表信息创建临时表
                FeatureLayer layer = new FeatureLayer(tableGeLiKaiGuan, "layerGeLiKaiGuan", "layerGeLiKaiGuan");//创建图层(并关联表)
                LabelSource source = new LabelSource(tableGeLiKaiGuan);//绑定Table
                source.DefaultLabelProperties.Caption = "name";//指定哪个字段作为显示标注
                source.DefaultLabelProperties.Style.Font.ForeColor = Color.Red;
                source.DefaultLabelProperties.CalloutLine.Use = false;  //是否使用标注线  
                source.DefaultLabelProperties.Layout.Offset = 5;//标注偏移   
                LabelLayer labelLayer = new LabelLayer();
                labelLayer.Sources.Append(source);//加载指定数据
                mainMapControl.Map.Layers.Add(labelLayer);
                mainMapControl.Map.Layers.Add(layer);
                mainMapControl.Tools["Select"].UseDefaultInfoTipLayerExpressions = false;
                MapInfo.Tools.MapTool.SetInfoTipExpression(mainMapControl.Tools["Select"], layer, "name");
            }
        }

        /// <summary>
        /// 加载杆塔数据绘图
        /// </summary>
        private void LoadGANTAData()
        {
            //清楚图层数据
            (tableGANTA as ITableFeatureCollection).Clear(); 

            //重新加载数据
            string sql = "SELECT y.杆塔编号 as name,经度,纬度 FROM [配电杆塔-物理杆] w left join 运行杆塔 y on w.杆塔名称=y.设备名称"; //"SELECT 杆塔名称 as name,经度,纬度 FROM [配电杆塔-物理杆]";
            DataTable dtGanTa = SQLHelper.Instance.GetDataTable(sql);
            foreach (DataRow dr in dtGanTa.Rows)
            {
                Feature f = new Feature(tableGANTA.TableInfo.Columns);
                try
                {
                    MapInfo.Geometry.Point point = new MapInfo.Geometry.Point(mainMapControl.Map.GetDisplayCoordSys(), new DPoint(Convert.ToDouble(dr["经度"]), Convert.ToDouble(dr["纬度"])));
                    f.Geometry = point;
                    f.Style = PublicDim.StyleGANTA;
                    f["name"] = dr["name"];
                    tableGANTA.InsertFeature(f);
                }
                catch
                { }
            }
        }

        /// <summary>
        /// 加载线路数据绘图
        /// </summary>
        private void LoadXIANLUData()
        {
            //清楚图层数据
            (tableXIANLU as ITableFeatureCollection).Clear();

            //重新加载数据
            //"SELECT [设备名称]+CONVERT(VARCHAR,[长度])+[型号] as name,(SELECT 经度 FROM [配电杆塔-物理杆] where 杆塔名称=[起点杆号]) as 经度1,(SELECT 纬度 FROM [配电杆塔-物理杆] where 杆塔名称=[起点杆号]) as 纬度1,(SELECT 经度 FROM [配电杆塔-物理杆] where 杆塔名称=[终点杆号]) as 经度2,(SELECT 纬度 FROM [配电杆塔-物理杆] where 杆塔名称=[终点杆号]) as 纬度2,[所属线路] FROM [导线] order by [所属线路]";
            string sql1 = "SELECT 杆塔名称,经度,纬度 FROM [配电杆塔-物理杆] order by 杆塔名称";//查找所有杆塔
            string sql2 = "select 设备名称,型号 as name,所属线路,起点杆号,终点杆号 from 导线";//查找所有线路信息
            //bool bNewLine=false;
            DataTable dtGanta = SQLHelper.Instance.GetDataTable(sql1);
            DataTable dtLine = SQLHelper.Instance.GetDataTable(sql2);

            foreach (DataRow dr in dtLine.Rows)
            {
                //逐条画线
                GetLine(dr, dtGanta);
            }

            #region 已经不用
            //List<DPoint> listDPoint=new List<DPoint>();
            //for (int i=0;i<dttableXIANLU.Rows.Count-1;i++)
            //{
            //    DataRow dr=dttableXIANLU.Rows[i];
            //    if(i==dttableXIANLU.Rows.Count-2)
            //    {
            //        try
            //        {
            //            listDPoint.Add(new DPoint(Convert.ToDouble(dr["经度1"]), Convert.ToDouble(dr["纬度1"])));
            //        }
            //        catch
            //        { }
            //        try
            //        {
            //            listDPoint.Add(new DPoint(Convert.ToDouble(dr["经度2"]), Convert.ToDouble(dr["纬度2"])));
            //        }
            //        catch
            //        { }
            //        //listDPoint.Add( new DPoint(Convert.ToDouble(dr["经度1"]), Convert.ToDouble(dr["纬度1"])));
            //        //listDPoint.Add( new DPoint(Convert.ToDouble(dr["经度2"]), Convert.ToDouble(dr["纬度2"])));
            //        try
            //        {
            //            DrawLine(listDPoint,dr["name"],dr["所属线路"]);
            //        }
            //        catch
            //        {}
            //        finally
            //        {
            //            listDPoint.Clear();
            //            listDPoint=null;
            //        }
            //    }
            //    else
            //    {
            //        try
            //        {
            //            listDPoint.Add(new DPoint(Convert.ToDouble(dr["经度1"]), Convert.ToDouble(dr["纬度1"])));
            //        }
            //        catch
            //        { }
            //        if (dr["所属线路"] != null && dttableXIANLU.Rows[i + 1]["所属线路"] != null && dr["所属线路"].ToString() != dttableXIANLU.Rows[i + 1]["所属线路"].ToString())
            //        {
            //            try
            //            {
            //                DrawLine(listDPoint, dr["name"], dr["所属线路"]);
            //            }
            //            catch
            //            { }
            //            finally
            //            {
            //                listDPoint.Clear();
            //            }
            //        }
            //    }

            //    //Feature f = new Feature(tableXIANLU.TableInfo.Columns);
            //    //try
            //    //{
            //    //    MapInfo.get
            //    //    MapInfo.Geometry.Point point = new MapInfo.Geometry.Point(mainMapControl.Map.GetDisplayCoordSys(), new DPoint(Convert.ToDouble(dr["经度"]), Convert.ToDouble(dr["纬度"])));
            //    //    f.Geometry = point;
            //    //    f.Style = PublicDim.StyleBanYaQi;
            //    //    f["name"] = dr["name"];
            //    //    tableXIANLU.InsertFeature(f);
            //    //}
            //    //catch
            //    //{ }
            //}
            #endregion
        }

        private void GetLine(DataRow drLine, DataTable dtGanta)
        {
            try
            {
                string LineInfo = drLine["设备名称"].ToString();
                string SegName = drLine["name"].ToString();
                string LineName = drLine["所属线路"].ToString();
                LineInfo = LineInfo.Replace("#导线段","#");
                string[] stemp = LineInfo.Split('-');
                if (stemp == null || stemp.Length < 2)
                    return;

                string startGanta = stemp[0];
                string endGanta = stemp[1];

                //if (startGanta.Length != endGanta.Length)
                //{
                //    MessageBox.Show(startGanta + "\r\n" + endGanta);
                //}

                List<DPoint> listDPoint = new List<DPoint>();

                bool bFindStart=false;
                bool bFindEnd = false;
                bool isStart=true;
                foreach (DataRow dr in dtGanta.Rows)
                {
                    if (bFindStart)
                    {
                        DPoint dp = new DPoint(Convert.ToDouble(dr["经度"]), Convert.ToDouble(dr["纬度"]));
                        string nowName=dr["杆塔名称"].ToString();
                        if (nowName.Length == endGanta.Length || nowName.Length == startGanta.Length)
                        {
                            listDPoint.Add(dp);
                            if (endGanta.Equals(dr["杆塔名称"].ToString()) || startGanta.Equals(dr["杆塔名称"].ToString()))
                            {
                                bFindEnd = true;
                                break;
                            }
                            else
                            { }
                        }
                        
                    }
                    else
                    {
                        if (endGanta.Equals(dr["杆塔名称"].ToString()) || startGanta.Equals(dr["杆塔名称"].ToString()))
                        {
                            DPoint dp = new DPoint(Convert.ToDouble(dr["经度"]), Convert.ToDouble(dr["纬度"]));
                            listDPoint.Add(dp);
                            bFindStart = true;
                        }
                    }
                }

                if (bFindEnd && bFindStart)
                {
                    DrawLine(listDPoint, SegName, LineName);
                }

            }
            catch
            { }
        }

        private void DrawLine(List<DPoint> listDPoint,object SegName,object LineName)
        {
            if(listDPoint==null||listDPoint.Count<2)
                return;

            CoordSys cs = mainMapControl.Map.GetDisplayCoordSys();

            DPoint[] arrDPoint = new DPoint[listDPoint.Count];
            for(int i=0;i<listDPoint.Count;i++)
            {
                
                arrDPoint[i]=listDPoint[i];
                //arrDPoint[1] = listDPoint[i+1];
            }

            MapInfo.Geometry.Curve c = new Curve(cs, CurveSegmentType.Linear, arrDPoint);
            MapInfo.Geometry.MultiCurve mc = new MultiCurve(cs, c);
            Feature f = new Feature(tableXIANLU.TableInfo.Columns);
            f.Geometry = mc;
            string sLineName = string.Empty;
            if (LineName != null)
            {
                sLineName = LineName.ToString();
            }
            if (sLineName.Contains("支"))
            {
                f.Style = PublicDim.StyleSubLine;
            }
            else
            {
                f.Style = PublicDim.StyleMainLine;
            }
            f["name"] = SegName;
            tableXIANLU.InsertFeature(f);
        }

        /// <summary>
        /// 加载变压器数据绘图
        /// </summary>
        private void LoadBanYaQiData()
        {
            //清楚图层数据
            (tableBanYaQi as ITableFeatureCollection).Clear();

            //重新加载数据
            string sql = "SELECT B.[设备名称]+B.[型号]+B.[空载容量] as name ,G.经度,G.纬度 FROM [配电-柱上变压器] B LEFT JOIN [配电杆塔-物理杆] G on B.[所属杆塔]=G.杆塔名称";
            DataTable dtBanYaQiData = SQLHelper.Instance.GetDataTable(sql);
            foreach (DataRow dr in dtBanYaQiData.Rows)
            {
                Feature f = new Feature(tableBanYaQi.TableInfo.Columns);
                try
                {
                    MapInfo.Geometry.Point point = new MapInfo.Geometry.Point(mainMapControl.Map.GetDisplayCoordSys(), new DPoint(Convert.ToDouble(dr["经度"]), Convert.ToDouble(dr["纬度"])));
                    f.Geometry = point;
                    f.Style = PublicDim.StyleBanYaQi;
                    f["name"] = dr["name"];
                    tableBanYaQi.InsertFeature(f);
                }
                catch
                { }
            }
        }

        /// <summary>
        /// 加载断路器数据绘图
        /// </summary>
        private void LoadDuanLuQiData()
        {
            //清楚图层数据
            (tableDuanLuQi as ITableFeatureCollection).Clear();

            //重新加载数据
            string sql = "SELECT B.[设备名称] as name ,G.经度,G.纬度 FROM [配电-柱上断路器] B LEFT JOIN [配电杆塔-物理杆] G on B.[所属杆塔]=G.杆塔名称";
            DataTable dtDuanLuQi = SQLHelper.Instance.GetDataTable(sql);
            foreach (DataRow dr in dtDuanLuQi.Rows)
            {
                Feature f = new Feature(tableDuanLuQi.TableInfo.Columns);
                try
                {
                    MapInfo.Geometry.Point point = new MapInfo.Geometry.Point(mainMapControl.Map.GetDisplayCoordSys(), new DPoint(Convert.ToDouble(dr["经度"]), Convert.ToDouble(dr["纬度"])));
                    f.Geometry = point;
                    f.Style = PublicDim.StyleBanYaQi;
                    f["name"] = string.Empty; //dr["name"];
                    tableDuanLuQi.InsertFeature(f);
                }
                catch
                { }
            }
        }

        /// <summary>
        ///加载隔离开关数据绘图
        /// </summary>
        private void LoadGeLiKaiGuanData()
        {
            //清楚图层数据
            (tableGeLiKaiGuan as ITableFeatureCollection).Clear();

            //重新加载数据
            string sql = "SELECT B.[设备名称] as name ,G.经度,G.纬度 FROM [配电-柱上隔离开关] B LEFT JOIN [配电杆塔-物理杆] G on B.[所属杆塔名称]=G.杆塔名称";
            DataTable dtGeLiKaiGuan = SQLHelper.Instance.GetDataTable(sql);
            foreach (DataRow dr in dtGeLiKaiGuan.Rows)
            {
                Feature f = new Feature(tableGeLiKaiGuan.TableInfo.Columns);
                try
                {
                    MapInfo.Geometry.Point point = new MapInfo.Geometry.Point(mainMapControl.Map.GetDisplayCoordSys(), new DPoint(Convert.ToDouble(dr["经度"]), Convert.ToDouble(dr["纬度"])));
                    f.Geometry = point;
                    f.Style = PublicDim.StyleBanYaQi;
                    f["name"] = dr["name"];
                    tableGeLiKaiGuan.InsertFeature(f);
                }
                catch
                { }
            }
        }
        #endregion

        
    }
}