using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MapInfo.Mapping;

namespace Devgis.MapApp
{
    /// <summary>
    /// 公共定义类
    /// </summary>
    public class PublicDim
    {
        private static string Caption="Devgis提示";
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
    }
}
