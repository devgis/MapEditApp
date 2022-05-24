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
            this.eagleMap.MouseWheelSupport = new MouseWheelSupport(MouseWheelBehavior.None, 10, 5);

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

            #region 鹰眼相关
            if (Session.Current.Catalog.GetTable("EagleEyeTemp") == null)
                loadEagleLayer();
            Table tblTemp = Session.Current.Catalog.GetTable("EagleEyeTemp");
            try
            {
                (tblTemp as ITableFeatureCollection).Clear();
            }
            catch (Exception)
            { }

            try
            {
                if (eagleMap.Map.Layers["MyEagleEye"] != null)
                    eagleMap.Map.Layers.Remove(eagleEye);
                DRect rect = mainMapControl.Map.Bounds;
                FeatureGeometry fg = new MapInfo.Geometry.Rectangle(mainMapControl.Map.GetDisplayCoordSys(), rect);
                SimpleLineStyle vLine = new SimpleLineStyle(new LineWidth(2, LineWidthUnit.Point), 2, Color.Red);
                SimpleInterior vInter = new SimpleInterior(9, Color.Yellow, Color.Yellow, true);
                CompositeStyle cStyle = new CompositeStyle(new AreaStyle(vLine, vInter), null, null, null);
                fRec = new Feature(fg, cStyle);
                tblTemp.InsertFeature(fRec);
                eagleEye = new FeatureLayer(tblTemp, "EagleEye ", "MyEagleEye");
                eagleMap.Map.Layers.Insert(0, eagleEye);
            }
            catch (Exception)
            { }
            #endregion
        }

        private void eagleMap_MouseClick(object sender, MouseEventArgs e)
        {
            //鹰眼地图点击时切换主地图到该点中兴
            DPoint pt = new DPoint();
            eagleMap.Map.DisplayTransform.FromDisplay(e.Location, out pt);
            mainMapControl.Map.Center = pt;

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
            
        }

        private void MainMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PublicDim.ShowQuestion("确认退出系统？") != DialogResult.OK)
            {
                e.Cancel = true;
                return;
            }
            System.Diagnostics.Process.Start("http://www.devgis.com/");
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

        #region 添加专题图工具栏
        private void tsmiThemeCreate_Click(object sender, EventArgs e)
        {
            try
            {
                CreateThemeWizard createTheme = new CreateThemeWizard(mainMapControl.Map, this);
                _thm = createTheme.CreateTheme(null);
                MapLayer themedLayer = createTheme.SelectedLayer;
                if (themedLayer is FeatureLayer)
                {
                    _themedFeatureLayer = (FeatureLayer)themedLayer;
                }
                else
                {
                    _themedLabelLayer = (LabelLayer)themedLayer;
                }
                if (createTheme.WizardResult == WizardStepResult.Done)
                {
                    tsmiThemeCreate.Enabled = false;
                    tsmiThemeDelete.Enabled = true;
                    tsmiThemeEdit.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("创建专题图失败: " + ex.Message);
            }
        }
        #endregion

        #region 删除专题图工具栏
        private void tsmiThemeDelete_Click(object sender, EventArgs e)
        {
            // Remove the theme

            RemoveTheme();

            // Update the controls
            if (_thm == null)
            {
                tsmiThemeCreate.Enabled = true;
                tsmiThemeDelete.Enabled = false;
                tsmiThemeEdit.Enabled = false;
            }

            // if this was the last map layer, reset the close menus
            if (mainMapControl.Map.Layers.Count == 0)
            {
                tsmiThemeCreate.Enabled = false;
                tsmiThemeDelete.Enabled = false;
                tsmiThemeEdit.Enabled = false;
            }
        }
        #endregion 

        #region 修改专题图工具栏
        private void tsmiThemeEdit_Click(object sender, EventArgs e)
        {
            // Modify the theme
            ModifyTheme();
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
                        System.Diagnostics.Process.Start("http://flysoft.taobao.com/");
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
                case "mapToolBtnEagele":
                    pEagleMap.Visible = !pEagleMap.Visible;
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
                case "editToolBtnPoint":
                    this.mainMapControl.Tools.LeftButtonTool = MapTool.AddPoint;
                    break;
                case "editToolBtnLine":
                    this.mainMapControl.Tools.LeftButtonTool = MapTool.AddPolyline;
                    break;
                case "editToolBtnCircle":
                    this.mainMapControl.Tools.LeftButtonTool = MapTool.AddCircle;
                    break;
                case "editToolBtnOval":
                    this.mainMapControl.Tools.LeftButtonTool = MapTool.AddEllipse;
                    break;
                case "editToolBtnRectangle":
                    this.mainMapControl.Tools.LeftButtonTool = MapTool.AddRectangle;
                    break;
                case "editToolBtnPolygon":
                    this.mainMapControl.Tools.LeftButtonTool = MapTool.AddPolygon;
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
            string MapPath = Path.Combine(Application.StartupPath, @"Data\World.mws");
            MapWorkSpaceLoader mwsLoader = new MapWorkSpaceLoader(MapPath);
            mainMapControl.Map.Load(mwsLoader);
            eagleMap.Map = mainMapControl.Map.Clone() as Map;
        }

        //初始化鹰眼地图
        private void loadEagleLayer()
        {
            TableInfoMemTable ti = new TableInfoMemTable("EagleEyeTemp");
            ti.Temporary = true;
            Column column;
            column = new GeometryColumn(eagleMap.Map.GetDisplayCoordSys());
            column.Alias = "MI_Geometry ";
            column.DataType = MIDbType.FeatureGeometry;
            ti.Columns.Add(column);

            column = new Column();
            column.Alias = "MI_Style ";
            column.DataType = MIDbType.Style;
            ti.Columns.Add(column);
            Table table;
            try
            {
                table = Session.Current.Catalog.CreateTable(ti);

            }
            catch (Exception ex)
            {
                table = Session.Current.Catalog.GetTable("EagleEyeTemp");
            }
            if (eagleMap.Map.Layers["MyEagleEye"] != null)
                eagleMap.Map.Layers.Remove(eagleEye);
            eagleEye = new FeatureLayer(table, "EagleEye ", "MyEagleEye");
            eagleMap.Map.Layers.Insert(0, eagleEye);
            mainMapControl.Refresh();
        }

        private void ShowFeatureInfo(Feature SelectedFeature)
        {
            InfoBox frmInfoBox = new InfoBox(SelectedFeature);
            frmInfoBox.ShowDialog();
        }

        //加载地图比例尺
        private void LoadScaleBar()
        {    
            //创建一个scalebar
            ScaleBarAdornment sba = new ScaleBarAdornment(mainMapControl.Map);
            int x = mainMapControl.Map.Size.Width - sba.Size.Width - 5;
            int y = mainMapControl.Map.Size.Height - sba.Size.Height - 5;
            sba.Location = new System.Drawing.Point(x, y);

            //加载到地图
            ScaleBarAdornmentControl sbac = new ScaleBarAdornmentControl(sba, mainMapControl.Map);
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
            try
            {
                MapInfo.Mapping.Map mapnew = mainMapControl.Map.Clone() as MapInfo.Mapping.Map;
                MapInfo.Printing.MapPrintDocument mapPrintD = new MapPrintDocument(mapnew);
                mapPrintD.MapPrintSize = MapPrintSize.FillPage;//已打印地图的大小信息。
                mapPrintD.Border = PrintBorder.On;
                mapPrintD.DefaultPageSettings.Landscape = true;
                PrintPreviewDialog pd = new PrintPreviewDialog();
                pd.Document = mapPrintD;
                pd.ShowDialog();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("未安装打印机！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        //恢复地图到默认
        private void ResetMap()
        {
            //获取地图参数
            mainMapControl.Map.Center = dpCenter;
            mainMapControl.Map.Scale = mapScale;
        }

        //移除专题图
        private void RemoveTheme()
        {
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            try
            {
                if (_thm is FeatureStyleModifier)
                {
                    if (_themedFeatureLayer != null)
                    {
                        FeatureStyleModifier modifier = _themedFeatureLayer.Modifiers[_thm.Alias];

                        if (modifier != null)
                        {
                            result = System.Windows.Forms.MessageBox.Show(this, "Removing Theme : " + modifier.Name, "ThemeDialogSampleApp", buttons);
                            if (result == DialogResult.Yes)
                            {
                                _themedFeatureLayer.Modifiers.Remove(modifier);
                                mainMapControl.Map.Invalidate();
                                _thm = null;
                            }
                        }
                    }
                }
                else if (_thm is ObjectTheme)
                {
                    ObjectThemeLayer thmLayer = mainMapControl.Map.Layers[_thm.Alias] as ObjectThemeLayer;
                    if (thmLayer != null)
                    {
                        result = System.Windows.Forms.MessageBox.Show(this, "Removing Theme : " + thmLayer.Name, "ThemeDialogSampleApp", buttons);
                        if (result == DialogResult.Yes)
                        {
                            mainMapControl.Map.Layers.Remove(thmLayer);
                            mainMapControl.Map.Invalidate();
                            _thm = null;
                        }
                    }
                }
                else if (_thm is LabelModifier)
                {
                    if (_themedLabelLayer != null)
                    {
                        LabelModifier labelModifier = _themedLabelLayer.Sources[0].Modifiers[_thm.Alias];
                        if (labelModifier != null)
                        {
                            result = System.Windows.Forms.MessageBox.Show(this, "Removing Theme : " + labelModifier.Name, "ThemeDialogSampleApp", buttons);
                            if (result == DialogResult.Yes)
                            {
                                _themedLabelLayer.Sources[0].Modifiers.Remove(labelModifier);
                                mainMapControl.Map.Invalidate();
                                _thm = null;
                            }
                        }
                    }
                }
                else
                {
                    //nothing to do here...
                }
            }
            catch (MapException)
            {
            }
        }

        //修改专题图
        private void ModifyTheme()
        {
            try
            {
                // Bring up the modify theme dialog for modifier themes	
                if (_themedFeatureLayer != null)
                {
                    FeatureStyleModifier modifier = _themedFeatureLayer.Modifiers[_thm.Alias];
                    if (modifier != null)
                    {
                        if (modifier is DotDensityTheme)
                        {
                            DotDensityTheme thm = modifier as DotDensityTheme;
                            ModifyDotDensityThemeDlg dlg = new ModifyDotDensityThemeDlg(mainMapControl.Map, thm);
                            dlg.ShowDialog();
                        }
                        else if (modifier is RangedTheme)
                        {
                            RangedTheme thm = modifier as RangedTheme;
                            ModifyRangedThemeDlg dlg = new ModifyRangedThemeDlg(mainMapControl.Map, thm);
                            dlg.ShowDialog();
                        }
                        else if (modifier is IndividualValueTheme)
                        {
                            IndividualValueTheme thm = modifier as IndividualValueTheme;
                            ModifyIndValueThemeDlg dlg = new ModifyIndValueThemeDlg(mainMapControl.Map, thm);
                            dlg.ShowDialog();
                        }
                        _themedFeatureLayer.Invalidate();
                    }
                }
                // Bring up the modify theme dialog for object themes
                ObjectThemeLayer thmLayer = mainMapControl.Map.Layers[_thm.Alias] as ObjectThemeLayer;
                if (thmLayer != null)
                {
                    if (thmLayer.Theme is GraduatedSymbolTheme)
                    {
                        GraduatedSymbolTheme thm = thmLayer.Theme as GraduatedSymbolTheme;
                        ModifyGradSymbolThemeDlg dlg = new ModifyGradSymbolThemeDlg(mainMapControl.Map, thm);
                        dlg.ShowDialog();
                    }
                    else if (thmLayer.Theme is BarTheme)
                    {
                        BarTheme thm = thmLayer.Theme as BarTheme;
                        ModifyBarThemeDlg dlg = new ModifyBarThemeDlg(mainMapControl.Map, thm);
                        dlg.ShowDialog();
                    }
                    else if (thmLayer.Theme is PieTheme)
                    {
                        PieTheme thm = thmLayer.Theme as PieTheme;
                        ModifyPieThemeDlg dlg = new ModifyPieThemeDlg(mainMapControl.Map, thm);
                        dlg.ShowDialog();
                    }
                    thmLayer.RebuildTheme();
                }
                if (_themedLabelLayer != null)
                {
                    LabelModifier labelModifier = _themedLabelLayer.Sources[0].Modifiers[_thm.Alias];
                    if (labelModifier != null)
                    {
                        if (labelModifier is RangedLabelTheme)
                        {
                            RangedLabelTheme thm = labelModifier as RangedLabelTheme;
                            ModifyRangedThemeDlg dlg = new ModifyRangedThemeDlg(mainMapControl.Map, thm);
                            dlg.ShowDialog();
                        }
                        else if (labelModifier is IndividualValueLabelTheme)
                        {
                            IndividualValueLabelTheme thm = labelModifier as IndividualValueLabelTheme;
                            ModifyIndValueThemeDlg dlg = new ModifyIndValueThemeDlg(mainMapControl.Map, thm);
                            dlg.ShowDialog();
                        }
                        _themedLabelLayer.Invalidate();
                    }
                }
            }
            catch (MapException)
            {
            }
        }
        #endregion

        
        

        
    }
}