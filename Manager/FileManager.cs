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
            string path = "";
            switch (fileStorePAth)
            {
                case FileStorePAth.img:
                    path = Path.Combine(Environment.ContentRootPath, "img");
                    break;
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Path.GetFileName(file.FileName);
            while (File.Exists(Path.Combine(path, fileName)))
            {
                fileName = fileName.Split(".")[0] + (new Random().Next(10000, 99999).ToString()) + "." +
                           fileName.Split(".")[1];
            }

            using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return fileName;
        }

        public static void DeleteFile(string filename, FileStorePAth fileStorePAth, IWebHostEnvironment Environment)
        {
            switch (fileStorePAth)
            {
                case FileStorePAth.img:
                    if (File.Exists(Path.Combine(Environment.ContentRootPath, "img/" + filename)))
                        System.IO.File.Delete(Path.Combine(Environment.ContentRootPath, "img/" + filename!));
                    break;
            }
        }
    }
}