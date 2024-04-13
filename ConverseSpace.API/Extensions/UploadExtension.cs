using ConverseSpace.Application.Posts.Commands;
using ConverseSpace.Domain;
using ConverseSpace.Domain.Models.Enums;

namespace ConverseSpace.API.Extensions;

public static class UploadExtension
{
    public static Result<List<UploadResponse>> Upload(IFormFileCollection files, long maxFileSizeInBytes = 20 * 1024 * 1024)
    {
        var validMimeTypes = new List<string>() { "image/jpeg", "image/png", "image/gif", "audio/mpeg" };
        var responses = new List<UploadResponse>();

        foreach (var file in files)
        {
            string mimeType = file.ContentType;

            if (!validMimeTypes.Contains(mimeType))
                return Result<List<UploadResponse>>.Failure(new Error(400, "Не поддерживаемый тип файла"));

            long size = file.Length;
            if (size > maxFileSizeInBytes)
                return Result<List<UploadResponse>>.Failure(new Error(400, "Файл должен весить меньше 20 МБ"));

            Guid id = Guid.NewGuid();
            string extension = Path.GetExtension(file.FileName);
            string filename = id.ToString() + extension;
            string directoryPath = Path.Combine("/var/lib/app/Uploads/", "Posts");
            string filePath = Path.Combine(directoryPath, filename);

            try
            {
                Directory.CreateDirectory(directoryPath);

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                MediaType? type = GetMediaTypeFromExtension(extension);

                responses.Add(new UploadResponse(id, filePath, type!.Value));
            }
            catch (Exception ex)
            {
                return Result<List<UploadResponse>>.Failure(new Error(500, $"Ошибка при загрузке файла: {ex.Message}"));
            }
        }

        return Result<List<UploadResponse>>.Success(responses);
    }

    private static MediaType? GetMediaTypeFromExtension(string extension)
    {
        switch (extension)
        {
            case ".jpg":
            case ".png":
                return MediaType.Img;
            case ".mp3":
                return MediaType.Audio;
            case ".gif":
                return MediaType.Gif;
            default:
                return null;
        }
    }
}

