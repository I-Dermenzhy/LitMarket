namespace MVC.Services;

public class FileHelper(IWebHostEnvironment webHostEnvironment) : IFileHelper
{
    private const string s_bookImagesDirectoryRelativePath = @"images/books";

    private readonly IWebHostEnvironment _webHostEnvironment = webHostEnvironment
        ?? throw new ArgumentNullException(nameof(webHostEnvironment));

    public void DownloadBookImage(IFormFile file, string bookTitle, out string imageUrl)
    {
        ArgumentNullException.ThrowIfNull(file, nameof(file));

        string fileName = GenerateFileName(file);
        string directoryPath = GetFileDirectoryPath(bookTitle);
        string filePath = GetFilePath(directoryPath, fileName);

        Directory.CreateDirectory(directoryPath);

        SaveFile(file, filePath);

        imageUrl = $"/{s_bookImagesDirectoryRelativePath}/{bookTitle}/{fileName}";

    }

    public void DeleteFile(string fileUrl)
    {
        string wwwRootPath = _webHostEnvironment.WebRootPath;

        string path = Path.Combine(wwwRootPath, fileUrl.TrimStart('/'));

        if (File.Exists(path))
            File.Delete(path);
    }

    private static void SaveFile(IFormFile file, string filePath)
    {
        using FileStream fileStream = new(filePath, FileMode.Create);
        file.CopyTo(fileStream);
    }

    private string GetFileDirectoryPath(string? bookTitle)
    {
        string imagesDirectoryPath = GetImagesDirectoryPath();

        if (bookTitle is null)
            return imagesDirectoryPath;

        return Path.Combine(imagesDirectoryPath, bookTitle);
    }

    private static string GetFilePath(string directoryPath, string fileName)
        => Path.Combine(directoryPath, fileName);

    private static string GenerateFileName(IFormFile file)
        => Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

    private string GetImagesDirectoryPath()
    {
        string wwwRootPath = _webHostEnvironment.WebRootPath;
        return Path.Combine(wwwRootPath, s_bookImagesDirectoryRelativePath);
    }
}