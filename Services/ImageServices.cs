namespace Admin.Services
{
    public class ImageServices
    {
        public async Task<string> SaveFileAsync(IFormFile file)
        {
            var currentDir = Directory.GetCurrentDirectory();
            currentDir = Path.Combine(currentDir, @"Upload\Images\");
            if (!Directory.Exists(currentDir))
            {
                Directory.CreateDirectory(currentDir);
            }

            string path = Path.Combine(currentDir, currentDir + file.FileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
                stream.Flush();
            }
            return file.FileName;
        }

        public async Task<FileStream> GetImageAsync(string fileName)
        {
            fileName = @"Upload\Images\" + fileName;
            var image = File.OpenRead(fileName);
            return image;
        }
    }
}
