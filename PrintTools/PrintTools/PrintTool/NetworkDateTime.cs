// 源文件头信息：
// <copyright file="NetworkDateTime.cs">
// Copyright(c)2014-2034 Kencery.All rights reserved.
// 个人博客：http://www.cnblogs.com/hanyinglong
// 创建人：韩迎龙(kencery)
// 创建时间：2015/06/03
// </copyright>

using System;
using System.Configuration;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace PrintTools
{
    /// <summary>
    /// 功能:读取线上接口中的当前时间，C# WebClient函数实现，组装调用的路径为:
    /// http://api.k780.com:88/?app=life.time&appkey=10003&sign=b59bc3ef6191eb9f747dd4e83c99f2a4&format=json
    /// 修改记录：时间  内容  姓名
    /// 1.
    /// </summary>
    public static class NetworkDateTime
    {
        /// <summary>
        /// 读取Web.Config—URL地址
        /// </summary>
        public static string Url { get; set; }

        /// <summary>
        /// 读取Web.Config—App
        /// </summary>
        public static string App { get; set; }

        /// <summary>
        /// 读取Web.Config—Appkey
        /// </summary>
        public static string Appkey { get; set; }

        /// <summary>
        /// 读取Web.Config—Sign
        /// </summary>
        public static string Sign { get; set; }

        /// <summary>
        /// 读取Web.Config—Format
        /// </summary>
        public static string Format { get; set; }

        /// <summary>
        /// 初始化网址信息，读取app.Config中的信息组装Api接口
        /// </summary>
        static NetworkDateTime()
        {
            Url = ReadWebConfig("url");
            App = ReadWebConfig("app");
            Appkey = ReadWebConfig("appkey");
            Sign = ReadWebConfig("sign");
            Format = ReadWebConfig("format");
        }

        /// <summary>
        /// 调用WebClient读取API中的JSON串解析出系统中的时间
        /// </summary>
        public static DateTime CurrentDateTime()
        {
            //构造访问路径，路径如下：
            //http://api.k780.com:88/?app=life.time&appkey=10003&sign=b59bc3ef6191eb9f747dd4e83c99f2a4&format=json

            DateTime dateTime;
            using (var client = CreateAPiWebClient())
            {
                try
                {
                    var url = string.Format("{0}?app={1}&appkey={2}&sign={3}&format={4}", Url, App, Appkey, Sign, Format);
                    string result = client.DownloadString(url);
                    //获取到string字符串之后将string字符串解析成Json串
                    var apiDateTimeJson = Deseriallize<ApiDateTime>(result);
                    if (apiDateTimeJson.success == "1")
                    {
                        dateTime = DateTime.Parse(apiDateTimeJson.result.datetime_1);
                    }
                    else
                    {
                        dateTime = DateTime.Now;
                    }
                }
                catch
                {
                    dateTime = DateTime.Now;
                }
            }
            return dateTime;
        }

        /// <summary>
        /// 将字符串转换为Json对象
        /// </summary>
        /// <typeparam name="T">转换的对象</typeparam>
        /// <param name="strData">字符串信息</param>
        public static T Deseriallize<T>(string strData)
        {
            var javaScriptSerializer = new JavaScriptSerializer();
            return javaScriptSerializer.Deserialize<T>(strData);
        }

        /// <summary>
        /// 使用WebClient调用API
        /// </summary>
        public static WebClient CreateAPiWebClient()
        {
            var client = new WebClient();
            client.Headers["Content-type"] = "application/json";
            client.Encoding = Encoding.UTF8;
            return client;
        }

        /// <summary>
        /// 读取Web.Config中的信息，传递key信息读取出来value信息
        /// </summary>
        /// <param name="key">key</param>
        public static string ReadWebConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}