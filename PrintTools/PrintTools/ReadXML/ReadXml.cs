// 源文件头信息：
// <copyright file="ReadXml.cs">
// Copyright(c)2014-2034 Kencery.All rights reserved.
// 个人博客：http://www.cnblogs.com/hanyinglong
// 创建人：韩迎龙(kencery)
// 创建时间：2015/05/06
// </copyright>

using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Xml;

namespace PrintTools
{
    public class RadioButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                return !(bool)value;
            }
            return value;
        }
    }

    /// <summary>
    /// 功能:实现读取系统名称和各种类型以修改及备注信息(方便用户，不需要写死，如果为空也可以，系统会填写默认值)(请假单，出工单)
    /// 修改记录：时间  内容  姓名
    /// 1.
    /// </summary>
    public static class ReadXml
    {
        //存放XML地址
        public static string PathXMl { get; set; }

        /// <summary>
        /// 初始化地址信息，将XML的地址存放在PathXML中
        /// </summary>
        static ReadXml()
        {
            PathXMl = @"..\..\xml\MenuRemarkName.xml";
        }

        /// <summary>
        /// 从XML中读取小工具的名称
        /// </summary>
        public static Dictionary<string, string> ReadXmlTitle()
        {
            //第一步：定义Dictionary<string,string>泛型存放页面公用的内容，Dictionary的键不能相同
            var dictionary = new Dictionary<string, string>();

            //第二步：读取XML文件
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(PathXMl);

            //第三步：解析XML内容，读取XML集合进行foreach集合整理
            var xmlElement = xmlDocument.DocumentElement; //获取跟节点信息
            if (xmlElement == null)
            {
                return dictionary;
            }
            XmlNodeList xmlNodeLists = xmlElement.ChildNodes;
            //第四步：给Dictionary赋值进行返回
            foreach (XmlElement xmlNodeList in xmlNodeLists)
            {
                dictionary.Add(xmlNodeList.Name, xmlNodeList.InnerText);
            }
            return dictionary;
        }

        /// <summary>
        /// 读取XML文件中的信息，传递到前台进行绑定—请假条
        /// </summary>
        public static PrintTxtValue GetLeavePrintTxtValue(string departmentText, bool? thingHoliday, bool? loseHoliday,
            bool? wedHoliday, bool? leaveHoliday, bool? failHoliday, bool? yearHoliday)
        {
            //调用方法，读取XML
            var indexDic = ReadXml.ReadXmlTitle();
            //构造需要显示到前端的信息
            var printTxtValue = new PrintTxtValue
            {
                PrintDateTime = NetworkDateTime.CurrentDateTime().ToString("yyyy-MM-dd HH:mm"),
                Title = string.IsNullOrEmpty(indexDic["leave"]) ? "请假条" : indexDic["leave"],
                CompanyName = string.IsNullOrEmpty(indexDic["companyName"]) ? "甘肃联众建筑设计有限责任公司" : indexDic["companyName"],
                Remark = string.IsNullOrEmpty(indexDic["leaveRemak"]) ? "暂无备注" : indexDic["leaveRemak"],
                DepartMent = departmentText,
                ThingHoliday = thingHoliday,
                LoseHoliday = loseHoliday,
                WedHoliday = wedHoliday,
                LeaveHoliday = leaveHoliday,
                FailHoliday = failHoliday,
                YearHoliday = yearHoliday
            };
            return printTxtValue;
        }

        /// <summary>
        /// 读取XML文件中的信息，传递到前台进行绑定—出工单
        /// </summary>
        public static PrintTxtValue GetGoWorkPrintTxtValue()
        {
            //调用方法，读取XML
            var indexDic = ReadXml.ReadXmlTitle();
            //构造前段需要显示的信息
            var printTxtValue = new PrintTxtValue
            {
                PrintDateTime = NetworkDateTime.CurrentDateTime().ToString("yyyy-MM-dd HH:mm"),
                Title = string.IsNullOrEmpty(indexDic["goWork"]) ? "出工单" : indexDic["goWork"],
                CompanyName = string.IsNullOrEmpty(indexDic["companyName"]) ? "甘肃联众建筑设计有限责任公司" : indexDic["companyName"],
                Remark = string.IsNullOrEmpty(indexDic["goWorkRemrk"]) ? "暂无备注" : indexDic["goWorkRemrk"]
            };
            return printTxtValue;
        }
    }
}