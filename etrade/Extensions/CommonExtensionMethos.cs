using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace etrade
{
    public static class CommonExtensionMethos
    {
        public static string Serialize(this object _object)
        {
            return JsonConvert.SerializeObject(_object);
        }
        public static T Deserialize<T>(this string jsonData)
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
        public static T DeepClone<T>(this T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
        public static object ReplaceDbNull(this object val)
        {
            if ((val == null))
            {
                return DBNull.Value;
            }
            else if (val.GetType() == typeof(string))
            {
                if (string.IsNullOrEmpty(val as string))
                {
                    return DBNull.Value;
                }
            }

            return val;

        }
        public static string ReplaceNull(this Object val)
        {
            if ((val == null))
            {
                return null;
            }
            else if (val.GetType() == typeof(string))
            {
                if (string.IsNullOrEmpty(val as string))
                {
                    return null;
                }
            }

            return val as string;
        }
        //public static string GetUniqueFileName(string extension)
        //{
        //    return string.Format(@"{0}{1}", DateTime.Now.Ticks, (string.IsNullOrEmpty(extension) ? "" : string.Format(".{0}", extension.Replace(".", ""))));
        //}
        //public static string SaveFile(this HttpPostedFileBase file)
        //{
        //    string relativePath = ConfigurationManager.AppSettings["FileUploadDir"];
        //    if (string.IsNullOrEmpty(relativePath))
        //    {
        //        throw new Exception("File Upload location Not Configured, Key=>FileUploadDir");
        //    }
        //    var actualLocation = HttpContext.Current.Server.MapPath("~" + relativePath);
        //    if (!Directory.Exists(actualLocation))
        //    {
        //        Directory.CreateDirectory(actualLocation);
        //    }
        //    var fileName = Path.GetFileName(file.FileName);
        //    string documentName = fileName;
        //    string extension = new FileInfo(documentName).Extension;
        //    string wellKnownFileName = GetUniqueFileName(extension);
        //    var path = Path.Combine(actualLocation, wellKnownFileName);
        //    file.SaveAs(path);
        //    return Path.Combine(relativePath, wellKnownFileName);
        //}
        //public static bool DeleteFile(string Dir, string fileName)
        //{
        //    try
        //    {
        //        var filePath = HttpContext.Current.Server.MapPath("~" + Path.Combine(Dir, fileName));
        //        if (File.Exists(filePath))
        //        {
        //            File.Delete(filePath);

        //        }
        //        return true;
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        throw new Exception(string.Format("File Deletion Aborted. File not found -'{0}'", fileName));
        //    }
        //}
        public static string ReplaceNullIfEmpty(this string str)
        {
            return string.IsNullOrEmpty(str) ? null : str;
        }
    }
}
