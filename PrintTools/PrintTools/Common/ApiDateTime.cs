// 源文件头信息：
// <copyright file="ApiDateTime.cs">
// Copyright(c)2014-2034 Kencery.All rights reserved.
// 个人博客：http://www.cnblogs.com/hanyinglong
// 创建人：韩迎龙(kencery)
// 创建时间：2015/06/03
// </copyright>


namespace PrintTools
{
    /// <summary>
    /// 进行转换API中读取到的Json信息
    /// </summary>
    public class ApiDateTime
    {
        /// <summary>
        /// 调用API返回是否成功的标志
        /// </summary>
        public string success { get; set; }

        /// <summary>
        /// 时间详细信息
        /// </summary>
        public DetailInfo result { get; set; }
    }

        /// <summary>
        /// 读取调用API之后返回的所有的详细信息
        /// </summary>
    public class DetailInfo
    {
        /// <summary>
        /// 时间签
        /// </summary>
        public string timestamp { get; set; }

       /// <summary>
       /// 时间，格式：2015-06-03 22:25:51
       /// </summary>
        public string datetime_1 { get; set; }

        /// <summary>
        /// 时间，格式：2015年06月03日 22时25分51秒
        /// </summary>
        public string datetime_2 { get; set; }

        /// <summary>
        /// 星期，格式：3
        /// </summary>
        public string week_1 { get; set; }

        /// <summary>
        /// 星期，格式：星期三
        /// </summary>
        public string week_2 { get; set; }

        /// <summary>
        /// 星期，格式：周三
        /// </summary>
        public string week_3 { get; set; }

        /// <summary>
        /// 星期，格式：Tuesday
        /// </summary>
        public string week_4 { get; set; }
    }
}