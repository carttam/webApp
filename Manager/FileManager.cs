namespace webApp.Manager
{
    public class FileManager
    {
        public enum FileStorePAth
        {
            img,
        }
        public static string storeAs(IFormFile file, FileStorePAth fileStorePAth, IWebHostEnvironment Environment)
        {
            if (file != null)
            {
                string path = "";
                switch (fileStorePAth)
                {
                    case FileStorePAth.img:
                        path = Path.Combine(Environment.WebRootPath, "img");
                        break;
                }
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = Path.GetFileName(file.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create ))
                {
                    file.CopyTo(stream);
                }
                return fileName;
            }
            return "";
        }
        public static void DeleteFile(string filename, FileStorePAth fileStorePAth, IWebHostEnvironment Environment)
        {
            if (filename != null)
            {
                switch (fileStorePAth)
                {
                    case FileStorePAth.img:
                        System.IO.File.Delete(Path.Combine(Environment.WebRootPath, "img/" + filename));
                        break;
                }
            }
        }
    }
}
