using System;
using System.Linq;

namespace FWS.Generic.Framework.Helpers.File
{
    public static class FilePathHelper
    {
        public static string GetFileNameFromFilePath(string filePath)
        {
            return filePath?.Split(new char[] {'\\'}, StringSplitOptions.RemoveEmptyEntries).Last();
        }
    }
}
