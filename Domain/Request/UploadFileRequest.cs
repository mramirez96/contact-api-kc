namespace Domain.Request
{
    public class UploadFileRequest
    {
        public string ContentType { get; set; } = "image/png";
        public string FileName { get; set; }
        public string ImageDataBase64 { get; set; }
    }
}
