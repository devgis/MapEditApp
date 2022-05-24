namespace Devgis.MapApp
{
    partial class MainMap
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMap));
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.tsslZoomLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.tsmiSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSystemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTheme = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiThemeCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiThemeDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiThemeEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelpHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMapControl = new MapInfo.Windows.Controls.MapControl();
            this.mainToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.pEagleMap = new System.Windows.Forms.Panel();
            this.eagleMap = new MapInfo.Windows.Controls.MapControl();
            this.sysToolStrip = new System.Windows.Forms.ToolStrip();
            this.sysTooBtnExit = new System.Windows.Forms.ToolStripButton();
            this.mapToolStrip = new System.Windows.Forms.ToolStrip();
            this.mapToolBtnLayer = new System.Windows.Forms.ToolStripButton();
            this.mapToolBtnSelect = new System.Windows.Forms.ToolStripButton();
            this.mapToolBtnZoomIn = new System.Windows.Forms.ToolStripButton();
            this.mapToolBtnZoomOut = new System.Windows.Forms.ToolStripButton();
            this.mapToolBtnPan = new System.Windows.Forms.ToolStripButton();
            this.mapToolBtnEagele = new System.Windows.Forms.ToolStripButton();
            this.mapToolBtnSearch = new System.Windows.Forms.ToolStripButton();
            this.mapToolBtnCenter = new System.Windows.Forms.ToolStripButton();
            this.mapToolBtnDistance = new System.Windows.Forms.ToolStripButton();
            this.mapToolBtnArea = new System.Windows.Forms.ToolStripButton();
            this.mapToolBtnIdentify = new System.Windows.Forms.ToolStripButton();
            this.mapToolBtnImage = new System.Windows.Forms.ToolStripButton();
            this.mapToolBtnPrint = new System.Windows.Forms.ToolStripButton();
            this.MapToolBtnReset = new System.Windows.Forms.ToolStripButton();
            this.MapToolBtnData = new System.Windows.Forms.ToolStripButton();
            this.editToolStrip = new System.Windows.Forms.ToolStrip();
            this.editToolBtnPoint = new System.Windows.Forms.ToolStripButton();
            this.editToolBtnLine = new System.Windows.Forms.ToolStripButton();
            this.editToolBtnCircle = new System.Windows.Forms.ToolStripButton();
            this.editToolBtnOval = new System.Windows.Forms.ToolStripButton();
            this.editToolBtnRectangle = new System.Windows.Forms.ToolStripButton();
            this.editToolBtnPolygon = new System.Windows.Forms.ToolStripButton();
            this.mainStatusStrip.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.mainToolStripContainer.ContentPanel.SuspendLayout();
            this.mainToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.mainToolStripContainer.SuspendLayout();
            this.pEagleMap.SuspendLayout();
            this.sysToolStrip.SuspendLayout();
            this.mapToolStrip.SuspendLayout();
            this.editToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslZoomLabel});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 342);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(797, 22);
            this.mainStatusStrip.TabIndex = 2;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // tsslZoomLabel
            // 
            this.tsslZoomLabel.Name = "tsslZoomLabel";
            this.tsslZoomLabel.Size = new System.Drawing.Size(0, 17);
            this.tsslZoomLabel.ToolTipText = "地图缩放";
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSystem,
            this.tsmiTheme,
            this.tsmiHelp});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(797, 25);
            this.mainMenu.TabIndex = 4;
            this.mainMenu.Text = "主菜单";
            // 
            // tsmiSystem
            // 
            this.tsmiSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSystemExit});
            this.tsmiSystem.Image = global::Devgis.MapApp.Properties.Resources._027;
            this.tsmiSystem.Name = "tsmiSystem";
            this.tsmiSystem.Size = new System.Drawing.Size(75, 21);
            this.tsmiSystem.Text = "系统(S)";
            // 
            // tsmiSystemExit
            // 
            this.tsmiSystemExit.Image = global::Devgis.MapApp.Properties.Resources._026;
            this.tsmiSystemExit.Name = "tsmiSystemExit";
            this.tsmiSystemExit.Size = new System.Drawing.Size(115, 22);
            this.tsmiSystemExit.Text = "退出(&E)";
            // 
            // tsmiTheme
            // 
            this.tsmiTheme.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiThemeCreate,
            this.tsmiThemeDelete,
            this.tsmiThemeEdit});
            this.tsmiTheme.Image = global::Devgis.MapApp.Properties.Resources.工作空间;
            this.tsmiTheme.Name = "tsmiTheme";
            this.tsmiTheme.Size = new System.Drawing.Size(87, 21);
            this.tsmiTheme.Text = "专题图(&T)";
            // 
            // tsmiThemeCreate
            // 
            this.tsmiThemeCreate.Image = global::Devgis.MapApp.Properties.Resources._046;
            this.tsmiThemeCreate.Name = "tsmiThemeCreate";
            this.tsmiThemeCreate.Size = new System.Drawing.Size(117, 22);
            this.tsmiThemeCreate.Text = "创建(&C)";
            this.tsmiThemeCreate.Click += new System.EventHandler(this.tsmiThemeCreate_Click);
            // 
            // tsmiThemeDelete
            // 
            this.tsmiThemeDelete.Enabled = false;
            this.tsmiThemeDelete.Image = global::Devgis.MapApp.Properties.Resources._036;
            this.tsmiThemeDelete.Name = "tsmiThemeDelete";
            this.tsmiThemeDelete.Size = new System.Drawing.Size(117, 22);
            this.tsmiThemeDelete.Text = "删除(&D)";
            this.tsmiThemeDelete.Click += new System.EventHandler(this.tsmiThemeDelete_Click);
            // 
            // tsmiThemeEdit
            // 
            this.tsmiThemeEdit.Enabled = false;
            this.tsmiThemeEdit.Image = global::Devgis.MapApp.Properties.Resources._040;
            this.tsmiThemeEdit.Name = "tsmiThemeEdit";
            this.tsmiThemeEdit.Size = new System.Drawing.Size(117, 22);
            this.tsmiThemeEdit.Text = "修改(&E)";
            this.tsmiThemeEdit.Click += new System.EventHandler(this.tsmiThemeEdit_Click);
            // 
            // tsmiHelp
            // 
            this.tsmiHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiHelpHelp,
            this.tsmiHelpAbout});
            this.tsmiHelp.Image = global::Devgis.MapApp.Properties.Resources._050;
            this.tsmiHelp.Name = "tsmiHelp";
            this.tsmiHelp.Size = new System.Drawing.Size(77, 21);
            this.tsmiHelp.Text = "帮助(&H)";
            // 
            // tsmiHelpHelp
            // 
            this.tsmiHelpHelp.Image = global::Devgis.MapApp.Properties.Resources._050;
            this.tsmiHelpHelp.Name = "tsmiHelpHelp";
            this.tsmiHelpHelp.Size = new System.Drawing.Size(117, 22);
            this.tsmiHelpHelp.Text = "帮助(&H)";
            // 
            // tsmiHelpAbout
            // 
            this.tsmiHelpAbout.Image = global::Devgis.MapApp.Properties.Resources._033;
            this.tsmiHelpAbout.Name = "tsmiHelpAbout";
            this.tsmiHelpAbout.Size = new System.Drawing.Size(117, 22);
            this.tsmiHelpAbout.Text = "关于(&A)";
            this.tsmiHelpAbout.Click += new System.EventHandler(this.tsmiHelpAbout_Click);
            // 
            // mainMapControl
            // 
            this.mainMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainMapControl.IgnoreLostFocusEvent = false;
            this.mainMapControl.Location = new System.Drawing.Point(0, 0);
            this.mainMapControl.Name = "mainMapControl";
            this.mainMapControl.Size = new System.Drawing.Size(797, 292);
            this.mainMapControl.TabIndex = 0;
            this.mainMapControl.Text = "主地图";
            this.mainMapControl.Tools.LeftButtonTool = null;
            this.mainMapControl.Tools.MiddleButtonTool = null;
            this.mainMapControl.Tools.RightButtonTool = null;
            // 
            // mainToolStripContainer
            // 
            // 
            // mainToolStripContainer.ContentPanel
            // 
            this.mainToolStripContainer.ContentPanel.Controls.Add(this.pEagleMap);
            this.mainToolStripContainer.ContentPanel.Controls.Add(this.mainMapControl);
            this.mainToolStripContainer.ContentPanel.Size = new System.Drawing.Size(797, 292);
            this.mainToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainToolStripContainer.Location = new System.Drawing.Point(0, 25);
            this.mainToolStripContainer.Name = "mainToolStripContainer";
            this.mainToolStripContainer.Size = new System.Drawing.Size(797, 317);
            this.mainToolStripContainer.TabIndex = 5;
            this.mainToolStripContainer.Text = "toolStripContainer1";
            // 
            // mainToolStripContainer.TopToolStripPanel
            // 
            this.mainToolStripContainer.TopToolStripPanel.Controls.Add(this.sysToolStrip);
            this.mainToolStripContainer.TopToolStripPanel.Controls.Add(this.mapToolStrip);
            // 
            // pEagleMap
            // 
            this.pEagleMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pEagleMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pEagleMap.Controls.Add(this.eagleMap);
            this.pEagleMap.Location = new System.Drawing.Point(0, 163);
            this.pEagleMap.Name = "pEagleMap";
            this.pEagleMap.Size = new System.Drawing.Size(217, 128);
            this.pEagleMap.TabIndex = 1;
            // 
            // eagleMap
            // 
            this.eagleMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.eagleMap.IgnoreLostFocusEvent = false;
            this.eagleMap.Location = new System.Drawing.Point(0, 0);
            this.eagleMap.Name = "eagleMap";
            this.eagleMap.Size = new System.Drawing.Size(215, 126);
            this.eagleMap.TabIndex = 0;
            this.eagleMap.Text = "mapControl1";
            this.eagleMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.eagleMap_MouseClick);
            this.eagleMap.Tools.LeftButtonTool = null;
            this.eagleMap.Tools.MiddleButtonTool = null;
            this.eagleMap.Tools.RightButtonTool = null;
            // 
            // sysToolStrip
            // 
            this.sysToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.sysToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sysTooBtnExit});
            this.sysToolStrip.Location = new System.Drawing.Point(3, 0);
            this.sysToolStrip.Name = "sysToolStrip";
            this.sysToolStrip.Size = new System.Drawing.Size(35, 25);
            this.sysToolStrip.TabIndex = 2;
            // 
            // sysTooBtnExit
            // 
            this.sysTooBtnExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.sysTooBtnExit.Image = global::Devgis.MapApp.Properties.Resources._026;
            this.sysTooBtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sysTooBtnExit.Name = "sysTooBtnExit";
            this.sysTooBtnExit.Size = new System.Drawing.Size(23, 22);
            this.sysTooBtnExit.Text = "退出";
            this.sysTooBtnExit.ToolTipText = "退出系统";
            this.sysTooBtnExit.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // mapToolStrip
            // 
            this.mapToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.mapToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapToolBtnLayer,
            this.mapToolBtnSelect,
            this.mapToolBtnZoomIn,
            this.mapToolBtnZoomOut,
            this.mapToolBtnPan,
            this.mapToolBtnEagele,
            this.mapToolBtnSearch,
            this.mapToolBtnCenter,
            this.mapToolBtnDistance,
            this.mapToolBtnArea,
            this.mapToolBtnIdentify,
            this.mapToolBtnImage,
            this.mapToolBtnPrint,
            this.MapToolBtnReset,
            this.MapToolBtnData});
            this.mapToolStrip.Location = new System.Drawing.Point(38, 0);
            this.mapToolStrip.Name = "mapToolStrip";
            this.mapToolStrip.Size = new System.Drawing.Size(357, 25);
            this.mapToolStrip.TabIndex = 0;
            // 
            // mapToolBtnLayer
            // 
            this.mapToolBtnLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mapToolBtnLayer.Image = global::Devgis.MapApp.Properties.Resources._004;
            this.mapToolBtnLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mapToolBtnLayer.Name = "mapToolBtnLayer";
            this.mapToolBtnLayer.Size = new System.Drawing.Size(23, 22);
            this.mapToolBtnLayer.Text = "图层";
            this.mapToolBtnLayer.ToolTipText = "编辑图层相关信息";
            this.mapToolBtnLayer.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // mapToolBtnSelect
            // 
            this.mapToolBtnSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mapToolBtnSelect.Image = global::Devgis.MapApp.Properties.Resources.选择物体;
            this.mapToolBtnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mapToolBtnSelect.Name = "mapToolBtnSelect";
            this.mapToolBtnSelect.Size = new System.Drawing.Size(23, 22);
            this.mapToolBtnSelect.Text = "选择";
            this.mapToolBtnSelect.ToolTipText = "选择图元";
            this.mapToolBtnSelect.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // mapToolBtnZoomIn
            // 
            this.mapToolBtnZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mapToolBtnZoomIn.Image = global::Devgis.MapApp.Properties.Resources.放大;
            this.mapToolBtnZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mapToolBtnZoomIn.Name = "mapToolBtnZoomIn";
            this.mapToolBtnZoomIn.Size = new System.Drawing.Size(23, 22);
            this.mapToolBtnZoomIn.Text = "放大";
            this.mapToolBtnZoomIn.ToolTipText = "放大地图";
            this.mapToolBtnZoomIn.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // mapToolBtnZoomOut
            // 
            this.mapToolBtnZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mapToolBtnZoomOut.Image = global::Devgis.MapApp.Properties.Resources.缩小;
            this.mapToolBtnZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mapToolBtnZoomOut.Name = "mapToolBtnZoomOut";
            this.mapToolBtnZoomOut.Size = new System.Drawing.Size(23, 22);
            this.mapToolBtnZoomOut.Text = "缩小";
            this.mapToolBtnZoomOut.ToolTipText = "缩小地图";
            this.mapToolBtnZoomOut.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // mapToolBtnPan
            // 
            this.mapToolBtnPan.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mapToolBtnPan.Image = global::Devgis.MapApp.Properties.Resources.平移;
            this.mapToolBtnPan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mapToolBtnPan.Name = "mapToolBtnPan";
            this.mapToolBtnPan.Size = new System.Drawing.Size(23, 22);
            this.mapToolBtnPan.Text = "平移";
            this.mapToolBtnPan.ToolTipText = "平移地图";
            this.mapToolBtnPan.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // mapToolBtnEagele
            // 
            this.mapToolBtnEagele.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mapToolBtnEagele.Image = global::Devgis.MapApp.Properties.Resources.组合对象;
            this.mapToolBtnEagele.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mapToolBtnEagele.Name = "mapToolBtnEagele";
            this.mapToolBtnEagele.Size = new System.Drawing.Size(23, 22);
            this.mapToolBtnEagele.Text = "鹰眼";
            this.mapToolBtnEagele.ToolTipText = "打开或者关闭鹰眼地图";
            this.mapToolBtnEagele.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // mapToolBtnSearch
            // 
            this.mapToolBtnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mapToolBtnSearch.Image = global::Devgis.MapApp.Properties.Resources._007;
            this.mapToolBtnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mapToolBtnSearch.Name = "mapToolBtnSearch";
            this.mapToolBtnSearch.Size = new System.Drawing.Size(23, 22);
            this.mapToolBtnSearch.Text = "地图查询";
            this.mapToolBtnSearch.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // mapToolBtnCenter
            // 
            this.mapToolBtnCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mapToolBtnCenter.Image = global::Devgis.MapApp.Properties.Resources.刺点;
            this.mapToolBtnCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mapToolBtnCenter.Name = "mapToolBtnCenter";
            this.mapToolBtnCenter.Size = new System.Drawing.Size(23, 22);
            this.mapToolBtnCenter.Text = "中点";
            this.mapToolBtnCenter.ToolTipText = "设置当前点为地图中点";
            this.mapToolBtnCenter.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // mapToolBtnDistance
            // 
            this.mapToolBtnDistance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mapToolBtnDistance.Image = global::Devgis.MapApp.Properties.Resources.量算距离;
            this.mapToolBtnDistance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mapToolBtnDistance.Name = "mapToolBtnDistance";
            this.mapToolBtnDistance.Size = new System.Drawing.Size(23, 22);
            this.mapToolBtnDistance.Text = "测量距离";
            this.mapToolBtnDistance.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // mapToolBtnArea
            // 
            this.mapToolBtnArea.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mapToolBtnArea.Image = global::Devgis.MapApp.Properties.Resources.量算面积;
            this.mapToolBtnArea.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mapToolBtnArea.Name = "mapToolBtnArea";
            this.mapToolBtnArea.Size = new System.Drawing.Size(23, 22);
            this.mapToolBtnArea.Text = "测量面积";
            this.mapToolBtnArea.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // mapToolBtnIdentify
            // 
            this.mapToolBtnIdentify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mapToolBtnIdentify.Image = global::Devgis.MapApp.Properties.Resources.Identify;
            this.mapToolBtnIdentify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mapToolBtnIdentify.Name = "mapToolBtnIdentify";
            this.mapToolBtnIdentify.Size = new System.Drawing.Size(23, 22);
            this.mapToolBtnIdentify.Text = "信息";
            this.mapToolBtnIdentify.ToolTipText = "查看图元信息";
            this.mapToolBtnIdentify.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // mapToolBtnImage
            // 
            this.mapToolBtnImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mapToolBtnImage.Image = global::Devgis.MapApp.Properties.Resources._038;
            this.mapToolBtnImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mapToolBtnImage.Name = "mapToolBtnImage";
            this.mapToolBtnImage.Size = new System.Drawing.Size(23, 22);
            this.mapToolBtnImage.Text = "导出";
            this.mapToolBtnImage.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // mapToolBtnPrint
            // 
            this.mapToolBtnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mapToolBtnPrint.Image = global::Devgis.MapApp.Properties.Resources.打印;
            this.mapToolBtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mapToolBtnPrint.Name = "mapToolBtnPrint";
            this.mapToolBtnPrint.Size = new System.Drawing.Size(23, 22);
            this.mapToolBtnPrint.Text = "打印";
            this.mapToolBtnPrint.ToolTipText = "打印地图";
            this.mapToolBtnPrint.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // MapToolBtnReset
            // 
            this.MapToolBtnReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MapToolBtnReset.Image = global::Devgis.MapApp.Properties.Resources.g;
            this.MapToolBtnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MapToolBtnReset.Name = "MapToolBtnReset";
            this.MapToolBtnReset.Size = new System.Drawing.Size(23, 22);
            this.MapToolBtnReset.Text = "重设";
            this.MapToolBtnReset.ToolTipText = "重设地图到初始化演示";
            this.MapToolBtnReset.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // MapToolBtnData
            // 
            this.MapToolBtnData.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.MapToolBtnData.Image = global::Devgis.MapApp.Properties.Resources._014;
            this.MapToolBtnData.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.MapToolBtnData.Name = "MapToolBtnData";
            this.MapToolBtnData.Size = new System.Drawing.Size(23, 22);
            this.MapToolBtnData.Text = "属性表";
            this.MapToolBtnData.ToolTipText = "显示地图属性表";
            this.MapToolBtnData.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // editToolStrip
            // 
            this.editToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.editToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolBtnPoint,
            this.editToolBtnLine,
            this.editToolBtnCircle,
            this.editToolBtnOval,
            this.editToolBtnRectangle,
            this.editToolBtnPolygon});
            this.editToolStrip.Location = new System.Drawing.Point(397, 25);
            this.editToolStrip.Name = "editToolStrip";
            this.editToolStrip.Size = new System.Drawing.Size(181, 25);
            this.editToolStrip.TabIndex = 1;
            // 
            // editToolBtnPoint
            // 
            this.editToolBtnPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editToolBtnPoint.Image = global::Devgis.MapApp.Properties.Resources.绘制点;
            this.editToolBtnPoint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editToolBtnPoint.Name = "editToolBtnPoint";
            this.editToolBtnPoint.Size = new System.Drawing.Size(23, 22);
            this.editToolBtnPoint.Text = "点";
            this.editToolBtnPoint.ToolTipText = "绘制点";
            this.editToolBtnPoint.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // editToolBtnLine
            // 
            this.editToolBtnLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editToolBtnLine.Image = global::Devgis.MapApp.Properties.Resources.绘制多线段;
            this.editToolBtnLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editToolBtnLine.Name = "editToolBtnLine";
            this.editToolBtnLine.Size = new System.Drawing.Size(23, 22);
            this.editToolBtnLine.Text = "绘制线";
            this.editToolBtnLine.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // editToolBtnCircle
            // 
            this.editToolBtnCircle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editToolBtnCircle.Image = global::Devgis.MapApp.Properties.Resources.绘制两点圆;
            this.editToolBtnCircle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editToolBtnCircle.Name = "editToolBtnCircle";
            this.editToolBtnCircle.Size = new System.Drawing.Size(23, 22);
            this.editToolBtnCircle.Text = "绘制圆";
            this.editToolBtnCircle.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // editToolBtnOval
            // 
            this.editToolBtnOval.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editToolBtnOval.Image = global::Devgis.MapApp.Properties.Resources.绘制椭圆;
            this.editToolBtnOval.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editToolBtnOval.Name = "editToolBtnOval";
            this.editToolBtnOval.Size = new System.Drawing.Size(23, 22);
            this.editToolBtnOval.Text = "绘制椭圆";
            this.editToolBtnOval.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // editToolBtnRectangle
            // 
            this.editToolBtnRectangle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editToolBtnRectangle.Image = global::Devgis.MapApp.Properties.Resources.绘制矩形;
            this.editToolBtnRectangle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editToolBtnRectangle.Name = "editToolBtnRectangle";
            this.editToolBtnRectangle.Size = new System.Drawing.Size(23, 22);
            this.editToolBtnRectangle.Text = "矩形";
            this.editToolBtnRectangle.ToolTipText = "绘制矩形";
            this.editToolBtnRectangle.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // editToolBtnPolygon
            // 
            this.editToolBtnPolygon.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editToolBtnPolygon.Image = global::Devgis.MapApp.Properties.Resources.绘制多边形;
            this.editToolBtnPolygon.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editToolBtnPolygon.Name = "editToolBtnPolygon";
            this.editToolBtnPolygon.Size = new System.Drawing.Size(23, 22);
            this.editToolBtnPolygon.Text = "绘制多面体";
            this.editToolBtnPolygon.Click += new System.EventHandler(this.mapToolButton_Click);
            // 
            // MainMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 364);
            this.Controls.Add(this.editToolStrip);
            this.Controls.Add(this.mainToolStripContainer);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenu;
            this.Name = "MainMap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DevGIS GIS系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainMap_Load);
            this.Click += new System.EventHandler(this.mapToolButton_Click);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMap_FormClosing);
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.mainToolStripContainer.ContentPanel.ResumeLayout(false);
            this.mainToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.mainToolStripContainer.TopToolStripPanel.PerformLayout();
            this.mainToolStripContainer.ResumeLayout(false);
            this.mainToolStripContainer.PerformLayout();
            this.pEagleMap.ResumeLayout(false);
            this.sysToolStrip.ResumeLayout(false);
            this.sysToolStrip.PerformLayout();
            this.mapToolStrip.ResumeLayout(false);
            this.mapToolStrip.PerformLayout();
            this.editToolStrip.ResumeLayout(false);
            this.editToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsslZoomLabel;
        private System.Windows.Forms.MenuStrip mainMenu;
        private MapInfo.Windows.Controls.MapControl mainMapControl;
        private System.Windows.Forms.ToolStripMenuItem tsmiSystem;
        private System.Windows.Forms.ToolStripMenuItem tsmiSystemExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelpHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmiHelpAbout;
        private System.Windows.Forms.ToolStripContainer mainToolStripContainer;
        private System.Windows.Forms.ToolStrip mapToolStrip;
        private System.Windows.Forms.ToolStrip editToolStrip;
        private System.Windows.Forms.ToolStrip sysToolStrip;
        private System.Windows.Forms.ToolStripButton sysTooBtnExit;
        private System.Windows.Forms.ToolStripButton mapToolBtnLayer;
        private System.Windows.Forms.ToolStripButton mapToolBtnSelect;
        private System.Windows.Forms.ToolStripButton mapToolBtnZoomIn;
        private System.Windows.Forms.ToolStripButton mapToolBtnZoomOut;
        private System.Windows.Forms.ToolStripButton mapToolBtnPan;
        private System.Windows.Forms.ToolStripButton editToolBtnPoint;
        private System.Windows.Forms.ToolStripButton editToolBtnLine;
        private System.Windows.Forms.ToolStripButton editToolBtnCircle;
        private System.Windows.Forms.ToolStripButton editToolBtnOval;
        private System.Windows.Forms.ToolStripButton editToolBtnPolygon;
        private System.Windows.Forms.Panel pEagleMap;
        private MapInfo.Windows.Controls.MapControl eagleMap;
        private System.Windows.Forms.ToolStripButton mapToolBtnEagele;
        private System.Windows.Forms.ToolStripButton mapToolBtnSearch;
        private System.Windows.Forms.ToolStripButton mapToolBtnCenter;
        private System.Windows.Forms.ToolStripButton mapToolBtnDistance;
        private System.Windows.Forms.ToolStripButton mapToolBtnArea;
        private System.Windows.Forms.ToolStripButton mapToolBtnIdentify;
        private System.Windows.Forms.ToolStripButton editToolBtnRectangle;
        private System.Windows.Forms.ToolStripButton mapToolBtnImage;
        private System.Windows.Forms.ToolStripButton mapToolBtnPrint;
        private System.Windows.Forms.ToolStripButton MapToolBtnReset;
        private System.Windows.Forms.ToolStripButton MapToolBtnData;
        private System.Windows.Forms.ToolStripMenuItem tsmiTheme;
        private System.Windows.Forms.ToolStripMenuItem tsmiThemeCreate;
        private System.Windows.Forms.ToolStripMenuItem tsmiThemeDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiThemeEdit;
    }
}

