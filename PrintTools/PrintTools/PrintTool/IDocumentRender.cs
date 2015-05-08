// 源文件头信息：
// <copyright file="IDocumentRender.cs">
// Copyright(c)2014-2034 Kencery.All rights reserved.
// 个人博客：http://www.cnblogs.com/hanyinglong
// 创建人：韩迎龙(kencery)
// 创建时间：2015/05/08
// </copyright>

using System;
using System.Windows.Documents;

namespace PrintTools
{
    /// <summary>
    /// 功能:声明接口，定义接口初始化
    /// 修改记录：时间  内容  姓名
    /// 1.
    /// </summary>
    public interface IDocumentRender
    {
        /// <summary>
        /// 调用方法
        /// </summary>
        /// <param name="flowDocument">承载流内容和设置流内容格式</param>
        /// <param name="data">传递需要转换流文件的信息</param>
        void Render(FlowDocument flowDocument, Object data);
    }
}