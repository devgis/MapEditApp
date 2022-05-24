using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MapInfo.Mapping;
using MapInfo.Styles;
using System.Drawing;

namespace Devgis.Common
{
    /// <summary>
    /// 公共定义类
    /// </summary>
    public class PublicDim
    {
        private static string Caption="系统提示";
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="Message">消息内容</param>
        public static void ShowInfoMessage(String Message)
        {
            System.Windows.Forms.MessageBox.Show(Message, Caption
                ,System.Windows.Forms.MessageBoxButtons.OK
                ,System.Windows.Forms.MessageBoxIcon.Information);
        }
        /// <summary>
        /// 提示错误信息
        /// </summary>
        /// <param name="Message">消息内容</param>
        public static void ShowErrorMessage(String Message)
        {
            System.Windows.Forms.MessageBox.Show(Message,Caption
                , System.Windows.Forms.MessageBoxButtons.OK
                , System.Windows.Forms.MessageBoxIcon.Error);
        }

        /// <summary>
        /// 弹出确认对话框
        /// </summary>
        /// <param name="Message">确认消息</param>
        /// <returns></returns>
        public static DialogResult ShowQuestion(String Message)
        {
            return MessageBox.Show(Message, Caption, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        public static FeatureRenderer MyFeatureRenderer;

        #region 样式定义
        /// <summary>
        /// 主干线样式
        /// </summary>
        public static SimpleLineStyle StyleMainLine = new SimpleLineStyle(new LineWidth(2, LineWidthUnit.Point), 2, Color.Green);//默认线样式

        /// <summary>
        /// 分支线路样式
        /// </summary>
        public static SimpleLineStyle StyleSubLine = new SimpleLineStyle(new LineWidth(1, LineWidthUnit.Point), 2, Color.Green);//默认线样式

        /// <summary>
        /// 杆塔样式
        /// </summary>
        public static SimpleVectorPointStyle StyleGANTA = new SimpleVectorPointStyle(32, Color.White, 4);

        /// <summary>
        /// 杆塔样式
        /// </summary>
        public static BitmapPointStyle StyleBanYaQi = new BitmapPointStyle("CAMP1-32.BMP", BitmapStyles.None, Color.White, 16);

        /// <summary>
        /// 断路器样式
        /// </summary>
        public static BitmapPointStyle StyleDuanLuQi = new BitmapPointStyle("RAIL2-32.BMP", BitmapStyles.None, Color.White, 16);

        /// <summary>
        /// 隔离开关样式
        /// </summary>
        public static BitmapPointStyle StyleGeLiKaiGuan = new BitmapPointStyle("RAIL3-32.BMP", BitmapStyles.None, Color.White, 16);
        #endregion
    }
}
