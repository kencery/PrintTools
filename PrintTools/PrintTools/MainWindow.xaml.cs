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
            PrintTool.Title = string.IsNullOrEmpty(indexDic["title"]) ? "打印小工具" : indexDic["title"]; //软件名称
            //打印功能名称
            Leave.Header = string.IsNullOrEmpty(indexDic["leave"]) ? "请假单" : indexDic["leave"];
            
            GoWork.Header = string.IsNullOrEmpty(indexDic["goWork"]) ? "出工单" : indexDic["goWork"];
            CreateName.Content = "接收时间：" + DateTime.Now.ToString("yyyy-MM-dd");
            TabControl.FontSize = 16;
        }
    }
}