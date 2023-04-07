using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security;
using System.IO;
using System.Security.Cryptography;
using System.Text;
public static class ABUtility
{
    private static readonly double[] byteUnits = {1073741824.0, 1048576.0, 1024.0, 1};

    private static readonly string[] byteUnitsNames = {"GB", "MB", "KB", "B"};
    
    /// <summary>
    /// 获取文件大小
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static string FormatBytes(ulong bytes)
    {
        var size = "0 B";
        if (bytes == 0) return size;
        for (int i = 0; i < byteUnits.Length; i++)
        {
            if(!(bytes >= byteUnits[i]))
                continue;
            size = $"{bytes / byteUnits[i]:##.##} {byteUnitsNames[i]}";
        }
        return size;
    }
    /// <summary>
    /// md5文件
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string MD5File(string path)
    {
        if(!File.Exists(path))
        {
            Debug.LogErrorFormat("File {0} not Exits!!! ", path);
            return string.Empty;
        }
        using (var fs = File.OpenRead(path))
        {
            var bytes = MD5.Create(path).ComputeHash(fs);
            return ToHash(bytes);
        }
    }
    /// <summary>
    /// 转换成hash值
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    static string ToHash(byte[] data)
    {
        if (data == null) return string.Empty;
        StringBuilder sb = new StringBuilder();
        foreach (var byteData in data)
        {
            sb.Append(byteData.ToString("X2"));
        }
        return sb.ToString();
    }
    
    
}
