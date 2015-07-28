using System;
using System.Collections.Generic;
using System.Linq;
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
        private Timer timer;

        /// <summary>
        /// 初始化页面首次调用的方法
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //初始化信息的时候给首页的内容读取XML文件中的信息显示，读取需要显示的内容
            var indexDic = ReadXml.ReadXmlTitle();

            //通用内容整理—打印请假条，出工单
            PrintTool.Title = string.IsNullOrEmpty(indexDic["title"]) ? "打印小工具" : indexDic["title"]; //软件名称
            LeavePrintDateTime.Text = GoWorkPrintDateTime.Text = "打印时间：" + DateTime.Now.ToString("yyyy-MM-dd"); //打印时间
            LevelCompanyName.Text = GoWorkCompanyName.Text = string.IsNullOrEmpty(indexDic["companyName"])
                ? "甘肃联众建筑设计有限责任公司"
                : indexDic["companyName"]; //请假单和出工单共用的方法
            TabControl.FontSize = 16; //字体大小限制为16px

            //打印—请假条
            LeaveTitle.Header = LeaveName.Text = string.IsNullOrEmpty(indexDic["leave"]) ? "请假条" : indexDic["leave"];
            LeveRemark.Text = string.IsNullOrEmpty(indexDic["leaveRemak"]) ? "暂无备注" : indexDic["leaveRemak"]; //打印请假条备注

            //打印—出工单
            GoWork.Header = GoWorkName.Text = string.IsNullOrEmpty(indexDic["goWork"]) ? "出工单" : indexDic["goWork"];
            GoWorkRemark.Text = string.IsNullOrEmpty(indexDic["goWorkRemrk"]) ? "暂无备注" : indexDic["goWorkRemrk"];
            string departMent = string.IsNullOrEmpty(indexDic["departMent"]) ? "建筑室,结构室,设备室、后勤" : indexDic["departMent"];
            //读取下拉列表信息
            BindDepartMentInfo(departMent);
        }

        /// <summary>
        /// 直接打印请假单的实现，调用模板页直接打印
        /// </summary>
        private void PrintLeave_Click(object sender, RoutedEventArgs e)
        {
            //初始化打印机状态，使其项目调用打印机接口，弹出框供用户可以随机选择打印的模板
            var printDialog = new PrintDialog();
            //首先判断只有窗口未打开才能调用打印驱动
            if (printDialog.ShowDialog() != true)
            {
                return;
            }
            //获取前台传递的信息
            var departmentText = DepartmentBox.Text;
            bool? thingHoliday = ThingHoliday.IsChecked; //事假
            bool? loseHoliday = LoseHoliday.IsChecked; //丧事
            bool? wedHoliday = WedHoliday.IsChecked; //婚事
            bool? leaveHoliday = LeaveHoliday.IsChecked; //产假
            bool? failHoliday = FailHoliday.IsChecked; //病假
            bool? yearHoliday = YearHoliday.IsChecked; //年休假

            //调用方法实现前台读取XML
            var printTxtValue = ReadXml.GetLeavePrintTxtValue(departmentText, thingHoliday, loseHoliday, wedHoliday,
                leaveHoliday, failHoliday, yearHoliday);

            FlowDocument flowDocument =
                PrintPreWindow.LoadDocumentAndRender(@"PrintTemplete/LeavePrintTemplate.xaml", printTxtValue);
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
            //timer = new Timer(TimerCallBack, null, 3000, Timeout.Infinite);

            #endregion
        }

        /// <summary>
        /// 打印预览请假单的实现，调用预览模板页进行并且打印
        /// </summary>
        private void PrintPreLeave_Click(object sender, RoutedEventArgs e)
        {
            //调用方法实现前台读取XML,获取前台传递的信息,
            var departmentText = DepartmentBox.Text;
            bool? thingHoliday = ThingHoliday.IsChecked; //事假
            bool? loseHoliday = LoseHoliday.IsChecked; //丧事
            bool? wedHoliday = WedHoliday.IsChecked; //婚事
            bool? leaveHoliday = LeaveHoliday.IsChecked; //产假
            bool? failHoliday = FailHoliday.IsChecked; //病假
            bool? yearHoliday = YearHoliday.IsChecked; //年休假

            var printTxtValue = ReadXml.GetLeavePrintTxtValue(departmentText, thingHoliday, loseHoliday, wedHoliday,
                leaveHoliday, failHoliday, yearHoliday);

            var print = new PrintPreWindow(@"PrintTemplete/LeavePrintTemplate.xaml", printTxtValue)
            {
                Owner = this,
                ShowInTaskbar = false
            };
            print.ShowDialog();
        }

        /// <summary>
        /// 直接打印出工单的实现，调用模板页直接打印
        /// </summary>
        private void PrintGoWork_Click(object sender, RoutedEventArgs e)
        {
            //初始化打印机状态，使其调用打印机接口，弹出框用户可以随机选择打印模板
            var printDialog = new PrintDialog();
            //首先判断只有窗口未打开才能调用打印驱动
            if (printDialog.ShowDialog() != true)
            {
                return;
            }
            //调用方法实现前台读取XML
            var printTxtValue = ReadXml.GetGoWorkPrintTxtValue();
            FlowDocument flowDocument = PrintPreWindow.LoadDocumentAndRender(@"PrintTemplete/GoWorkPrintTemplate.xaml",
                printTxtValue);
            Dispatcher.BeginInvoke(new DoPrintMethod(DoPrint), DispatcherPriority.ApplicationIdle, printDialog,
                ((IDocumentPaginatorSource) flowDocument).DocumentPaginator);
        }

        /// <summary>
        /// 打印预览出工单的实现，调用预览模板页进行并且打印
        /// </summary>
        private void PrintPreGoWork_Click(object sender, RoutedEventArgs e)
        {
            var printTxtValue = ReadXml.GetGoWorkPrintTxtValue();
            var print = new PrintPreWindow(@"PrintTemplete/GoWorkPrintTemplate.xaml", printTxtValue)
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
            timer.Dispose();
            Dispatcher.BeginInvoke(new EnableButtonMethod(EnableButton));
        }

        /// <summary>
        /// 将单击按钮置为可用状态
        /// </summary>
        private void EnableButton()
        {
            PrintLeave.IsEnabled = true;
        }


        /// <summary>
        /// 绑定部门信息
        /// </summary>
        /// <param name="departMent">部门信息</param>
        private void BindDepartMentInfo(string departMent)
        {
            List<string> list = departMent.ToListInfo<string>(',', c => c);
            //组装需要显示的集合
            List<DepartMentInfo> listInfo = list.Select((t, i) => new DepartMentInfo
            {
                Id = i + 1,
                Name = t
            }).ToList();
            DepartmentBox.ItemsSource = listInfo;
            DepartmentBox.SelectedValuePath = "Id";
            DepartmentBox.DisplayMemberPath = "Name";
        }
    }
}