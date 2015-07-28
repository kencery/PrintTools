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
        /// 事假
        /// </summary>
        public bool? ThingHoliday { get; set; }

        /// <summary>
        /// 丧事
        /// </summary>
        public bool? LoseHoliday { get; set; }

        /// <summary>
        /// //婚事
        /// </summary>
        public bool? WedHoliday { get; set; }

        /// <summary>
        /// 产假
        /// </summary>
        public bool? LeaveHoliday { get; set; }

        /// <summary>
        /// 病假
        /// </summary>
        public bool? FailHoliday { get; set; }

        /// <summary>
        /// 年休假
        /// </summary>
        public bool? YearHoliday { get; set; }

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


        //V1.0需要返回到前段的信息------------------------------------
        /// <summary>
        /// 打印时间—读取XML
        /// </summary>
        public string PrintDateTime { get; set; }

        /// <summary>
        /// 标题(请假条，出工单)—读取XML
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司备注
        /// </summary>
        public string Remark { get; set; }
    }
}