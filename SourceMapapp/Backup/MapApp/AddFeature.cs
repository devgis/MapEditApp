using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapInfo.Mapping;
using MapInfo.Data;
using MapInfo.Styles;
using MapInfo.Text;
using MapInfo.Windows.Dialogs;
using MapInfo.Engine;
using MapInfo.Geometry;

namespace Devgis.MapApp
{
    public partial class AddFeature : Form
    {
        public string Name;
        public MapInfo.Styles.Style FeatureStyle;
        private StyleType styleType;
        public AddFeature(StyleType StyleType)
        {
            InitializeComponent();
            this.styleType = StyleType;
        }

        private void AddFeature_Load(object sender, EventArgs e)
        {
            CoordSys csys = Session.Current.CoordSysFactory.CreateCoordSys(CoordSysType.NonEarth, null, DistanceUnit.Meter, 0, 0, 0, 0, 0, 0, 0, 0, 0, new MapInfo.Geometry.DRect(0, 0, mapControl1.Map.Size.Width, mapControl1.Map.Size.Height), null);
            if (PublicDim.MyFeatureRenderer == null)
            {
                PublicDim.MyFeatureRenderer = Session.Current.MapFactory.CreateFeatureRenderer("stylesample", "stylesample", mapControl1.Map.Handle, mapControl1.Map.Size, csys);
            }
            mapControl1.Map = PublicDim.MyFeatureRenderer;

            PublicDim.MyFeatureRenderer.SetView(csys.Bounds, csys);

            // used as storage for line tab
            _lineStyle = new SimpleLineStyle();


            // for fill tab
            _fillStyle = new SimpleInterior();

            // vector symbols
            _vectorSymbol = new SimpleVectorPointStyle();

            // bitmap symbol
            _bitmapSymbol = new BitmapPointStyle();

            // font symbol
            _fontSymbol = new FontPointStyle();

            // text
            _textStyle = new TextStyle();
            _textAngle = 0.0;



            switch (styleType)
            {
                case StyleType.Point:
                    SetVectorSymbolSample();
                    break;
                case StyleType.Line:
                    SetLineSample();
                    break;
                case StyleType.Shape:
                    SetFillSample();
                    break;
                case StyleType.Text:
                    SetTextSample();
                    break;
            }
        }

        private void SetVectorSymbolSample()
        {
            try
            {
                FeatureRenderer fr = mapControl1.Map as FeatureRenderer;
                Feature f = fr.VectorSymbolSample();
                CompositeStyle cs = (CompositeStyle)f.Style;
                cs.SymbolStyle = _vectorSymbol;
                fr.Feature = f;
                FeatureStyle = f.Style;
            }
            catch
            { }
        }

        private SimpleLineStyle _lineStyle = null;
        private SimpleInterior _fillStyle = null;
        private SimpleVectorPointStyle _vectorSymbol = null;
        private BitmapPointStyle _bitmapSymbol = null;
        private FontPointStyle _fontSymbol = null;
        private TextStyle _textStyle = null;
        private double _textAngle = 0;
        private Alignment _textAlignment = Alignment.CenterCenter;
        private Spacing _textSpacing = Spacing.Single;
        //private LineStyleDlg _lineStyleDlg = null;
        //private AreaStyleDlg _areaStyleDlg = null;
        //private TextStyleDlg _textStyleDlg = null;
        //private SymbolStyleDlg _symbolStyleDlg = null;
        private void SetLineSample()
        {
            FeatureRenderer fr = mapControl1.Map as FeatureRenderer;
            Feature f = null;
            f = fr.LineSample(10.0, FeatureRenderer.LineSampleType.CrossedDiagonals);
            CompositeStyle cs = (CompositeStyle)f.Style;
            cs.LineStyle = _lineStyle;
            fr.Feature= f;
        }

