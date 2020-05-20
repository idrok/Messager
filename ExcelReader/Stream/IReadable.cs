using System;

namespace GameInnvoation.ExcelReader
{
    /// <summary>
    /// 文件读取方式，基本支持
    /// 1.本地文件读取，asset
    /// 2.本地文件读取ex，指定文件路径
    /// 3.从远端下载读取，http服务器，addressable资源
    /// </summary>
    public interface IReadable
    {
        // byte[] Reader<ReaderType>(Func<ReaderType, IDisposable> action);
        
        // 接口带泛型，接口的泛型和普通的泛型不一样，接口的泛型是一种新的数据类型
        // 接口的泛型是不能直接来使用，必须通过协调以后才可以使用
        // 泛型接口比不带泛型的接口复杂很多
        void Reader<T>(IConfigable ini, Action<T> callback);
    }
}