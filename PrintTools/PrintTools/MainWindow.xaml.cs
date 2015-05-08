using System;
using System.Windows;

namespace PrintTools
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //初始化信息的时候给首页的内容读取XML文件中的信息显示，读取需要显示的内容
            var indexDic = ReadXml.ReadXmlTitle();
            //软件名称，打印时间
            PrintTool.Title = string.IsNullOrEmpty(indexDic["title"]) ? "打印小工具" : indexDic["title"]; //软件名称
            LeavePrintDateTime.Text = "打印时间：" + DateTime.Now.ToString("yyyy-MM-dd");

            //请假单和出工单共用的方法
            LevelCompanyName.Text =
                string.IsNullOrEmpty(indexDic["companyName"]) ? "甘肃联众建筑设计有限责任公司" : indexDic["companyName"];

            //打印_请假条，首页顶端想要的信息 
            LeaveTitle.Header = LeaveName.Text = string.IsNullOrEmpty(indexDic["leave"]) ? "请假条" : indexDic["leave"];
            //打印备注：最多允许6条备注，超过6条备注后页面会变得很乱
            LeveRemark.Text = string.IsNullOrEmpty(indexDic["leaveRemak"]) ? "暂无备注" : indexDic["leaveRemak"];
            //打印_出工单，首页顶端想要的信息 
            GoWork.Header = string.IsNullOrEmpty(indexDic["goWork"]) ? "出工单" : indexDic["goWork"];

            TabControl.FontSize = 16;
        }

        /// <summary>
        /// 直接打印请假单的实现，调用模板页直接打印
        /// </summary>
        private void PrintLeave_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 打印预览请假单的实现，调用预览模板页进行并且打印
        /// </summary>
        private void PrintPreLeave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}