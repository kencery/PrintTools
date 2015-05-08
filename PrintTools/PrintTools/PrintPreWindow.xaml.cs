using System;
using System.IO;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Threading;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace PrintTools
{
    /// <summary>
    /// 封装打印预览和直接打印的底层实现类
    /// </summary>
    partial class PrintPreWindow
    {
        /// <summary>
        /// WPF使用委托加载XPS打印对象
        /// </summary>
        private delegate void LoadXpsMethod();

        /// <summary>
        /// 传递需要打印的数据集合
        /// </summary>
        private readonly Object _objectData;

        /// <summary>
        /// WPF XPS .NET底层封装的需要打印的内容
        /// </summary>
        private readonly FlowDocument _flowDocument;

        /// <summary>
        /// 只是单个的运行打印预览的模板
        /// </summary>
        public PrintPreWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造初始化对象
        /// </summary>
        public PrintPreWindow(string strTemlName, Object data, IDocumentRender render = null)
        {
            InitializeComponent();
            //初始化的时候调用对象
            _objectData = data;
            _flowDocument = LoadDocumentAndRender(strTemlName, data, render);
            Dispatcher.BeginInvoke(new LoadXpsMethod(LoadXps), DispatcherPriority.ApplicationIdle);
        }

        /// <summary>
        /// 提供给需要打印预览和打印按钮调用的方法
        /// </summary>
        /// <param name="strTemlName">打印模板的路径</param>
        /// <param name="data">需要打印的内容</param>
        /// <param name="documentRender">实现打印的模板赋值文件</param>
        public static FlowDocument LoadDocumentAndRender(string strTemlName, object data,
            IDocumentRender documentRender = null)
        {
            var flowDocument =
                (FlowDocument) Application.LoadComponent(new Uri(strTemlName, UriKind.RelativeOrAbsolute));
            flowDocument.PagePadding = new Thickness();
            flowDocument.DataContext = data;
            if (documentRender != null)
            {
                documentRender.Render(flowDocument, data);
            }
            return flowDocument;
        }

        /// <summary>
        /// 执行异步委托，使用内存流
        /// </summary>
        public void LoadXps()
        {
            //构造一个基于内存的XPS Document
            var memoryStream = new MemoryStream();
            Package package = Package.Open(memoryStream, FileMode.Create, FileAccess.ReadWrite);
            var uri = new Uri("pack://InMemoryDocument.xps");
            PackageStore.RemovePackage(uri);
            PackageStore.AddPackage(uri, package);
            var xpsDocument = new XpsDocument(package, CompressionOption.Fast, uri.AbsoluteUri);
            //将FlowDocument写入基于内存的XPSDocument中去
            XpsDocumentWriter xpsDocumentWriter = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
            xpsDocumentWriter.Write(((IDocumentPaginatorSource) _flowDocument).DocumentPaginator);
            //获取这个基于内存的XPSDocument的FixedDocument
            DocumentViewer.Document = xpsDocument.GetFixedDocumentSequence();
            //关闭基于内存的XPSDocument,释放资源
            xpsDocument.Close();
        }
    }
}