// 源文件头信息：
// <copyright file="Common.cs">
// Copyright(c)2014-2034 Kencery.All rights reserved.
// 个人博客：http://www.cnblogs.com/hanyinglong
// 创建人：韩迎龙(kencery)
// 创建时间：2015-7-28
// </copyright>

using System;
using System.Collections.Generic;

namespace PrintTools
{
    /// <summary>
    /// 功能:页面填写之后进行返回需要用到的值
    /// 修改记录：时间  内容  姓名
    /// </summary>
    public static class Common
    {
        /// <summary>
        /// 将按照split分割的字符串转换成List数组
        /// </summary>
        public static List<T> ToListInfo<T>(this string str, char split, Converter<string, T> converHandler)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new List<T>();
            }
            //进行转换
            string[] arrStr = str.Split(split);
            //将一种类型的数组转换为另一种类型的数组。
            T[] tarrStr = Array.ConvertAll(arrStr, converHandler);
            return new List<T>(tarrStr);
        }
    }
}