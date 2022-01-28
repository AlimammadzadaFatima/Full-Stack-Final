using ImtahanBack.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ImtahanBack.Helper
{
    public class FileManager
    {
       
        
        public static string Save(string rootpath, string folder, IFormFile formFile)
        {
            string filename = formFile.FileName;
            filename = filename.Length <= 64 ? filename : filename.Substring(filename.Length - 64, 64).ToString();
            filename = Guid.NewGuid().ToString() + filename;
            var path = Path.Combine(rootpath, folder, filename);
            using(FileStream stream = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(stream);
            }
            return filename;
        }
        public static bool Delete (string rootpath, string folder, string filename)
        {
            var path = Path.Combine(rootpath, folder, filename);
            if (File.Exists(path))
            {
                System.IO.File.Delete(path);
                return true;
            }
            return false;
        }
    }
}
