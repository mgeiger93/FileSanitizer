namespace FileSanitizer.BL
{
    public interface ISanitizedFileSreamProvider
    {
        Stream GetSanitizedStream(Stream stream, string fileExtention);
    }
}