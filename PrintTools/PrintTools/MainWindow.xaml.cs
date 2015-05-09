using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Threading;


namespace PrintTools
{
    /// <summary>
    /// 打印主页面的实现，打印主页面和打印出来的纸页面的布局各走各的布局
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 使用委托传递方法当做参数进行调用打印的方法
        /// </summary>
        private delegate void DoPrintMethod(PrintDialog printDialog, DocumentPaginator documentPaginator);

        /// <summary>
        /// 定义委托执行将打印完成的控件重新置为可用
        /// </summary>
        private delegate void EnableButtonMethod();

        /// <summary>
        /// 定义时间判断打印控件的启用禁用状态
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// 初始化页面首次调用的方法
        /// </summary>
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
            //初始化打印机状态，使其项目调用打印机接口   ，弹出框供用户可以随机选择打印的模板
            var printDialog = new PrintDialog();
            //首先判断只有窗口未打开才能调用打印驱动
            if (printDialog.ShowDialog() != true)
            {
                return;
            }
            FlowDocument flowDocument =
                PrintPreWindow.LoadDocumentAndRender(@"PrintTemplete/LeavePrintTemplate.xaml", "");
            Dispatcher.BeginInvoke(new DoPrintMethod(DoPrint), DispatcherPriority.ApplicationIdle, printDialog,
                ((IDocumentPaginatorSource) flowDocument).DocumentPaginator);

            #region---------------------直接打印实现，但是需要安装OneNote或者驱动--------------------------

            //PrintLeave.IsEnabled = false; //当单击之后将按钮置为非启用状态
            ////初始化打印机状态，使其项目调用打印机接口
            //var printDialog = new PrintDialog();
            //FlowDocument flowDocument = PrintPreWindow.LoadDocumentAndRender(
            //    @"PrintTemplete/LeavePrintTemplate.xaml", "");
            ////执行异步委托调用参数
            //Dispatcher.BeginInvoke(new DoPrintMethod(DoPrint), DispatcherPriority.ApplicationIdle, printDialog,
            //    ((IDocumentPaginatorSource) flowDocument).DocumentPaginator);
            ////定义执行完成之后释放资源并且将按钮状职位可用
            //_timer = new Timer(TimerCallBack, null, 3000, Timeout.Infinite);

            #endregion
        }

        /// <summary>
        /// 打印预览请假单的实现，调用预览模板页进行并且打印
        /// </summary>
        private void PrintPreLeave_Click(object sender, RoutedEventArgs e)
        {

            var print = new PrintPreWindow(@"PrintTemplete/LeavePrintTemplate.xaml", "")
            {
                Owner = this,
                ShowInTaskbar = false
            };
            print.ShowDialog();
        }

        /// <summary>
        /// 实现调用打印机驱动程序
        /// </summary>
        private void DoPrint(PrintDialog printDialog, DocumentPaginator documentPaginator)
        {
            printDialog.PrintDocument(documentPaginator, "Document Print");
        }

        /// <summary>
        /// 调用时间的回调，并且释放资源
        /// </summary>
        /// <param name="objectdata"></param>
        public void TimerCallBack(Object objectdata)
        {
            _timer.Dispose();
            Dispatcher.BeginInvoke(new EnableButtonMethod(EnableButton));
        }

        /// <summary>
        /// 将单击按钮置为可用状态
        /// </summary>
        private void EnableButton()
        {
            PrintLeave.IsEnabled = true;
        }
    }
}