        private void SetFillSample()
        {
            // use linestyle also
            FeatureRenderer fr = mapControl1.Map as FeatureRenderer;
            Feature f = fr.AreaSample(10.0);
            CompositeStyle cs = (CompositeStyle)f.Style;
            cs.AreaStyle.Border = _lineStyle;
            cs.AreaStyle.Interior = _fillStyle;
            fr.Feature = f;
        }

        private void SetTextSample()
        {
            String textBoxString = "Text文字";
            string textString;
            if (textBoxString.Length == 0)
            {
                textString = " ";
            }
            else
            {
                textString = textBoxString.Replace(Environment.NewLine, "\n");
            }
            FeatureRenderer fr = mapControl1.Map as FeatureRenderer;
            Feature f = fr.TextSample(textString, _textAngle, _textAlignment, _textSpacing, _textStyle);
            CompositeStyle cs = (CompositeStyle)f.Style;
            cs.TextStyle = _textStyle;
            fr.Feature = f;
        }

        private void btnStyle_Click(object sender, EventArgs e)
        {
            switch (this.styleType)
            {
                case StyleType.Point:
                    SymbolStyleDlg _symbolStyleDlg = new SymbolStyleDlg();
			        if (_symbolStyleDlg.ShowDialog() == DialogResult.OK) {
                        FeatureRenderer fr = mapControl1.Map as FeatureRenderer;
                        Feature f = fr.VectorSymbolSample();
                        CompositeStyle cs = (CompositeStyle)f.Style;
                        cs.SymbolStyle = _symbolStyleDlg.SymbolStyle;
                        fr.Feature = f;
                        FeatureStyle = f.Style;
			        }
                    break;
                case StyleType.Line:
                    LineStyleDlg _LineStyleDlg = new LineStyleDlg();
                    if (_LineStyleDlg.ShowDialog() == DialogResult.OK)
                    {
                        FeatureRenderer fr = mapControl1.Map as FeatureRenderer;
                        Feature f = fr.LineSample(10.0, FeatureRenderer.LineSampleType.CrossedDiagonals);
                        CompositeStyle cs = (CompositeStyle)f.Style;
                        cs.LineStyle = _LineStyleDlg.LineStyle;
                        fr.Feature = f;
                        FeatureStyle = f.Style;
                    }
                    break;
                case StyleType.Shape:
                    AreaStyleDlg _ShapeStyleDlg = new AreaStyleDlg();
                    if (_ShapeStyleDlg.ShowDialog() == DialogResult.OK)
                    {
                        FeatureRenderer fr = mapControl1.Map as FeatureRenderer;
                        Feature f = fr.AreaSample(10.0);
                        CompositeStyle cs = (CompositeStyle)f.Style;
                        cs.AreaStyle = _ShapeStyleDlg.AreaStyle;
                        fr.Feature = f;
                        FeatureStyle = f.Style;
                    }
                    break;
                case StyleType.Text:
                    TextStyleDlg _TestStyleDlg = new TextStyleDlg();
                    if (_TestStyleDlg.ShowDialog() == DialogResult.OK)
                    {
                        String textBoxString = "Text文字";
                        string textString;
                        if (textBoxString.Length == 0)
                        {
                            textString = " ";
                        }
                        else
                        {
                            textString = textBoxString.Replace(Environment.NewLine, "\n");
                        }
                        FeatureRenderer fr = mapControl1.Map as FeatureRenderer;
                        Feature f = fr.TextSample(textString, _textAngle, _textAlignment, _textSpacing, _textStyle);
                        CompositeStyle cs = (CompositeStyle)f.Style;
                        cs.TextStyle =  new TextStyle(_TestStyleDlg.FontStyle);
                        fr.Feature = f;
                        FeatureStyle = f.Style;
                    }
                    
                    break;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbName.Text.Trim()))
            {
                PublicDim.ShowInfoMessage("请输入名称！");
                tbName.Focus();
                return;
            }
            else
            {
                Name = tbName.Text;
                this.DialogResult = DialogResult.OK;
            }

        }
    }
}