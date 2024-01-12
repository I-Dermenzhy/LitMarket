namespace MVC.Services;

public interface IFileHelper
{
    public void DownloadBookImage(IFormFile file, string bookTitle, out string imageUrl);
    public void DeleteFile(string fileUrl);
}