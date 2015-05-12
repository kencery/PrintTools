// 源文件头信息：
// <copyright file="PrintTxtValue.cs">
// Copyright(c)2014-2034 Kencery.All rights reserved.
// 个人博客：http://www.cnblogs.com/hanyinglong
// 创建人：韩迎龙(kencery)
// 创建时间：2015/05/08
// </copyright>
namespace PrintTools
{
    /// <summary>
    /// 功能:页面填写之后进行返回需要用到的值
    /// 修改记录：时间  内容  姓名
    /// 1.
    /// </summary>
    public class PrintTxtValue
    {
        /// <summary>
        /// 请假人名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string DepartMent { get; set; }

        /// <summary>
        /// 假别
        /// </summary>
        public string[] LeaveOther { get; set; }

        /// <summary>
        /// 请假时间(小时，天数)
        /// </summary>
        public string LeaveDateTime { get; set; }

        /// <summary>
        /// 起止日期
        /// </summary>
        public string StartTime { get; set; }

        /// <summary>
        /// 主管意见
        /// </summary>
        public string BeInChangeIdea { get; set; }

        /// <summary>
        /// 公司领导意见
        /// </summary>
        public string LeadIdea { get; set; }

        /// <summary>
        /// 主管意见签字
        /// </summary>
        public string BeInChangeIdeaSign { get; set; }

        /// <summary>
        /// 公司领导签字
        /// </summary>
        public string LeadIdeaSign { get; set; }

        /// <summary>
        /// 构造函数，赋给类初始化的数据
        /// </summary>
        public PrintTxtValue()
        {
            var indexDic = ReadXml.ReadXmlTitle();

        }
    }
}